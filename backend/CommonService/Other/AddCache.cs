using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CommonService.Other
{
    public static class AddCache
    {
        public static void AddCacheBuilder(WebApplicationBuilder builder)
        {
            _ = builder.Services.AddMemoryCache(options =>
            {
                options.SizeLimit = 512; // max 512 entries
            });
            _ = builder.Services.AddResponseCaching();
        }

        public static void UseCacheMiddleware(WebApplication app)
        {
            app.UseResponseCaching();
        }
    }
}
