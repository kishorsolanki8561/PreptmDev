using ModelService.Model;
using ModelService.Model.MastersModel;
using static CommonService.Other.UtilityManager;

namespace MasterMicroService.IService
{
    public interface ILookupTypeService
    {
        ServiceResponse<int> AddUpdate(LookupTypeModel model);
        ServiceResponse<LookupTypeViewModel> GetById(int Id);
        ServiceResponse<bool> Delete(int Id);
        ServiceResponse<bool> UpdateStatus(int Id);
        ServiceResponse<List<LookupTypeViewListModel>> GetPagination(LookupTypeFilterModel filterModel);
    }
}
