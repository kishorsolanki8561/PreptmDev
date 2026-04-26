using CommonService.JWT;
using CommonService.Other;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Text;

namespace CommonService.Middlewares
{
    public class EncryptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HelperService _helperService;
        public EncryptionMiddleware(RequestDelegate next, HelperService helperService)
        {
            _next = next;
            _helperService = helperService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                string[] sitemapPath = context.Request.Path.Value.Split('/');
                if (sitemapPath is not null && sitemapPath.Length > 0 && sitemapPath.Any(s => s.ToLower() == "getsitemap"))
                {
                    await _next(context);
                    return;
                }

                if (sitemapPath is not null && sitemapPath.Length > 0 && sitemapPath.Any(s => s.ToLower() == "getfiles"))
                {
                    await _next(context);
                    return;
                }
                if (sitemapPath is not null && sitemapPath.Length > 0 && sitemapPath.Any(s => s.ToLower() == "createnewpath"))
                {
                    await _next(context);
                    return;
                }

                string isMobileMode = context.Request.Headers["isMobileMode"].FirstOrDefault()?.Split(" ").Last() ?? "";
                if (!string.IsNullOrEmpty(isMobileMode) && isMobileMode.ToLower() == "true")
                {
                    await _next(context);
                    return;
                }
                string Origin = !string.IsNullOrEmpty(context.Request.Headers.Origin) ? context.Request.Headers.Origin.ToString().Replace("https://", "").Replace("http://", "").ToLower() : context.Request.Host.Value.ToString().Replace("https://", "").Replace("http://", "").ToLower();
                if (string.IsNullOrEmpty(Origin) || ((!string.IsNullOrEmpty(Origin) && Origin.Split(":")[0] != "204.12.245.106") && (!string.IsNullOrEmpty(Origin) && Origin.Split(":")[0] != "localhost") && (!string.IsNullOrEmpty(Origin) && Origin.Split(":")[0] != "admin.preptm.com") && (!string.IsNullOrEmpty(Origin) && Origin.Split(":")[0] != "sadmin.preptm.com")))
                {
                    Console.WriteLine(Origin);
                    context.Request.EnableBuffering();
                    var requestBodyStream = new MemoryStream();
                    string Query = context.Request.QueryString.ToString();
                    await context.Request.Body.CopyToAsync(requestBodyStream);
                    var requestBody = Encoding.UTF8.GetString(requestBodyStream.ToArray());

                    if (!string.IsNullOrEmpty(requestBody))
                    {
                        string decryptedRequestBody = _helperService.Decrypt(requestBody);
                        context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(decryptedRequestBody));
                        context.Request.Body.Position = 0;
                        context.Request.ContentType = "application/json,multipart/form-data";
                    }

                    if (!string.IsNullOrEmpty(Query))
                    {
                        Query = Query.Replace("?", "");
                        string decryptedQueryString = _helperService.Decrypt(Query);
                        context.Request.QueryString = new QueryString("?" + decryptedQueryString);
                    }

                    //Replace the response body stream to capture response
                    var originalResponseBodyStream = context.Response.Body;
                    using var responseBodyStream = new MemoryStream();
                    context.Response.Body = responseBodyStream;
                    await _next(context);

                    // Encrypt response
                    context.Response.Body.Seek(0, SeekOrigin.Begin);
                    var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
                    var encryptedResponseBody = _helperService.Encrypt(responseBody);
                    var encryptedResponseBodyBytes = Encoding.UTF8.GetBytes(encryptedResponseBody);
                    context.Response.Body = originalResponseBodyStream;
                    //context.Response.ContentType = "application/json,text/plain";

                    await context.Response.Body.WriteAsync(encryptedResponseBodyBytes, 0, encryptedResponseBodyBytes.Length);
                }
                else
                {
                    await _next(context);
                    return;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("EncryptionMiddleware.cs", "InvokeAsync"));
            }

        }
    }
}
