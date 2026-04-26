using FrontMicroService.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelService.Model.Translation;

namespace FrontMicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        private readonly IRecruitmentService _recruitmentService;
        private readonly ISchemeService _schemeService;

        public DashboardController(IDashboardService dashboardService, IRecruitmentService recruitmentService, ISchemeService schemeService)
        {
            _dashboardService = dashboardService;
            _recruitmentService = recruitmentService;
            _schemeService = schemeService;
        }

        [HttpGet]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, VaryByHeader = "lang", VaryByQueryKeys = new[] { "*" })]
        public object GetDashboardRecentAndPopularPostList(int pageSize)
        {
            return _dashboardService.GetDashboardRecentAndPopularPostList(pageSize);
        }

        [HttpPost]
        public async Task<object> GetFrontDashboardList(DashboradFilterModel filterModel)
        {
            return  await _dashboardService.GetFrontDashboardList(filterModel);
        }

        [HttpPost]
        public object GetDashboardSearchFilter(DashboradSearchFilterModel filterModel)
        {
            return _dashboardService.GetDashboardSearchFilter(filterModel);
        }

        [HttpGet]
        public IActionResult GetModuleWiseDataByIdAndSlug(int? id, string? slugUrl, bool isRecruitment = false)
        {
            var result = _recruitmentService.GetModuleWiseDataByIdAndSlug(id, slugUrl, isRecruitment);
            if (result.Data == null) return NotFound(result);
            return Ok(result);
        }

        [HttpGet]
        public object GetDepartmentDataByIdAndSlug(int? id, string? slugUrl)
        {
            return _recruitmentService.GetDepartmentDataByIdAndSlug(id, slugUrl);
        }

        [HttpGet]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, VaryByHeader = "lang", VaryByQueryKeys = new[] { "*" })]
        public async Task<object> GetDashboardData(int pageSize)
        {
            return await _dashboardService.GetDashboardData(pageSize);
        }
        [HttpGet]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, VaryByHeader = "lang", VaryByQueryKeys = new[] { "*" })]
        public ActionResult GetPopularBySearchText(int numberOfRecord = 10, string? SearchText = "")
        {
            return Ok(_dashboardService.GetPopularBySearchText(numberOfRecord, SearchText));
        }
        [HttpGet]
        [ResponseCache(Duration = 600, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "*" })]
        public ActionResult GetBanners(int numberOfRecord = 5)
        {
            return Ok(_dashboardService.GetBannersByPageSize(numberOfRecord));
        }

        [HttpGet("{langCode}")]
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "*" })]
        public IActionResult GetSiteMap([FromRoute] string langCode)
        {
            if (langCode == "hi" || langCode == "en")
            {
                string code = langCode == "en" ? "" : langCode;
                var result = _dashboardService.GetSiteMap(code);
                return new ContentResult
                {
                    ContentType = "application/xml",
                    Content = result.Data,
                    StatusCode = string.IsNullOrEmpty(result.Data) ? 204 : 200
                };
            }
            return BadRequest("Invalid language code. Use 'en' or 'hi'.");
        }

        [HttpGet]
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
        public IActionResult GetSiteMap()
        {
            var result = _dashboardService.GetSiteMap();
            return new ContentResult
            {
                ContentType = "application/xml",
                Content = result.Data,
                StatusCode = string.IsNullOrEmpty(result.Data) ? 204 : 200
            };
        }

        [HttpGet]
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
        public IActionResult GetSiteMapIndex()
        {
            var result = _dashboardService.GetSiteMapIndex();
            return new ContentResult
            {
                ContentType = "application/xml",
                Content = result.Data,
                StatusCode = 200
            };
        }
    }
}
