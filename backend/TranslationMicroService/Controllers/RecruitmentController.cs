using CommonService.JWT;
using CommonService.Other;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelService.Model;
using ModelService.Model.Translation;
using ModelService.Model.Translation.Recruitment;
using Newtonsoft.Json;
using Serilog;
using System.Text;
using TranslationMicroService.IService;
using static CommonService.Other.UtilityManager;

namespace TranslationMicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class RecruitmentController : ControllerBase
    {
        private readonly IRecruitmentService _recruitmentService;
        public RecruitmentController(IRecruitmentService recruitmentService)
        {
            _recruitmentService = recruitmentService;
        }

        [ProducesResponseType(typeof(ServiceResponse<string>), 200)]
        [HttpPost]
        public async Task<ActionResult> AddUpDate(RecruitmentReqestDTO model)
        {
            bool IsSuccess = false;
            var Data = UtilityManager.PostValidator<RecruitmentReqestDTO>.IsValid(ref IsSuccess, model);
            if (!IsSuccess) return Conflict(Data);
            return Ok(await _recruitmentService.AddUpdate(model));
        }

        [HttpGet]
        public ServiceResponse<RecruitmentResponseDTO> GetById(int Id)
        {
            ServiceResponse<RecruitmentResponseDTO> obj = new ServiceResponse<RecruitmentResponseDTO>();
            try
            {
                return _recruitmentService.GetById(Id);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
        [HttpGet]
        public ServiceResponse<bool> Delete(int Id)
        {
            ServiceResponse<bool> obj = new ServiceResponse<bool>();
            try
            {
                return _recruitmentService.Delete(Id);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
        [HttpGet]
        public ServiceResponse<bool> UpdateStatus(int Id)
        {
            ServiceResponse<bool> obj = new ServiceResponse<bool>();
            try
            {
                return _recruitmentService.UpdateStatus(Id);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
        [HttpGet]
        public ServiceResponse<bool> RecruitmentProgressStatus(int Id, int ProgressStatus)
        {
            ServiceResponse<bool> obj = new ServiceResponse<bool>();
            try
            {
                return _recruitmentService.RecruitmentProgressStatus(Id, ProgressStatus);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
        [HttpPost]
        public ServiceResponse<List<RecruitmentViewModel>> GetPagination(RecruitmentFilterModel filterModel)
        {
            ServiceResponse<List<RecruitmentViewModel>> obj = new ServiceResponse<List<RecruitmentViewModel>>();
            try
            {
                return _recruitmentService.GetPagination(filterModel);

            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }

        [HttpGet]
        public ServiceResponse<List<RecruitmentTitleCheckModel>> CheckRecruitmentTitle(string SearchText)
        {
            ServiceResponse<List<RecruitmentTitleCheckModel>> obj = new ServiceResponse<List<RecruitmentTitleCheckModel>>();
            try
            {
                return _recruitmentService.CheckRecruitmentTitle(SearchText);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
    }
}
