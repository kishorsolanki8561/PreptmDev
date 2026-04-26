using ModelService.Model.MastersModel;
using static CommonService.Other.UtilityManager;

namespace MasterMicroService.IService
{
    public interface IAdditionalPagesService
    {
        ServiceResponse<int> AddUpdate(AdditionalPagesModel model);
        ServiceResponse<AdditionalPagesViewModel> GetById(int Id);
        ServiceResponse<List<AdditionalPagesListModel>> GetList();

    }
}
