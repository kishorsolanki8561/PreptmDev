using ModelService.Model.Translation.Article;
using ModelService.Model.Translation.Paper;
using static CommonService.Other.UtilityManager;

namespace TranslationMicroService.Service.Paper
{
    public interface IPaperService
    {
        Task<ServiceResponse<int>> AddUpdate(PaperRequestDTO model);
        Task<ServiceResponse<PaperResponseDTO>> GetById(int Id);
        Task<ServiceResponse<bool>> Delete(int Id);
        Task<ServiceResponse<bool>> UpdateStatus(int Id);
        Task<ServiceResponse<bool>> ProgressStatus(int Id, int ProgressStatus);
        ServiceResponse<List<PapperTitleCheckDTO>> CheckArticleTitle(string SearchText);
        ServiceResponse<List<PapperViewListDTO>> GetPagination(PaperFilterDTO filterModel);
    }
}
