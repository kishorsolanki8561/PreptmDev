using AllDropDownMicroService.Service;
using CommonService.JWT;
using CommonService.Other;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using static CommonService.Other.HelperService;
using static CommonService.Other.UtilityManager;

namespace AllDropDownMicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class AllDropDownController : ControllerBase
    {
        private readonly IAllDropDownServcie _allDropDownServcie;
        public AllDropDownController(IAllDropDownServcie allDropDownServcie)
        {
            _allDropDownServcie = allDropDownServcie;
        }
        [HttpGet]
        public ServiceResponse<IDictionary<string, object>> AllDropDown(string keys, string? userType = "", int? userid = 0)
        {
            ServiceResponse<IDictionary<string, object>> objReturn = new ServiceResponse<IDictionary<string, object>>();
            try
            {
                if (!string.IsNullOrEmpty(keys))
                {
                    return _allDropDownServcie.AllDropDown(keys, userType, userid);
                }
                objReturn.IsSuccess = false;
                objReturn.Message = MessageStatus.Error;
                return objReturn;
            }
            catch
            {
                objReturn.IsSuccess = false;
                objReturn.Message = MessageStatus.Error;
                return objReturn;
            }
        }

        [HttpGet]
        public object GetSubCategory(string? categorySlugUrl, int? cateCode)
        {
            ServiceResponse<List<SelectListsItem>> objReturn = new ServiceResponse<List<SelectListsItem>>();
            try
            {
                return _allDropDownServcie.GetSubCategory(!string.IsNullOrEmpty(categorySlugUrl) ? categorySlugUrl : string.Empty, cateCode != null ? cateCode.Value : 0);
            }
            catch
            {
                objReturn.IsSuccess = false;
                objReturn.Message = MessageStatus.Error;
                return objReturn;
            }
        }

        [HttpGet]
        public object GetDDLLookupDataByLookupTypeIdAndLookupType(string? SlugUrl="", string? LookupType = "", string? LookupTypeId = "")
        {
            return _allDropDownServcie.GetDDLLookupDataByLookupTypeIdAndLookupType(SlugUrl,LookupType,LookupTypeId);
        }
    }
}
