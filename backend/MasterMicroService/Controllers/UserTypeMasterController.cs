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
    public class UserTypeMasterController : ControllerBase
    {
        private readonly IUserTypeMasterService  _userTypeMasterService;

        public UserTypeMasterController(IUserTypeMasterService userTypeMasterService)
        {
            _userTypeMasterService = userTypeMasterService;
        }
        [ProducesResponseType(typeof(ServiceResponse<string>), 200)]
        [HttpPost]
        public object AddUpdate(UserTypeMasterModel model)
        {
            ServiceResponse<int> obj = new ServiceResponse<int>();
            bool IsSuccess = false;
            var Data = UtilityManager.PostValidator<UserTypeMasterModel>.IsValid(ref IsSuccess, model);
            try
            {
                if (!IsSuccess) return Data;
                return _userTypeMasterService.AddUpdate(model);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
        [HttpGet]
        public ServiceResponse<UserTypeMasterModel> GetById(int Id)
        {
            ServiceResponse<UserTypeMasterModel> obj = new ServiceResponse<UserTypeMasterModel>();
            try
            {
                return _userTypeMasterService.GetById(Id);
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
                return _userTypeMasterService.Delete(Id);
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
                return _userTypeMasterService.UpdateStatus(Id);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }

        [HttpGet]
        public ServiceResponse<List<UserTypeMasterViewModel>> GetList()
        {
            ServiceResponse<List<UserTypeMasterViewModel>> obj = new ServiceResponse<List<UserTypeMasterViewModel>>();
            try
            {
                return _userTypeMasterService.GetList();

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
