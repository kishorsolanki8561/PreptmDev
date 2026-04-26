using ModelService.Model.MastersModel;
using static CommonService.Other.UtilityManager;

namespace MasterMicroService.IService
{
    public interface IUserTypeMasterService
    {
        ServiceResponse<int> AddUpdate(UserTypeMasterModel model);
        ServiceResponse<bool> Delete(int Id);
        ServiceResponse<UserTypeMasterModel> GetById(int Id);
        ServiceResponse<List<UserTypeMasterViewModel>> GetList();
        ServiceResponse<bool> UpdateStatus(int Id);
    }
}
