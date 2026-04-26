using FrontMicroService.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontMicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdmissionController : ControllerBase
    {
        private readonly IRecruitmentService _recruitmentService;
        public AdmissionController(IRecruitmentService recruitmentService)
        {
            _recruitmentService = recruitmentService;
        }
        [HttpGet]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, VaryByHeader = "lang", VaryByQueryKeys = new[] { "*" })]
        public async Task<IActionResult> GetAdmissionDetailsOfIdAndSlug(int? id, string? slugUrl)
        {
            var result = await _recruitmentService.GetRecruitmentDetailsOfIdAndSlug(id, slugUrl);
            if (result.Data == null) return NotFound(result);
            return Ok(result);
        }
    }

}
