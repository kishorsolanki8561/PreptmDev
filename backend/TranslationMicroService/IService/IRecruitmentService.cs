using ModelService.Model.Translation;
using ModelService.Model.Translation.Recruitment;
using static CommonService.Other.UtilityManager;

namespace TranslationMicroService.IService
{
    public interface IRecruitmentService
    {
        //ServiceResponse<int> AddUpdate(RecruitmentModel model);
        Task<ServiceResponse<long>> AddUpdate(RecruitmentReqestDTO model);
        //ServiceResponse<RecruitmentModel> GetById(int Id);
        ServiceResponse<RecruitmentResponseDTO> GetById(int Id);
        ServiceResponse<bool> Delete(int Id);
        ServiceResponse<bool> UpdateStatus(int Id);
        ServiceResponse<List<RecruitmentViewModel>> GetPagination(RecruitmentFilterModel filterModel);
        ServiceResponse<bool> RecruitmentProgressStatus(int Id, int ProgressStatus);
        ServiceResponse<List<RecruitmentTitleCheckModel>> CheckRecruitmentTitle(string SearchText);
    }
}
