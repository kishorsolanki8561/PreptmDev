using ModelService.Model.MastersModel;
using static CommonService.Other.UtilityManager;

namespace MasterMicroService.IService
{
    public interface IPageMasterService
    {
        //ServiceResponse<int> AddUpdate(PageMasterModel model);
        ServiceResponse<List<PageMasterViewModel>> GetList();
        //ServiceResponse<PageMasterModel> GetById(int Id);
        ServiceResponse<bool> Delete(int Id);
        ServiceResponse<bool> UpdateStatus(int Id);
        ServiceResponse<List<PageMasterViewModel>> GetPagination(PageMasterFilterModel filterModel);
        ServiceResponse<List<PagePermissionViewModel>> PagePermissionList(int userTypeCode);
        ServiceResponse<bool> PageMasterPermissionModifiedById(List<int> model, int UserTypeCode);
        ServiceResponse<int> AddUpdate(PageMasterModel model);
        ServiceResponse<PageMasterModel> GetById(int Id);
    }
}
