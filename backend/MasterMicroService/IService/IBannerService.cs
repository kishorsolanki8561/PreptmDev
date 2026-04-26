using ModelService.Model.MastersModel;
using static CommonService.Other.UtilityManager;

namespace MasterMicroService.IService
{
    public interface IBannerService
    {
        ServiceResponse<int> AddUpdate(BannerModel model);
        ServiceResponse<BannerViewModel> GetById(int Id);
        ServiceResponse<bool> Delete(int Id);
        ServiceResponse<bool> UpdateStatus(int Id);
        ServiceResponse<List<BannerViewListModel>> GetPagination(BannerFilterModel filterModel);
    }
}
