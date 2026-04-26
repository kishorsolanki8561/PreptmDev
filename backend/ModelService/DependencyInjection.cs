using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ModelService
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddModelService(this IServiceCollection services)
        {
            _ = services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
