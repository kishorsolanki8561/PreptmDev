using CommonService.JWT;
using CommonService.Other;
using Microsoft.AspNetCore.Mvc;
using ModelService.Model.Translation.Note;
using ModelService.Model.Translation.Syllabus;
using TranslationMicroService.Service.Note;
using TranslationMicroService.Service.Syllabus;
using static CommonService.Other.UtilityManager;

namespace TranslationMicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class SyllabusController : ControllerBase
    {
        private readonly ISyllabusService _syllabusService;
        public SyllabusController(ISyllabusService syllabusService)
        {

            _syllabusService = syllabusService;

        }

        [HttpPost]
        public async Task<ActionResult> AddUpdate(SyllabusRequestDTO model)
        {
            bool IsSuccess = false;
            var Data = UtilityManager.PostValidator<SyllabusRequestDTO>.IsValid(ref IsSuccess, model);
            if (!IsSuccess) return Conflict(Data);
            return Ok(await _syllabusService.AddUpdate(model));
        }

        [HttpGet]
        public async Task<ActionResult> GetById(int Id)
        {
            return Ok(await _syllabusService.GetById(Id));
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int Id)
        {
            return Ok(await _syllabusService.Delete(Id));
        }

        [HttpGet]
        public async Task<ActionResult> UpdateStatus(int Id)
        {
            return Ok(await _syllabusService.UpdateStatus(Id));
        }

        [HttpPost]
        public ActionResult GetList(SyllabusFilterDTO GetPagination)
        {
            return Ok(_syllabusService.GetPagination(GetPagination));
        }

        [HttpGet]
        public async Task<ActionResult> ProgressStatus(int Id, int ProgressStatus)
        {
            return Ok(await _syllabusService.ProgressStatus(Id, ProgressStatus));
        }

        [HttpGet]
        public ServiceResponse<List<SyllabusTitleCheckDTO>> CheckArticleTitle(string SearchText)
        {
            return _syllabusService.CheckArticleTitle(SearchText);
        }
    }
}
