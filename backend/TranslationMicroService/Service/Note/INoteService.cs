using ModelService.Model.Translation.Note;
using ModelService.Model.Translation.Paper;
using static CommonService.Other.UtilityManager;

namespace TranslationMicroService.Service.Note
{
    public interface INoteService
    {
        Task<ServiceResponse<int>> AddUpdate(NoteRequestDTO model);
        Task<ServiceResponse<NoteResponseDTO>> GetById(int Id);
        Task<ServiceResponse<bool>> Delete(int Id);
        Task<ServiceResponse<bool>> UpdateStatus(int Id);
        Task<ServiceResponse<bool>> ProgressStatus(int Id, int ProgressStatus);
        ServiceResponse<List<NoteTitleCheckDTO>> CheckArticleTitle(string SearchText);
        ServiceResponse<List<NoteViewListDTO>> GetPagination(NoteFilterDTO filterModel);
    }
}
