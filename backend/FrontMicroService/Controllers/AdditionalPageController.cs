using CommonService.Other;
using FrontMicroService.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelService.Model;
using static CommonService.Other.UtilityManager;

namespace FrontMicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdditionalPageController : ControllerBase
    {
        private readonly IAdditionalPagesService _additionalPagesService;
        private readonly HelperService _helperService;

        public AdditionalPageController(IAdditionalPagesService additionalPagesService, HelperService helperService)
        {
            _additionalPagesService = additionalPagesService;
            _helperService = helperService;
        }

        [HttpGet]
        public ServiceResponse<string> GetAdditionalPagesByPageType(int Id)
        {
            ServiceResponse<string> obj = new ServiceResponse<string>();
            try
            {
                return _additionalPagesService.GetAdditionalPagesByPageType(Id);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }

        [HttpGet]
        public string Encrypt(string dd)
        {
            return _helperService.Encrypt(dd);
        }
        [HttpGet]
        public string Decrypt(string dd)
        {
            return _helperService.Decrypt(dd);
        }
    }
}
