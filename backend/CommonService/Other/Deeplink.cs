using CommonService.Other.AppConfig;
using ModelService.Model.Front;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System.Web;

namespace CommonService.Other
{
    public static class Deeplink
    {
        public static DeepLinkModel GenerateDeeplink(this string obj, string socialTitle="",string socialDescription="",string socialImageLink ="")
        {
            try
            {
                string linkUrl = "";
                DeepLinkModel dynamic = new DeepLinkModel();
                var jObj = (JObject)JsonConvert.DeserializeObject(obj);
                var query = String.Join("&",
                    jObj.Children().Cast<JProperty>()
                    .Select(jp => jp.Name + "=" + HttpUtility.UrlEncode(jp.Value.ToString())));
                linkUrl = query;
                dynamic.dynamicLinkInfo.domainUriPrefix = AppConfigFactory.Configs.deepLinksConfigs.domainUriPrefix;
                dynamic.dynamicLinkInfo.androidInfo.androidPackageName = AppConfigFactory.Configs.deepLinksConfigs.AndroidPackageName;
                dynamic.dynamicLinkInfo.iosInfo.iosBundleId = AppConfigFactory.Configs.deepLinksConfigs.IosBundleId;
                dynamic.dynamicLinkInfo.link = AppConfigFactory.Configs.appUrlConfigs.FrontUrl + linkUrl;
                dynamic.dynamicLinkInfo.socialMetaTagInfo.socialTitle = socialTitle;
                dynamic.dynamicLinkInfo.socialMetaTagInfo.socialDescription = socialDescription;
                dynamic.dynamicLinkInfo.socialMetaTagInfo.socialImageLink = socialImageLink;

                return dynamic;
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("Deeplink.cs", "GenerateDeeplink"));
                throw ex;
            }
          
        }

        public static object PushNotification<T>(this T data, Notification notification, string priority, string to)
        {
            return new NotificationRquestModel<T> { data = data, notification = notification, priority = priority, to = to };
        }

        public class Notification
        {
            public string? title { get; set; }
            public string? body { get; set; }
            public string? click_action { get; set; }
        }
        public class NotificationRquestModel<T>
        {
            public Notification? notification { get; set; }
            public T data { get; set; }
            public string? priority { get; set; }
            public string? to { get; set; }
        }
    }

}
