using CommonService.JWT;
using CommonService.Other;
using MasterService.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelService.Model.MastersModel;
using static CommonService.Other.UtilityManager;


namespace MasterService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _UserSerivce;
        private readonly JWTAuthManager _jWTAuthManager;
        public AccountController(IUserService userService, JWTAuthManager jWTAuthManager)
        {
            _UserSerivce = userService;
            _jWTAuthManager = jWTAuthManager;
        }
        [ProducesResponseType(typeof(ServiceResponse<string>), 200)]
        [HttpPost]
        public object Login(LoginModel model)
        {
            ServiceResponse<int> obj = new ServiceResponse<int>();
            bool IsSuccess = false;
            var Data = UtilityManager.PostValidator<LoginModel>.IsValid(ref IsSuccess, model);
                ServiceResponse<UserMasterViewModel> objUser = new ServiceResponse<UserMasterViewModel>();
            try
            {
                if (!IsSuccess) return Data;
                objUser = _UserSerivce.GetUserLogin(model);
                if (objUser.IsSuccess)
                {
                    //objUser.Data.Token = _jWTAuthManager.GetJWT(objUser.Data);
                    return objUser;
                }
                else
                {
                    obj.Exception = objUser.Exception;
                    obj.Message = objUser.Message;
                    obj.StatusCode = StatusCodes.Status401Unauthorized;
                    obj.IsSuccess = false;
                    return obj;
                }
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                obj.IsSuccess = false;
                return obj;
            }
        }
    }
}
