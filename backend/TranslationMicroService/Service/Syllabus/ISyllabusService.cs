using ModelService.Model.Translation.Note;
using ModelService.Model.Translation.Syllabus;
using static CommonService.Other.UtilityManager;

namespace TranslationMicroService.Service.Syllabus
{
    public interface ISyllabusService
    {
        Task<ServiceResponse<int>> AddUpdate(SyllabusRequestDTO model);
        Task<ServiceResponse<SyllabusResponseDTO>> GetById(int Id);
        Task<ServiceResponse<bool>> Delete(int Id);
        Task<ServiceResponse<bool>> UpdateStatus(int Id);
        Task<ServiceResponse<bool>> ProgressStatus(int Id, int ProgressStatus);
        ServiceResponse<List<SyllabusTitleCheckDTO>> CheckArticleTitle(string SearchText);
        ServiceResponse<List<SyllabusViewListDTO>> GetPagination(SyllabusFilterDTO filterModel);
    }
}
