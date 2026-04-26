using FrontMicroService.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontMicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlockContentController : ControllerBase
    {
        private readonly IBlockContentService _blockContentService;
        public BlockContentController(IBlockContentService blockContentService)
        {
            _blockContentService = blockContentService;
        }
        [HttpGet]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, VaryByHeader = "lang", VaryByQueryKeys = new[] { "*" })]
        public IActionResult GetBlockContentDetailsOfIdAndSlug(int? id, string? slugUrl)
        {
            var result = _blockContentService.GetBlockContentDetailsOfIdAndSlug(id, slugUrl);
            if (result.Data == null) return NotFound(result);
            return Ok(result);
        }
    }
}
