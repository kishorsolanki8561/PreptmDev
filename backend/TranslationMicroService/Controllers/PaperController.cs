using CommonService.JWT;
using CommonService.Other;
using Microsoft.AspNetCore.Mvc;
using ModelService.Model.Translation.Paper;
using TranslationMicroService.Service.Paper;
using static CommonService.Other.UtilityManager;

namespace TranslationMicroService.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaperController : ControllerBase
    {
        private readonly IPaperService _paperService;
        public PaperController(IPaperService paperService)
        {
            _paperService = paperService;
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdate(PaperRequestDTO model)
        {
            bool IsSuccess = false;
            var Data = UtilityManager.PostValidator<PaperRequestDTO>.IsValid(ref IsSuccess, model);
            if (!IsSuccess) return Conflict(Data);
            return Ok(await _paperService.AddUpdate(model));
        }

        [HttpGet]
        public async Task<ActionResult> GetById(int Id)
        {
            return Ok(await _paperService.GetById(Id));
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int Id)
        {
            return Ok(await _paperService.Delete(Id));
        }

        [HttpGet]
        public async Task<ActionResult> UpdateStatus(int Id)
        {
            return Ok(await _paperService.UpdateStatus(Id));
        }

        [HttpPost]
        public ActionResult GetList(PaperFilterDTO GetPagination)
        {
            return Ok(_paperService.GetPagination(GetPagination));
        }

        [HttpGet]
        public async Task<ActionResult> ProgressStatus(int Id, int ProgressStatus)
        {
            return Ok(await _paperService.ProgressStatus(Id, ProgressStatus));
        }

        [HttpGet]
        public ServiceResponse<List<PapperTitleCheckDTO>> CheckArticleTitle(string SearchText)
        {
            return _paperService.CheckArticleTitle(SearchText);
        }
    }
}
