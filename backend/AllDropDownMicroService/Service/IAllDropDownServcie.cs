using CommonService.Other;
using static CommonService.Other.HelperService;
using static CommonService.Other.UtilityManager;

namespace AllDropDownMicroService.Service
{
    public interface IAllDropDownServcie
    {
        UtilityManager.ServiceResponse<IDictionary<string, object>> AllDropDown(string keys, string userType = "", int? userId = 0);
        ServiceResponse<List<SelectListsItem>> GetSubCategory(string SlugUrl = "", int cateCode = 0);
        ServiceResponse<IDictionary<string, object>> GetDDLLookupDataByLookupTypeIdAndLookupType(string SlugUrl, string LookupType = "", string LookupTypeId = "");
    }
    
}
