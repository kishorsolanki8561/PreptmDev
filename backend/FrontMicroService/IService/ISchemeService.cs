using ModelService.Model.Front;
using static CommonService.Other.UtilityManager;

namespace FrontMicroService.IService
{
    public interface ISchemeService
    {
        ServiceResponse<SchemeFrontModel> GetModuleWiseDataByIdAndSlug(int? id, string? slugUrl, bool IsAdminView = false);
    }
}
