using ModelService.Model.Translation;
using static CommonService.Other.UtilityManager;

namespace TranslationMicroService.IService
{
    public interface ISchemeService
    {
        ServiceResponse<int> AddUpdate(SchemeRequestModel model);
        ServiceResponse<SchemeViewModel> GetById(int Id);
        public ServiceResponse<bool> Delete(int Id);
        public ServiceResponse<List<SchemeViewListModel>> GetPagination(SchemeFilterModel filterModel);
        public ServiceResponse<bool> UpdateStatus(int Id);
        public ServiceResponse<bool> SchemeProgressStatus(int Id, int ProgressStatus);
        public ServiceResponse<List<SchemeTitleCheckModel>> CheckSchemeTitle(string SearchText);
    }
}
