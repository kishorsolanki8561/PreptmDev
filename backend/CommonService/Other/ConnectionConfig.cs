using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonService.Other
{
    public class ConnectionConfig
    {
        public static string GetConfigValue(string Configuration, string KeyName)
        {
            using (StreamReader sr = new StreamReader($"{Configuration}.json"))
            {
                var json = sr.ReadToEnd();
                var data = (JObject?)JsonConvert.DeserializeObject(json);
                if (data[KeyName] == null) return null;
                return data[KeyName].Value<string>();
            }
        }
    }
}
