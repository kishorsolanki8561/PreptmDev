using ModelService.Model.MastersModel;
using static CommonService.Other.UtilityManager;

namespace MasterMicroService.IService
{
    public interface ICategoryMasterService
    {
        ServiceResponse<int> AddUpdate(CategoryMasterModel model);
        ServiceResponse<List<CategoryMasterViewModel>> GetList();
        ServiceResponse<CategoryMasterModel> GetById(int Id);
        ServiceResponse<bool> Delete(int Id);
        ServiceResponse<bool> UpdateStatus(int Id);
        ServiceResponse<List<CategoryMasterViewModel>> GetPagination(CategoryMasterFilterModel filterModel);
    }
}
