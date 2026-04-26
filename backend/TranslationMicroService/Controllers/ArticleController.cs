using CommonService.JWT;
using CommonService.Other;
using Microsoft.AspNetCore.Mvc;
using ModelService.Model.Translation.Article;
using TranslationMicroService.IService;
using static CommonService.Other.UtilityManager;

namespace TranslationMicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdate(ArticleRequestDTO model)
        {
            bool IsSuccess = false;
            var Data = UtilityManager.PostValidator<ArticleRequestDTO>.IsValid(ref IsSuccess, model);
            if (!IsSuccess) return Conflict(Data);
            return Ok(await _articleService.AddUpdate(model));
        }

        [HttpGet]
        public ActionResult GetById(int Id)
        {
            return Ok(_articleService.GetById(Id));
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int Id)
        {
            return Ok(await _articleService.Delete(Id));
        }

        [HttpGet]
        public async Task<ActionResult> UpdateStatus(int Id)
        {
            return Ok(await _articleService.UpdateStatus(Id));
        }

        [HttpPost]
        public ActionResult GetList(ArticleFilterModel GetPagination)
        {
            return Ok(_articleService.GetPagination(GetPagination));
        }

        [HttpGet]
        public async Task<ActionResult> ProgressStatus(int Id, int ProgressStatus)
        {
            return Ok(await _articleService.ProgressStatus(Id, ProgressStatus));
        }

        [HttpGet]
        public ServiceResponse<List<ArticleTitleCheckModel>> CheckArticleTitle(string SearchText)
        {
            return _articleService.CheckArticleTitle(SearchText);
        }
    }
}
