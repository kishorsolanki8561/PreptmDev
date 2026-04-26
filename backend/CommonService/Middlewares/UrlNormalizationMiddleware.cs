using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CommonService.Middlewares
{
    /// <summary>
    /// Enforces canonical URL conventions with 301 permanent redirects:
    ///   1. Lowercase all path segments
    ///   2. Strip trailing slashes (except root "/")
    /// www / HTTPS enforcement is handled at the reverse-proxy (IIS / nginx) level.
    /// </summary>
    public class UrlNormalizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<UrlNormalizationMiddleware> _logger;

        public UrlNormalizationMiddleware(RequestDelegate next, ILogger<UrlNormalizationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var req = context.Request;
            var path = req.Path.Value ?? "/";

            // Skip non-page requests (API, health checks, static files, sitemaps)
            if (ShouldSkip(path))
            {
                await _next(context);
                return;
            }

            bool needsRedirect = false;
            string normalizedPath = path;

            // 1. Strip trailing slash (keep root "/" intact)
            if (normalizedPath.Length > 1 && normalizedPath.EndsWith("/"))
            {
                normalizedPath = normalizedPath.TrimEnd('/');
                needsRedirect = true;
            }

            // 2. Lowercase the path
            string lowered = normalizedPath.ToLowerInvariant();
            if (lowered != normalizedPath)
            {
                normalizedPath = lowered;
                needsRedirect = true;
            }

            if (needsRedirect)
            {
                var location = normalizedPath + req.QueryString;
                _logger.LogDebug("URL normalised: {Original} → {Normalised}", path, location);
                context.Response.StatusCode = StatusCodes.Status301MovedPermanently;
                context.Response.Headers["Location"] = location;
                return;
            }

            await _next(context);
        }

        private static bool ShouldSkip(string path)
        {
            return path.StartsWith("/api/", StringComparison.OrdinalIgnoreCase)
                || path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase)
                || path.StartsWith("/health", StringComparison.OrdinalIgnoreCase)
                || path.Equals("/sitemap.xml", StringComparison.OrdinalIgnoreCase)
                || path.Equals("/robots.txt", StringComparison.OrdinalIgnoreCase);
        }
    }
}
