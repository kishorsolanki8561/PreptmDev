using CommonService.Other;
using CommonService.Other.AppConfig;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CommonService
{
    public class JWTHelper
    {
        public static string GetClaimValue(dynamic key, string token)
        {
            try
            {
                ClaimsPrincipal principal = getPrincipal(token.Replace("Bearer ", ""));
                if (principal == null)
                {
                    return null;
                }
                ClaimsIdentity identity = null;
                identity = (ClaimsIdentity)principal.Identity;
                return identity.Claims.Where(c => c.Type == key)
                   .Select(c => c.Value).SingleOrDefault();

            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("JWTHelper.cs", "GetClaimValue"));
                return null;
            }
        }

        public static ClaimsPrincipal getPrincipal(string token)
        {

            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                {
                    return null;
                }
                byte[] key = Encoding.ASCII.GetBytes(AppConfigFactory.Configs.jWTConfigs.Secret);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
                      parameters, out securityToken);
                return principal;
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("JWTHelper.cs", "getPrincipal"));
                return null;
            }
        }
    }
}
