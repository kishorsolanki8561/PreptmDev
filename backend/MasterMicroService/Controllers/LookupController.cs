using CommonService.JWT;
using CommonService.Other;
using MasterMicroService.IService;
using MasterMicroService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelService.Model;
using ModelService.Model.MastersModel;
using static CommonService.Other.UtilityManager;

namespace MasterMicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class LookupController : ControllerBase
    {
        private readonly ILookupService _lookupService;
        public LookupController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }
        [ProducesResponseType(typeof(ServiceResponse<string>), 200)]
        [HttpPost]
        public ActionResult AddUpdate(LookupModel model)
        {
            return Ok(_lookupService.AddUpdate(model));
        }
        [HttpGet]
        public ActionResult GetById(int Id)
        {
            return Ok(_lookupService.GetById(Id));
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            return Ok(_lookupService.Delete(Id));
        }
        [HttpGet]
        public ActionResult UpdateStatus(int Id)
        {
            return Ok(_lookupService.UpdateStatus(Id));
        }
        [HttpPost]
        public ActionResult GetList(LookupFilterModel GetPagination)
        {
            return Ok(_lookupService.GetPagination(GetPagination));
        }

    }
}
