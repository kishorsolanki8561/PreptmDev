using ModelService.Model.MastersModel;
using ModelService.Model.Translation;
using static CommonService.Other.UtilityManager;

namespace FrontMicroService.IService
{
    public interface IRecruitmentService
    {
        ServiceResponse<List<DashboardRecentAndPopularPostModel>> GetFrontRecruitmentList(RecruitmentFilterModel filterModel);
        ServiceResponse<object> GetModuleWiseDataByIdAndSlug(int? id, string? slugUrl, bool isRecruitment = false);
        ServiceResponse<DepartmentFrontViewModel> GetDepartmentDataByIdAndSlug(int? id, string? slugUrl);
        Task<ServiceResponse<object>> GetRecruitmentDetailsOfIdAndSlug(int? id, string? slugUrl, bool IsAdminView = false);
    }
}
