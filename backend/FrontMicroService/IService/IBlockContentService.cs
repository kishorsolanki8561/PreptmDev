using ModelService.Model;
using ModelService.Model.Translation;
using static CommonService.Other.UtilityManager;

namespace FrontMicroService.IService
{
    public interface IBlockContentService
    {
        ServiceResponse<List<DashboardRecentAndPopularPostModel>> GetFrontBlockContentList(FrontBlockContentFilterModel filterModel);
        ServiceResponse<object> GetBlockContentDetailsOfIdAndSlug(int? id, string? slugUrl,bool IsAdminView= false);
    }
}
