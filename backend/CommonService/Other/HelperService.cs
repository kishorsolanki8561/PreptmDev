using CommonService.JWT;
using CommonService.Other.AppConfig;
using Dapper;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Collections.Generic;
namespace CommonService.Other
{
    public class HelperService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private readonly JWTAuthManager _jWTAuthManager;
        private IWebHostEnvironment _env;
        private static readonly string[] SCOPES = { "https://www.googleapis.com/auth/cloud-platform" }; // Replace with the necessary scopes
        public HelperService(JWTAuthManager jWTAuthManager, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env)
        {
            _httpContextAccessor = httpContextAccessor;
            _jWTAuthManager = jWTAuthManager;
            _env = env;
        }
        public string GetPwHash(string aPassword)
        {
            byte[] Byte = Encoding.Default.GetBytes(aPassword);
            SHA1CryptoServiceProvider lSHA = new SHA1CryptoServiceProvider();
            return BitConverter.ToString(lSHA.ComputeHash(Byte)).Replace("-", "");
        }

        public string GetHeaderValue(string header)
        {
            IHeaderDictionary headers = _httpContextAccessor.HttpContext.Request.Headers;
            string headervalue = headers[header].ToString();
            return headervalue;
        }

        public string GenerateRandomNumber()
        {
            string num = new Random().Next(100000, 999999).ToString();
            num = string.Format(String.Format("{0,6:000000}", num));
            return num;
        }

        public string Encrypt(string plainText)
        {
            byte[] key = Encoding.UTF8.GetBytes(AppConfigFactory.Configs.jWTConfigs.EncryptDecryptKey);
            byte[] iv = Encoding.UTF8.GetBytes(AppConfigFactory.Configs.jWTConfigs.EncryptDecryptKey);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                //aesAlg.KeySize = 16;
                aesAlg.Mode = CipherMode.ECB;
                aesAlg.Padding = PaddingMode.PKCS7;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }
        public string Decrypt(string Text)
        {
            byte[] key = Encoding.UTF8.GetBytes(AppConfigFactory.Configs.jWTConfigs.EncryptDecryptKey);
            byte[] iv = Encoding.UTF8.GetBytes(AppConfigFactory.Configs.jWTConfigs.EncryptDecryptKey);
            byte[] cipherText = Convert.FromBase64String(Text);
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                //aesAlg.KeySize = 16;
                aesAlg.Mode = CipherMode.ECB;
                aesAlg.Padding = PaddingMode.PKCS7;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        public async Task<string> GetAccessTokenAsync()
        {
            try
            {
                string jsonPath = Path.Combine(_env.WebRootPath, "OtherFiles", "preptm-firebase.json");
                GoogleCredential credential;
                using (var stream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
                {
                    credential = GoogleCredential.FromStream(stream)
                                  .CreateScoped(SCOPES);
                }

                // Get access token
                var accessToken = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
                return accessToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting access token: " + ex.Message);
                throw;
            }
        }
        public class SelectListsItem
        {
            //
            // Summary:
            //     Gets or sets the text of the selected item.
            //
            // Returns:
            //     The text.
            public string? Text
            {
                get;
                set;
            }

            //
            // Summary:
            //     Gets or sets the value of the selected item.
            //
            // Returns:
            //     The value.
            public int Value
            {
                get;
                set;
            }
            public dynamic OtherData
            {
                get;
                set;
            }
        }

        public DynamicParameters AddDynamicParameters(dynamic obj, string[] spKeys = null, string[] otherKeys = null)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                JObject converted = JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(obj));
                foreach (KeyValuePair<string, JToken> keyValuePair in converted)
                {
                    if (spKeys != null)
                    {
                        var key = spKeys.Where(z => z == (string)keyValuePair.Key).FirstOrDefault();
                        if (key != null)
                        {

                            switch (keyValuePair.Value.Type)
                            {
                                case JTokenType.Boolean:
                                    parameters.Add("@" + keyValuePair.Key + "", (Boolean)keyValuePair.Value);
                                    break;
                                case JTokenType.Date:
                                    parameters.Add("@" + keyValuePair.Key + "", (DateTime)keyValuePair.Value);
                                    break;
                                case JTokenType.Integer:
                                    parameters.Add("@" + keyValuePair.Key + "", (int)keyValuePair.Value);
                                    break;
                                case JTokenType.String:
                                    if (keyValuePair.Key == "orderBy")
                                    {
                                        parameters.Add("@" + keyValuePair.Key + "", !string.IsNullOrEmpty(keyValuePair.Value.ToString()) ? keyValuePair.Value.ToString() : "CreatedDate");

                                    }
                                    else
                                    {
                                        parameters.Add("@" + keyValuePair.Key + "", !string.IsNullOrEmpty(keyValuePair.Value.ToString()) ? keyValuePair.Value.ToString() : string.Empty);
                                    }
                                    break;
                                case JTokenType.Guid:
                                    parameters.Add("@" + keyValuePair.Key + "", (JValue)keyValuePair.Value);
                                    break;
                                case JTokenType.Null:
                                    parameters.Add("@" + keyValuePair.Key + "", null);
                                    break;
                                case JTokenType.Bytes:
                                    parameters.Add("@" + keyValuePair.Key + "", (byte)keyValuePair.Value);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        switch (keyValuePair.Value.Type)
                        {
                            case JTokenType.Boolean:
                                parameters.Add("@" + keyValuePair.Key + "", (Boolean)keyValuePair.Value);
                                break;
                            case JTokenType.Date:
                                parameters.Add("@" + keyValuePair.Key + "", (DateTime)keyValuePair.Value);
                                break;
                            case JTokenType.Integer:
                                parameters.Add("@" + keyValuePair.Key + "", (int)keyValuePair.Value);
                                break;
                            case JTokenType.String:
                                if (keyValuePair.Key == "OrderBy")
                                {
                                    parameters.Add("@" + keyValuePair.Key + "", !string.IsNullOrEmpty(keyValuePair.Value.ToString()) ? keyValuePair.Value.ToString() : "CreatedDate");
                                }
                                else
                                {
                                    parameters.Add("@" + keyValuePair.Key + "", !string.IsNullOrEmpty(keyValuePair.Value.ToString()) ? keyValuePair.Value.ToString() : string.Empty);

                                }
                                break;
                            case JTokenType.Guid:
                                parameters.Add("@" + keyValuePair.Key + "", (JValue)keyValuePair.Value);
                                break;
                            case JTokenType.Null:
                                parameters.Add("@" + keyValuePair.Key + "", null);
                                break;
                            case JTokenType.Bytes:
                                parameters.Add("@" + keyValuePair.Key + "", (byte)keyValuePair.Value);
                                break;
                            case JTokenType.Object:
                                parameters.Add("@" + keyValuePair.Key + "", keyValuePair.Value, DbType.Xml, ParameterDirection.Input);
                                break;
                        }
                    }
                }
                //other Keys Added
                if (otherKeys != null)
                    foreach (var key in otherKeys)
                    {
                        if (spKeys.Contains(key))
                        {
                            parameters.Add("@UserId", _jWTAuthManager.User.Id);
                        }
                    }

                return parameters;
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("HelperService.cs", "AddDynamicParameters"));
                throw ex;
            }
        }

        public bool GetDifferences<T>(T oldObject, T newObject, List<string> ignoreProperties = null) where T : new()
        {
            if (oldObject == null || newObject == null)
                throw new ArgumentNullException("Both objects must be non-null.");

            // Create a new instance of T to store differences
            bool returnvalue = false;

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Default to an empty list if no properties to ignore are specified
            ignoreProperties = ignoreProperties ?? new List<string>();

            foreach (var property in properties)
            {
                // Skip the property if it is in the ignore list
                if (ignoreProperties.Contains(property.Name))
                    continue;

                if (!property.CanRead || !property.CanWrite) continue;

                var oldValue = property.GetValue(oldObject);
                var newValue = property.GetValue(newObject);

                if (!Equals(oldValue, newValue))
                {
                    returnvalue = true;
                }
            }

            return returnvalue;
        }

        // For comparing multiple objects in a list and ignoring specific properties
        //public  List<T> GetDifferences<T>(List<T> oldObjects, List<T> newObjects, List<string> ignoreProperties = null) where T : new()
        //{
        //    if (oldObjects == null || newObjects == null)
        //        throw new ArgumentNullException("Both object lists must be non-null.");

        //    if (oldObjects.Count != newObjects.Count)
        //        throw new ArgumentException("Both lists must have the same number of objects.");

        //    var differencesList = new List<T>();

        //    for (int i = 0; i < oldObjects.Count; i++)
        //    {
        //        var oldObject = oldObjects[i];
        //        var newObject = newObjects[i];

        //        // Compare each object and add the difference to the result list
        //        differencesList.Add(GetDifferences(oldObject, newObject, ignoreProperties));
        //    }

        //    return differencesList;
        //}
    }
    public static class CommonMethod
    {
        public static string ToAbsolutepathPath(this string url)
        {
            return string.Concat(!string.IsNullOrEmpty(url) ? AppConfigFactory.Configs.filesUrls.UpdateUrlContent : string.Empty, url);
        }
    }

}
