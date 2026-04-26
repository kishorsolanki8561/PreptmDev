using CommonService.JWT;
using CommonService.Other;
using MasterMicroService.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelService.Model.MastersModel;
using static CommonService.Other.UtilityManager;

namespace MasterMicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class PageMasterController : ControllerBase
    {
        private readonly IPageMasterService _pageMasterService;
        public PageMasterController(IPageMasterService pageMasterService)
        {
            _pageMasterService = pageMasterService;
        }

        [ProducesResponseType(typeof(ServiceResponse<string>), 200)]
        [HttpPost]
        public object AddUpdate(PageMasterModel model)
        {
            ServiceResponse<int> obj = new ServiceResponse<int>();
            bool IsSuccess = false;
            var Data = UtilityManager.PostValidator<PageMasterModel>.IsValid(ref IsSuccess, model);
            try
            {
                if (!IsSuccess) return Data;
                return _pageMasterService.AddUpdate(model);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
   


        [HttpGet]
        public ServiceResponse<PageMasterModel> GetById(int Id)
        {
            ServiceResponse<PageMasterModel> obj = new ServiceResponse<PageMasterModel>();
            try
            {
                return _pageMasterService.GetById(Id);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
        [HttpGet]
        public ServiceResponse<bool> Delete(int Id)
        {
            ServiceResponse<bool> obj = new ServiceResponse<bool>();
            try
            {
                return _pageMasterService.Delete(Id);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
        [HttpGet]
        public ServiceResponse<bool> UpdateStatus(int Id)
        {
            ServiceResponse<bool> obj = new ServiceResponse<bool>();
            try
            {
                return _pageMasterService.UpdateStatus(Id);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
        [HttpPost]
        public ServiceResponse<List<PageMasterViewModel>> GetPagination(PageMasterFilterModel filterModel)
        {
            ServiceResponse<List<PageMasterViewModel>> obj = new ServiceResponse<List<PageMasterViewModel>>();
            try
            {
                return _pageMasterService.GetPagination(filterModel);

            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
        [HttpGet]
        public ServiceResponse<List<PagePermissionViewModel>> PagePermissionListByUserTypeCode(int userTypeCode)
        {
            ServiceResponse<List<PagePermissionViewModel>> obj = new ServiceResponse<List<PagePermissionViewModel>>();
            try
            {
                return _pageMasterService.PagePermissionList(userTypeCode);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
        [HttpPost]
        public ServiceResponse<bool> PageMasterPermissionModifiedById(List<int> model, int UserTypeCode)
        {
            ServiceResponse<bool> obj = new ServiceResponse<bool>();
            try
            {
                return _pageMasterService.PageMasterPermissionModifiedById(model, UserTypeCode);
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
