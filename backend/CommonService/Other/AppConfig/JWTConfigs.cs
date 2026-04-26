using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonService.Other.AppConfig
{
    public class JWTConfigs
    {
        public string?  Secret { get; set; }
        public string? EncryptDecryptKey { get; set; }
    }
}
