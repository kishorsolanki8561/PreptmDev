using CommonService.Other;
using CommonService.Other.AppConfig;
using Microsoft.IdentityModel.Tokens;
using ModelService.Model.Front;
using ModelService.Model.MastersModel;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CommonService.JWT
{
    public class JWTAuthManager
    {
        public JWTAuthManager()
        {
        }
        public string GetJWT(UserMasterViewModel aUser)
        {
            return generateJwtToken(aUser);
        }
        private string generateJwtToken(UserMasterViewModel aUser)
        {
            try
            {
                // generate token that is valid for 7 days
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(AppConfigFactory.Configs.jWTConfigs.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("Id", aUser.Id.ToString()), new Claim("Name", aUser.Name.ToString()), new Claim("UserTypeCode", aUser.UserTypeCode.ToString()), new Claim("IsAutoLoggedOut", aUser.IsAutoLoggedOut.ToString()), new Claim("IsFront", "false") }),
                    Expires = DateTime.UtcNow.AddMonths(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("JWTAuthManager.cs", "generateJwtToken"));
                return string.Empty;
            }
        }
        public string FrontGetJWT(FrontUserViewModel aUser)
        {
            return FrontGenerateJwtToken(aUser);
        }
        private string FrontGenerateJwtToken(FrontUserViewModel aUser)
        {
            try
            {
                // generate token that is valid for 7 days
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(AppConfigFactory.Configs.jWTConfigs.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("Id", aUser.Id.ToString()), new Claim("Name", aUser.Name.ToString()), new Claim("IsFront", "true") }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);

            }
            catch (Exception ex) {
                Log.Error(ex, CommonFunction.Errorstring("JWTAuthManager.cs", "FrontGenerateJwtToken"));
                return string.Empty;
            }
        }

        public string GenerateJWT(List<Claim> aClaims)
        {
            try
            {
                JwtSecurityTokenHandler lTokenHandler = new JwtSecurityTokenHandler();
                byte[] lKey = Encoding.ASCII.GetBytes(AppConfigFactory.Configs.jWTConfigs.Secret);
                SecurityTokenDescriptor lTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(aClaims),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(lKey), SecurityAlgorithms.HmacSha256Signature)
                };

                return lTokenHandler.WriteToken(lTokenHandler.CreateToken(lTokenDescriptor));
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("JWTAuthManager.cs", "FrontGenerateJwtToken"));
                return string.Empty;
            }
        }

        public UserMasterViewModel User { get; set; }
        public FrontUserViewModel FrontUser { get; set; }

        public string LanguageCode { get; set; } = "en";
        public string Token { get; set; }
        public string RequestUrl { get;set; }

    }
}
