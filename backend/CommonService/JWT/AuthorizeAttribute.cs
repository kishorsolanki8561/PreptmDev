using CommonService.Dapper;
using CommonService.Other;
using CommonService.Other.AppConfig;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using ModelService.Model.Front;
using ModelService.Model.MastersModel;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CommonService.JWT
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        DapperGenericRepo _utilityManager = new DapperGenericRepo();
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                if (!string.IsNullOrEmpty(token) && IsValid(token))
                {
                    if (RouteUser(token))
                    {
                        var frontUser = (FrontUserViewModel)context.HttpContext.Items["frontUser"];
                        if (frontUser == null)
                        {
                            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                        }
                    }
                    else
                    {
                        var user = (UserMasterViewModel)context.HttpContext.Items["users"];
                        if (user == null)
                        {
                            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                        }
                        else
                        {
                            var result = _utilityManager.QueryFast<UserViewModel>("select * from Vw_User where Id=@Id", new { Id = user.Id }).Data;
                            if (result.IsAutoLoggedOut)
                            {
                                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                            }
                        }
                    }
                }
                else
                {
                    context.Result = new JsonResult(new { message = "Unauthorized(Invalid Token) Note: Token has been expired" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("AuthorizeAttribute.cs", "OnAuthorization"));
            }
        }

        private bool RouteUser(string token)
        {
            try
            {
                var Secret = AppConfigFactory.Configs.jWTConfigs.Secret;// _configuration.GetValue<string>("Secret");
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                return Boolean.Parse(jwtToken.Claims.First(x => x.Type == "IsFront").Value);
            }
            catch(Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("AuthorizeAttribute.cs", "RouteUser"));
                return false;
            }
        }
        private bool IsValid(string token)
        {
            JwtSecurityToken jwtSecurityToken;
            try
            {
                jwtSecurityToken = new JwtSecurityToken(token);
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("AuthorizeAttribute.cs", "IsValid"));
                return false;
            }

            return jwtSecurityToken.ValidTo > DateTime.UtcNow;
        }
    }
}
