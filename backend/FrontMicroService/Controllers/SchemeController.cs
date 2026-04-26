using FrontMicroService.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontMicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SchemeController : ControllerBase
    {
        private readonly ISchemeService _schemeService;
        public SchemeController(ISchemeService schemeService)
        {
            _schemeService = schemeService;
        }

        [HttpGet]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, VaryByHeader = "lang", VaryByQueryKeys = new[] { "*" })]
        public IActionResult GetSchemeDataByIdAndSlug(int? id, string? slugUrl)
        {
            var result = _schemeService.GetModuleWiseDataByIdAndSlug(id, slugUrl);
            if (result.Data == null) return NotFound(result);
            return Ok(result);
        }
    }
}
