using CommonService.JWT;
using CommonService.Other;
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
    public class AdditionalPagesController : ControllerBase
    {
        private readonly IAdditionalPagesService _additionalPagesService;
        public AdditionalPagesController(IAdditionalPagesService additionalPagesService)
        {
                _additionalPagesService = additionalPagesService;
        }
        [ProducesResponseType(typeof(ServiceResponse<string>), 200)]
        [HttpPost]
        public object AddUpdate(AdditionalPagesModel model)
        {
            ServiceResponse<int> obj = new ServiceResponse<int>();
            bool IsSuccess = false;
            var Data = UtilityManager.PostValidator<AdditionalPagesModel>.IsValid(ref IsSuccess, model);
            try
            {
                if (!IsSuccess) return Data;
                return _additionalPagesService.AddUpdate(model);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
        [HttpGet]
        public ServiceResponse<AdditionalPagesViewModel> GetById(int Id)
        {
            ServiceResponse<AdditionalPagesViewModel> obj = new ServiceResponse<AdditionalPagesViewModel>();
            try
            {
                return _additionalPagesService.GetById(Id);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
        [HttpGet]
        public ServiceResponse<List<AdditionalPagesListModel>> GetList()
        {
            ServiceResponse<List<AdditionalPagesListModel>> obj = new ServiceResponse<List<AdditionalPagesListModel>>();
            try
            {
                return _additionalPagesService.GetList();

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
