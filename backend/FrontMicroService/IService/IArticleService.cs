using ModelService.Model.Front.ArticleDTO;
using static CommonService.Other.UtilityManager;

namespace FrontMicroService.IService
{
    public interface IArticleService
    {
        ServiceResponse<ArticlesDTO> GetArticles(ArticleFilterDTO filterModel);
        ServiceResponse<object> GetArticleDetails(int? id, string? slugUrl, bool IsAdminView = false);
    }
}
