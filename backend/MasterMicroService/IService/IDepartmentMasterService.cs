using ModelService.Model.MastersModel;
using static CommonService.Other.UtilityManager;

namespace MasterMicroService.IService
{
    public interface IDepartmentMasterService
    {
        ServiceResponse<int> AddUpdate(DepartmentMasterModel model);
        ServiceResponse<List<DepartmentMasterViewModel>> GetList();
        ServiceResponse<DepartmentMasterModel> GetById(int Id);
        ServiceResponse<bool> Delete(int Id);
        ServiceResponse<bool> UpdateStatus(int Id);
        ServiceResponse<List<DepartmentMasterViewModel>> GetPagination(DepartmentMasterFilterModel filterModel);
    }
}
