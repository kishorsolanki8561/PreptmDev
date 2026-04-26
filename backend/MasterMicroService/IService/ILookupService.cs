using ModelService.Model.MastersModel;
using static CommonService.Other.UtilityManager;

namespace MasterMicroService.IService
{
    public  interface ILookupService
    {
        ServiceResponse<int> AddUpdate(LookupModel model);
        ServiceResponse<LookupViewModel> GetById(int Id);
        ServiceResponse<bool> Delete(int Id);
        ServiceResponse<bool> UpdateStatus(int Id);
        ServiceResponse<List<LookupViewListModel>> GetPagination(LookupFilterModel filterModel);
    }
}
