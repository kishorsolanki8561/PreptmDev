using MasterMicroService.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelService.Model;
using ModelService.Model.MastersModel;
using Newtonsoft.Json;
using static CommonService.Other.UtilityManager;

namespace MasterMicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        public readonly IBannerService _bannerService;
        public BannerController(IBannerService bannerService)
        {
            _bannerService = bannerService; 
        }
        [ProducesResponseType(typeof(ServiceResponse<string>), 200)]
        [HttpPost]
        public ActionResult AddUpdate(BannerModel model)
        {
            return Ok(_bannerService.AddUpdate(model));
        }
        [HttpGet]
        public ActionResult GetById(int Id)
        {
            return Ok(_bannerService.GetById(Id));
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            return Ok(_bannerService.Delete(Id));
        }
        [HttpGet]
        public ActionResult UpdateStatus(int Id)
        {
            return Ok(_bannerService.UpdateStatus(Id));
        }
        [HttpPost]
        public ActionResult GetList(BannerFilterModel GetPagination)
        {
            return Ok(_bannerService.GetPagination(GetPagination));
        }
    }
}
