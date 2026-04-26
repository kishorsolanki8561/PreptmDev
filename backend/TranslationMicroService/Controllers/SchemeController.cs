using CommonService.JWT;
using CommonService.Other;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelService.Model.MastersModel;
using ModelService.Model.Translation;
using Newtonsoft.Json;
using Serilog;
using TranslationMicroService.IService;
using static CommonService.Other.UtilityManager;

namespace TranslationMicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class SchemeController : ControllerBase
    {
        private readonly ISchemeService _schemeService;
        public SchemeController(ISchemeService schemeService)
        {
            _schemeService = schemeService;
        }

        [ProducesResponseType(typeof(ServiceResponse<SchemeRequestModel>), 200)]
        [HttpPost]
        public ActionResult AddUpdate(SchemeRequestModel model)
        {
            try
            {
                //SchemeRequestModel rwqmodel = JsonConvert.DeserializeObject<SchemeRequestModel>(model);
                //if (ThumbnailFile != null)
                //{
                //    rwqmodel.ThumbnailFile = ThumbnailFile;
                //}
                //if (rwqmodel.Id == 0 && files != null && rwqmodel.SchemeAttachmentLookups.Count() == files.Count())
                //{
                //    for (int i = 0; i < files.Count; i++)
                //    {
                //        rwqmodel.SchemeAttachmentLookups[i].AttchPath = files[i];
                //    }
                //}
                //else
                //{
                //    for (int i = 0; i < rwqmodel.SchemeAttachmentLookups.Count; i++)
                //    {
                //        var index = rwqmodel.SchemeAttachmentLookups[i].IndexNumber;

                //        if (index.HasValue && (files is not null && files.Count() > 0))
                //        {
                //            rwqmodel.SchemeAttachmentLookups[i].AttchPath = files[(int)index];
                //        }
                //    }
                //}
                return Ok(_schemeService.AddUpdate(model));

            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("SchemeService.cs", "AddUpDate"));
                return Conflict(ex.Message);
            }
        }
        [HttpGet]
        public ActionResult GetById(int Id)
        {
            return Ok(_schemeService.GetById(Id));
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            return Ok(_schemeService.Delete(Id));
        }
        [HttpGet]
        public ActionResult UpdateStatus(int Id)
        {
            return Ok(_schemeService.UpdateStatus(Id));
        }
        [HttpPost]
        public ActionResult GetList(SchemeFilterModel GetPagination)
        {
            return Ok(_schemeService.GetPagination(GetPagination));
        }

        [HttpGet]
        public ActionResult SchemeProgressStatus(int Id, int ProgressStatus)
        {
            return Ok(_schemeService.SchemeProgressStatus(Id, ProgressStatus));
        }

        [HttpGet]
        public ServiceResponse<List<SchemeTitleCheckModel>> CheckSchemeTitle(string SearchText)
        {
            ServiceResponse<List<SchemeTitleCheckModel>> obj = new ServiceResponse<List<SchemeTitleCheckModel>>();
            try
            {
                return _schemeService.CheckSchemeTitle(SearchText);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
    }
}
