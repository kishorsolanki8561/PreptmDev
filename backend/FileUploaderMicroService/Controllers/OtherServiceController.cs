using CommonService.Other;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileUploaderMicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OtherServiceController : ControllerBase
    {
        private readonly HelperService _helperService;
        public OtherServiceController(HelperService helperService) 
        {
            _helperService = helperService;
        }

        [HttpGet]
        public async Task<string> GetAccessTokenAsync()
        {
            return await _helperService.GetAccessTokenAsync();
        }
    }
}
