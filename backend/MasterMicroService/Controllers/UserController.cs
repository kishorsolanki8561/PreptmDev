using CommonService.JWT;
using CommonService.Other;
using MasterService.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelService.Model.Front;
using ModelService.Model.MastersModel;
using static CommonService.Other.UtilityManager;


namespace MasterService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _UserSerivce;
        public UserController(IUserService userService)
        {
            _UserSerivce = userService;
        }

        [ProducesResponseType(typeof(ServiceResponse<string>), 200)]
        [HttpPost]
        public object AddUpDate(UserMasterModel model)
        {
            ServiceResponse<int> obj = new ServiceResponse<int>();
            bool IsSuccess = false;
            var Data = UtilityManager.PostValidator<UserMasterModel>.IsValid(ref IsSuccess, model);
            try
            {
                if (!IsSuccess) return Data;
                return _UserSerivce.AddUpdate(model);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
        [HttpGet]
        public ServiceResponse<UserViewModel> GetById(int Id)
        {
            ServiceResponse<UserViewModel> obj = new ServiceResponse<UserViewModel>();
            try
            {
                return _UserSerivce.GetById(Id);
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
                return _UserSerivce.Delete(Id);
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
                return _UserSerivce.UpdateStatus(Id);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
        [HttpGet]
        public ServiceResponse<List<UserViewModel>> GetList()
        {
            ServiceResponse<List<UserViewModel>> obj = new ServiceResponse<List<UserViewModel>>();
            try
            {
                return _UserSerivce.GetList();

            }
            catch (Exception ex)
            {
                obj.Data = null;
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                obj.IsSuccess = false;
                return obj;
            }
        }
        [HttpPost]
        public ServiceResponse<List<UserMasterPaginationModel>> GetPagination(UserFilterModel filterModel)
        {
            ServiceResponse<List<UserMasterPaginationModel>> obj = new ServiceResponse<List<UserMasterPaginationModel>>();
            try
            {
                return _UserSerivce.GetPagination(filterModel);

            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
        [HttpPost]
        public ServiceResponse<List<FrontUserListModel>> GetFrontUserReport(UserFilterModel filterModel)
        {
            ServiceResponse<List<FrontUserListModel>> obj = new ServiceResponse<List<FrontUserListModel>>();
            try
            {
                return _UserSerivce.GetFrontUserReport(filterModel);

            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }

        [HttpPost]
        public ServiceResponse<List<UserFeedbackViewListModel>> GetFrontUserFeedbackReport(UserFeedbackFilterModel filterModel)
        {
            ServiceResponse<List<UserFeedbackViewListModel>> obj = new ServiceResponse<List<UserFeedbackViewListModel>>();
            try
            {
                return _UserSerivce.GetFrontUserFeedbackReport(filterModel);

            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
        [HttpGet]
        public ServiceResponse<bool> UserFeedbackProgressStatus(int Id, int ProgressStatus)
        {
            ServiceResponse<bool> obj = new ServiceResponse<bool>();
            try
            {
                return _UserSerivce.UserFeedbackProgressStatus(Id, ProgressStatus);
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
