using CommonService.Other;
using CommonService.Other.AppConfig;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using ModelService.Model;
using ModelService.Model.Front;
using ModelService.Model.MastersModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using Serilog.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CommonService.JWT
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JWTAuthManager _jWTAuthManager;
        public JwtMiddleware(RequestDelegate next, JWTAuthManager jWTAuthManager)
        {
            _next = next;
            _jWTAuthManager = jWTAuthManager;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                _jWTAuthManager.LanguageCode = context.Request.Headers["lang"].FirstOrDefault()?.Split(" ").Last() ?? "en";
                _jWTAuthManager.RequestUrl = context.Request.Headers["adminurl"].FirstOrDefault()?.Split(" ").Last() ?? "";
                if (token != null)
                {
                    if (IsValid(token))
                    {
                        string requset = await FormatRequest(context.Request);
                        string Scheme = context.Request.Scheme;
                        string Host = context.Request.Host + context.Request.Path;
                        string QueryString = context.Request.QueryString.ToString();
                        if (!string.IsNullOrEmpty(requset))
                            LogContext.PushProperty("Body", requset);
                        if (!string.IsNullOrEmpty(Scheme))
                            LogContext.PushProperty("Scheme", Scheme);
                        if (!string.IsNullOrEmpty(Host))
                            LogContext.PushProperty("Host", Host);
                        if (!string.IsNullOrEmpty(QueryString))
                            LogContext.PushProperty("QueryString", QueryString);

                        List<HeaderObject> headers = new List<HeaderObject>();
                        foreach (KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues> header in context.Request.Headers)
                        {
                            headers.Add(new HeaderObject { HeaderName = header.Key, HeaderValue = header.Value });
                        }
                        LogContext.PushProperty("Headers", JsonConvert.SerializeObject(headers));
                        string response = await FormatResponse(context.Request);

                        attachUserToContext(context, token);
                    }
                    else
                    {
                        _jWTAuthManager.User = null;
                        _jWTAuthManager.FrontUser = null;
                    }
                }

                else
                {
                    _jWTAuthManager.User = null;
                    _jWTAuthManager.FrontUser = null;
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("JwtMiddleware.cs", "attachUserToContext"));
            }

        }

        private void attachUserToContext(HttpContext context, string token)
        {
            try
            {
                var Secret = AppConfigFactory.Configs.jWTConfigs.Secret;
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var frontUser = new FrontUserViewModel();
                var User = new UserMasterViewModel();
                User.IsFront = Boolean.Parse(jwtToken.Claims.First(x => x.Type == "IsFront").Value);
                _jWTAuthManager.Token = token ?? "";
                if (User.IsFront)
                {
                    frontUser.Id = int.Parse(jwtToken.Claims.First(x => x.Type == "Id").Value);
                    frontUser.Name = jwtToken.Claims.First(x => x.Type == "Name").Value;
                    _jWTAuthManager.FrontUser = frontUser;
                    context.Request.HttpContext.Items["frontUser"] = frontUser;
                }
                else
                {
                    User.Id = int.Parse(jwtToken.Claims.First(x => x.Type == "Id").Value);
                    User.Name = jwtToken.Claims.First(x => x.Type == "Name").Value;
                    User.IsAutoLoggedOut = Convert.ToBoolean(jwtToken.Claims.First(x => x.Type == "IsAutoLoggedOut").Value);
                    //User.Companyid = int.Parse(jwtToken.Claims.First(x => x.Type == "CompanyId").Value);
                    _jWTAuthManager.User = User;
                    context.Request.HttpContext.Items["users"] = User;
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("JwtMiddleware.cs", "attachUserToContext"));
            }
        }
        private async Task<string> FormatRequest(HttpRequest request)
        {
            try
            {
                request.EnableBuffering();
                byte[] buffer = new byte[Convert.ToInt32(request.ContentLength)];
                await request.Body.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
                string bodyAsText = Encoding.UTF8.GetString(buffer);
                request.Body.Position = 0;
                return $"{bodyAsText}";
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("JwtMiddleware.cs", "FormatRequest"));
                return null;
            }
        }
        private async Task<string> FormatResponse(HttpRequest response)
        {
            try
            {
                response.Body.Seek(0, SeekOrigin.Begin);
                string text = await new StreamReader(response.Body).ReadToEndAsync();
                response.Body.Seek(0, SeekOrigin.Begin);
                return $"{text}";

            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("JwtMiddleware.cs", "FormatResponse"));
                return null;
            }
        }

        private bool IsValid(string token)
        {
            JwtSecurityToken jwtSecurityToken;
            try
            {
                var tokenParts = token.Split('.');
                if (tokenParts.Length != 3 && tokenParts.Length != 5)
                {
                    return false;
                }
                jwtSecurityToken = new JwtSecurityToken(token);
                return jwtSecurityToken.ValidTo > DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                {
                    Log.Error(ex, CommonFunction.Errorstring("JwtMiddleware.cs", "IsValid"));
                    Log.Error(ex, CommonFunction.Errorstring("JwtMiddleware.cs", "IsValid-" + token));
                    return false;
                }

            }
        }

        public class HeaderObject
        {
            public string? HeaderName { get; set; }
            public string? HeaderValue { get; set; }
        }

    }
}
