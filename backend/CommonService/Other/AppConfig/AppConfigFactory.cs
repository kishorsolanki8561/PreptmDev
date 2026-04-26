using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonService.Other.AppConfig
{
    public static class AppConfigFactory
    {
        private static IConfiguration _configuration;
        private static IAppConfigs _appConfigs;

        public static void InitializeConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
            _appConfigs = new AppConfigs(_configuration);
        }
        public static IAppConfigs Configs { get => _appConfigs; }
    }
}
