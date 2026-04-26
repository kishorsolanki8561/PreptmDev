using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CommonService.Other
{
    public static class SPResultHandler
    {
        public static T GetObject<T>(dynamic obj)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject((dynamic)obj[0]));
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("SPResultHandler.cs", "GetObject"));
                throw ex;
            }
        }
        public static List<T> GetList<T>(dynamic obj)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(JsonConvert.SerializeObject((dynamic)obj));
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("SPResultHandler.cs", "GetList"));
                throw ex;
            }
        }
        public static List<T> JsonParseList<T>(string jsonText)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(jsonText);
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("SPResultHandler.cs", "JsonParseList"));
                throw ex;
            }
        }
        public static int GetCount(dynamic obj)
        {
            try
            {
                return obj is null ? 0 : (int)(obj as List<object>).Count();

            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("SPResultHandler.cs", "GetCount"));
                throw ex;
            }
        }
        public static XElement ConvertJsonToXml<T>(this T json, string deserializeRootElementName ="")
        {
            string jsonString = JsonConvert.SerializeObject(json);
            JObject jObject = JObject.Parse(jsonString);
            return JsonConvert.DeserializeXNode(jObject.ToString(),!string.IsNullOrEmpty(deserializeRootElementName)? deserializeRootElementName : "Root").Root;
        }
    }
}
