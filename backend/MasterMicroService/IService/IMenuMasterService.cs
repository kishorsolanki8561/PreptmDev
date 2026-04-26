using ModelService.Model.MastersModel;
using static CommonService.Other.UtilityManager;

namespace MasterService.IService
{
    public interface IMenuMasterService
    {
        ServiceResponse<int> AddUpdate(MenuMasterModel model);
        ServiceResponse<List<MenuMasterViewModel>> GetList();
        ServiceResponse<MenuMasterModel> GetById(int Id);
        ServiceResponse<bool> Delete(int Id);
        ServiceResponse<bool> UpdateStatus(int Id);
        ServiceResponse<List<MenuMasterViewModel>> GetPagination(MenuMasterFilterModel filterModel);
        ServiceResponse<List<DynamicMenuModel>> GetDynamicMenuList(int UserTypeCode);
    }
}
