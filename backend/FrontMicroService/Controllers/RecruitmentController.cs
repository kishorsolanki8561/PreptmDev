using FrontMicroService.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelService.Model.Translation;
using System.Linq;

namespace FrontMicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RecruitmentController : ControllerBase
    {
        private readonly IRecruitmentService _recruitmentService;
        public RecruitmentController(IRecruitmentService recruitmentService)
        {
            _recruitmentService = recruitmentService;
        }
        [HttpPost]
        public object GetFrontRecruitmentList(RecruitmentFilterModel filterModel)
        {
            return _recruitmentService.GetFrontRecruitmentList(filterModel);
        }
        [HttpGet]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, VaryByHeader = "lang", VaryByQueryKeys = new[] { "*" })]
        public async Task<IActionResult> GetRecruitmentDetailsOfIdAndSlug(int? id, string? slugUrl)
        {
            var result = await _recruitmentService.GetRecruitmentDetailsOfIdAndSlug(id, slugUrl);
            if (result.Data == null) return NotFound(result);
            return Ok(result);
        }
    }
}
