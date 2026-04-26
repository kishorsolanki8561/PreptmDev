using ModelService.Model.Front;
using ModelService.Model.Translation;
using static CommonService.Other.UtilityManager;

namespace FrontMicroService.IService
{
    public interface IDashboardService
    {
        ServiceResponse<DashboardModel> GetDashboardRecentAndPopularPostList(int pageSize);
        Task<ServiceResponse<List<DashboardRecentAndPopularPostModel>>> GetFrontDashboardList(DashboradFilterModel filterModel);
        ServiceResponse<List<DashboardRecentAndPopularPostModel>> GetDashboardSearchFilter(DashboradSearchFilterModel filterModel);
        Task<ServiceResponse<DashboardModel>> GetDashboardData(int pageSize);
        ServiceResponse<List<string>> GetPopularBySearchText(int numberOfRecord, string SearchText);
        ServiceResponse<List<BannerListModel>> GetBannersByPageSize(int numberOfRecord);
        ServiceResponse<string> GetSiteMap(string langCode = "");
        ServiceResponse<string> GetSiteMapIndex();
    }
}
