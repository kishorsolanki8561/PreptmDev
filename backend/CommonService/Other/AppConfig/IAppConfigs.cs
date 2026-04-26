using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonService.Other.AppConfig
{
    public interface IAppConfigs
    {
        JWTConfigs jWTConfigs { get; }
        FilesUrls filesUrls { get; }
        FairBaseConfigs fairBaseConfigs { get; }
        ConnectionStrings connectionStrings { get; }
        DeepLinksConfigs deepLinksConfigs { get; }
        AppUrlConfigs appUrlConfigs { get; }
    }
}
