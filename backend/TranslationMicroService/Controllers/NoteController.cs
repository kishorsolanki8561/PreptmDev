using CommonService.JWT;
using CommonService.Other;
using Microsoft.AspNetCore.Mvc;
using ModelService.Model.Translation.Note;
using TranslationMicroService.Service.Note;
using static CommonService.Other.UtilityManager;

namespace TranslationMicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdate(NoteRequestDTO model)
        {
            bool IsSuccess = false;
            var Data = UtilityManager.PostValidator<NoteRequestDTO>.IsValid(ref IsSuccess, model);
            if (!IsSuccess) return Conflict(Data);
            return Ok(await _noteService.AddUpdate(model));
        }

        [HttpGet]
        public async Task<ActionResult> GetById(int Id)
        {
            return Ok(await _noteService.GetById(Id));
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int Id)
        {
            return Ok(await _noteService.Delete(Id));
        }

        [HttpGet]
        public async Task<ActionResult> UpdateStatus(int Id)
        {
            return Ok(await _noteService.UpdateStatus(Id));
        }

        [HttpPost]
        public ActionResult GetList(NoteFilterDTO GetPagination)
        {
            return Ok(_noteService.GetPagination(GetPagination));
        }

        [HttpGet]
        public async Task<ActionResult> ProgressStatus(int Id, int ProgressStatus)
        {
            return Ok(await _noteService.ProgressStatus(Id, ProgressStatus));
        }

        [HttpGet]
        public ServiceResponse<List<NoteTitleCheckDTO>> CheckArticleTitle(string SearchText)
        {
            return _noteService.CheckArticleTitle(SearchText);
        }
    }
}
