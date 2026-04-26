using ModelService.Model.MastersModel;
using static CommonService.Other.UtilityManager;

namespace MasterMicroService.IService
{
    public interface IGroupMasterService
    {
        ServiceResponse<int> AddUpdate(GroupMasterModel model);
        ServiceResponse<GroupMasterModel> GetById(int Id);
        ServiceResponse<bool> Delete(int Id);
        ServiceResponse<bool> UpdateStatus(int Id);
        ServiceResponse<List<GroupMasterViewModel>> GetPagination(GroupMasterFilterModel filterModel);
    }
}
