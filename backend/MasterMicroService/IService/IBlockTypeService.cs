using ModelService.Model.MastersModel;
using static CommonService.Other.UtilityManager;

namespace MasterMicroService.IService
{
    public interface IBlockTypeService
    {
        ServiceResponse<int> AddUpdate(BlockTypeModel model);
        ServiceResponse<BlockTypeModel> GetById(int Id);
        ServiceResponse<bool> Delete(int Id);
        ServiceResponse<bool> UpdateStatus(int Id);
        ServiceResponse<List<BlockTypeViewModel>> GetPagination(BlockTypeFilterModel filterModel);
    }
}
