using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonService.Other.AppConfig;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DBInfrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDBPersistence(this IServiceCollection service)
        {
            service.AddDbContext<DBPreptmContext>(options =>
            {
                options.UseSqlServer(AppConfigFactory.Configs.connectionStrings.Default);
                options.EnableSensitiveDataLogging();
            });

            service.AddScoped<DBPreptmContext>(provider => provider.GetService<DBPreptmContext>());
            service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return service;
        }
    }
}
