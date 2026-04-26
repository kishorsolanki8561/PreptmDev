using ModelService.Model.MastersModel;
using static CommonService.Other.UtilityManager;

namespace MasterMicroService.IService
{
    public interface ISubCategoryService
    {
        ServiceResponse<int> AddUpdate(SubCategoryModel model);
        ServiceResponse<List<SubCategoryViewModel>> GetList();
        ServiceResponse<SubCategoryModel> GetById(int Id);
        ServiceResponse<bool> Delete(int Id);
        ServiceResponse<bool> UpdateStatus(int Id);
        ServiceResponse<List<SubCategoryViewModel>> GetPagination(SubCategoryFilterModel filterModel);
    }
}
