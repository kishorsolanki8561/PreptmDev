using ModelService.Model;
using static CommonService.Other.UtilityManager;

namespace FrontMicroService.IService
{
    public interface IAdditionalPagesService
    {
        ServiceResponse<string> GetAdditionalPagesByPageType(int PageType);
    }
}
