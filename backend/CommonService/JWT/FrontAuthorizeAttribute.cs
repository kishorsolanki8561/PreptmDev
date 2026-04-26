using CommonService.Other;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ModelService.Model.Front;
using Serilog;

namespace CommonService.JWT
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class FrontAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                //Is The Front User
                var frontUser = (FrontUserViewModel)context.HttpContext.Items["frontUser"];
                if (frontUser == null)
                {
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("FrontAuthorizeAttribute.cs", "OnAuthorization"));
            }
            
        }

    }
}
