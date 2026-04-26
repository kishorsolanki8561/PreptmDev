using ModelService.Model;
using ModelService.Model.Translation;
using ModelService.Model.Translation.Article;
using static CommonService.Other.UtilityManager;

namespace TranslationMicroService.IService
{
    public interface IArticleService
    {
        Task<ServiceResponse<int>> AddUpdate(ArticleRequestDTO model);
        ServiceResponse<ArticleResponseDTO> GetById(int Id);
        Task<ServiceResponse<bool>> Delete(int Id);
        Task<ServiceResponse<bool>> UpdateStatus(int Id);
        Task<ServiceResponse<bool>> ProgressStatus(int Id, int ProgressStatus);
        ServiceResponse<List<ArticleTitleCheckModel>> CheckArticleTitle(string SearchText);
        ServiceResponse<List<ArticleViewListModel>> GetPagination(ArticleFilterModel filterModel);
    }
}
