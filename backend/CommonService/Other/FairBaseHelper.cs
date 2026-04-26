using CommonService.JWT;
using ModelService.CommonModel;
using ModelService.Model;
using ModelService.Model.Front;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static CommonService.Other.Deeplink;
using static Dapper.SqlMapper;
using Newtonsoft.Json.Linq;
using CommonService.Other.AppConfig;
using Serilog;

namespace CommonService.Other
{
    public class FairBaseHelper : UtilityManager
    {
        private HttpClient _Http;
        public FairBaseHelper()
        {
            _Http = new HttpClient();
            _Http.BaseAddress = new Uri(AppConfigFactory.Configs.fairBaseConfigs.FairBaseUrl);
        }
        public string Post(DeepLinkModel model)
        {
            try
            {
                _Http = new HttpClient();
                _Http.BaseAddress = new Uri(AppConfigFactory.Configs.fairBaseConfigs.FairBaseUrl);
                var Newresponse = _Http.PostAsJsonAsync("/v1/shortLinks?key=" + AppConfigFactory.Configs.fairBaseConfigs.FairBaseKey, model).GetAwaiter().GetResult();
                Newresponse.EnsureSuccessStatusCode();
                return Newresponse.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("FairBaseHelper.cs", "Post"));
                throw ex;
            }
        }
        public bool PostPushNotification(object data, string Title = "", string body = "", string priority = "High")
        {
            try
            {
                Notification notification = new Notification() { title = Title, body = "Tap for more details", click_action = "FLUTTER_NOTIFICATION_CLICK" };
                var fcmTokenTo = QueryList<string>(@"select [To] from Vw_GetFCMTokenOfFrontUser", null).Data.ToList();
                string res = "";
                foreach (var fcmT in fcmTokenTo)
                {
                    if (!string.IsNullOrEmpty(fcmT))
                    {
                        var pushModel = data.PushNotification(notification, priority, fcmT);
                        string stringjson = JsonConvert.SerializeObject(pushModel);
                        Console.WriteLine(stringjson);
                        _Http = new HttpClient();
                        _Http.BaseAddress = new Uri(AppConfigFactory.Configs.fairBaseConfigs.FcmFairBaseUrl);
                        _Http.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", AppConfigFactory.Configs.fairBaseConfigs.FcmFairBaseKey);
                        _Http.DefaultRequestHeaders.Accept
                                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Newresponse = _Http.PostAsJsonAsync("fcm/send", pushModel).GetAwaiter().GetResult();
                        Newresponse.EnsureSuccessStatusCode();
                        res = Newresponse.Content.ReadAsStringAsync().Result;
                        var jkhkh = res;
                    }
                }
                return !string.IsNullOrEmpty(res) ? true : false;
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("FairBaseHelper.cs", "PostPushNotification"));
                throw ex;
            }
            
        }
    }
}
