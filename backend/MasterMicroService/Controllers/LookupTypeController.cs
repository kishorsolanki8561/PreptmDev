using CommonService.JWT;
using MasterMicroService.IService;
using MasterMicroService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelService.Model.MastersModel;
using static CommonService.Other.UtilityManager;

namespace MasterMicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class LookupTypeController : ControllerBase
    {
        private readonly ILookupTypeService _lookupTypeService;
        public LookupTypeController(ILookupTypeService lookupTypeService)
        {
            _lookupTypeService = lookupTypeService;
        }
        [ProducesResponseType(typeof(ServiceResponse<string>), 200)]
        [HttpPost]
        public ActionResult AddUpdate(LookupTypeModel model)
        {
            return Ok(_lookupTypeService.AddUpdate(model));
        }
        [HttpGet]
        public ActionResult GetById(int Id)
        {
            return Ok(_lookupTypeService.GetById(Id));
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            return Ok(_lookupTypeService.Delete(Id));
        }
        [HttpGet]
        public ActionResult UpdateStatus(int Id)
        {
            return Ok(_lookupTypeService.UpdateStatus(Id));
        }

        [HttpPost]
        public ActionResult GetList(LookupTypeFilterModel GetPagination)
        {
            return Ok(_lookupTypeService.GetPagination(GetPagination));
        }
    }
}
