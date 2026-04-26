using ModelService.Model.Front;
using ModelService.Model.MastersModel;
using static CommonService.Other.UtilityManager;


namespace MasterService.IService
{
    public interface IUserService
    {
        ServiceResponse<int> AddUpdate(UserMasterModel model);
        ServiceResponse<List<UserViewModel>> GetList();
        ServiceResponse<UserViewModel> GetById(int Id);
        ServiceResponse<bool> Delete(int Id);
        ServiceResponse<bool> UpdateStatus(int Id);

        ServiceResponse<UserMasterViewModel> GetUserLogin(LoginModel model);
        ServiceResponse<List<UserMasterPaginationModel>> GetPagination(UserFilterModel filterModel);
        ServiceResponse<List<FrontUserListModel>> GetFrontUserReport(UserFilterModel filterModel);
        ServiceResponse<List<UserFeedbackViewListModel>> GetFrontUserFeedbackReport(UserFeedbackFilterModel filterModel);
        ServiceResponse<bool> UserFeedbackProgressStatus(int Id, int ProgressStatus);
    }
}
