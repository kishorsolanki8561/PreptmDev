using ModelService.Model.MastersModel;
using static CommonService.Other.UtilityManager;

namespace MasterMicroService.IService
{
    public interface IAssetsMasterService
    {
        ServiceResponse<int> AddUpdate(AssetsMasterModel model);
        ServiceResponse<AssetsMasterModel> GetById(int Id);
        ServiceResponse<List<AssetsMasterViewModel>> GetPagination(AssetsMasterFilterModel filterModel);
        ServiceResponse<bool> Delete(int Id);
        ServiceResponse<bool> UpdateStatus(int Id);
    }
}
