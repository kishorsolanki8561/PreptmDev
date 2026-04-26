using CommonService.JWT;
using CommonService.Other;
using FrontMicroService.IService;
using FrontMicroService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelService.Model;
using ModelService.Model.Front;
using Newtonsoft.Json;
using static CommonService.Other.UtilityManager;

namespace FrontMicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IBookmarkService _bookmarkService;
        private readonly IUserFeedbackService _userFeedbackService;
        public UserController(IUserService userService, IBookmarkService bookmarkService, IUserFeedbackService userFeedbackService)
        {
            _userService = userService;
            _bookmarkService = bookmarkService;
            _userFeedbackService = userFeedbackService;
        }

        [ProducesResponseType(typeof(ServiceResponse<string>), 200)]
        [HttpPost]
        public object Login(FrontUserLoginModel model)
        {
            bool IsSuccess = false;
            var Data = UtilityManager.PostValidator<FrontUserLoginModel>.IsValid(ref IsSuccess, model);
            ServiceResponse<FrontUserViewModel> objUser = new ServiceResponse<FrontUserViewModel>();
            try
            {
                if (!IsSuccess) return Data;
                return _userService.GetUserLogin(model);

            }
            catch (Exception ex)
            {
                objUser.Exception = ex.Message;
                objUser.StatusCode = StatusCodes.Status500InternalServerError;
                objUser.IsSuccess = false;
                return objUser;
            }
        }
        [Authorize]
        [ProducesResponseType(typeof(ServiceResponse<string>), 200)]
        [HttpPost]
        public object UpdateUser([FromForm(Name = "Data")] string model, IFormFile? profile)
        {
            FrontUserModel userModel = new FrontUserModel();
            ServiceResponse<int> obj = new ServiceResponse<int>();
            bool IsSuccess = false;
            var Data = UtilityManager.PostValidator<FrontUserModel>.IsValid(ref IsSuccess, JsonConvert.DeserializeObject<FrontUserModel>(model));
            try
            {
                if (!IsSuccess) return Data;
                userModel = JsonConvert.DeserializeObject<FrontUserModel>(model);
                return _userService.AddUpdate(userModel, profile);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }

        [Authorize]
        [HttpGet]
        public ServiceResponse<FrontUserViewModel> GetUserDetail()
        {
            ServiceResponse<FrontUserViewModel> obj = new ServiceResponse<FrontUserViewModel>();
            try
            {
                return _userService.GetById();
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }

        [Authorize]
        [HttpGet]
        public ServiceResponse<bool> Delete(int Id)
        {
            ServiceResponse<bool> obj = new ServiceResponse<bool>();
            try
            {
                return _userService.UserDelete(Id);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }

        [Authorize]
        [ProducesResponseType(typeof(ServiceResponse<object>), 200)]
        [HttpGet]
        public object AddRemoveBookmark(int BookmarkId = 0, int PostId = 0, int BlockTypeId = 0)
        {
            ServiceResponse<bool> obj = new ServiceResponse<bool>();
            bool IsSuccess = false;
            var Data = UtilityManager.PostValidator<BookmarkModel>.IsValid(ref IsSuccess, new BookmarkModel() { Id = BookmarkId, PostId = PostId, ModuleEnum = BlockTypeId });
            try
            {
                if (!IsSuccess) return Data;
                if (BookmarkId > 0 && PostId == 0 && BlockTypeId == 0)
                    return _bookmarkService.AddRemoveBookmark(new BookmarkModel() { Id = BookmarkId, PostId = 0, ModuleEnum = 0 });
                else if (BookmarkId == 0 && PostId > 0 && BlockTypeId > 0)
                    return _bookmarkService.AddRemoveBookmark(new BookmarkModel() { Id = BookmarkId, PostId = PostId, ModuleEnum = BlockTypeId });
                else
                {
                    obj.Exception = "ids is not match";
                    obj.StatusCode = StatusCodes.Status200OK;
                    return obj;
                }
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }

        //[Authorize]
        //[ProducesResponseType(typeof(ServiceResponse<object>), 200)]
        //[HttpGet]
        //public object AddRemoveBookmark(int BookmarkId)
        //{
        //    ServiceResponse<bool> obj = new ServiceResponse<bool>();
        //    bool IsSuccess = false;
        //    var Data = UtilityManager.PostValidator<BookmarkModel>.IsValid(ref IsSuccess, new BookmarkModel() { Id = BookmarkId, PostId = 0, ModuleEnum = 0 });
        //    try
        //    {
        //        if (!IsSuccess) return Data;
        //        return _bookmarkService.AddRemoveBookmark(new BookmarkModel() { Id = BookmarkId, PostId = 0, ModuleEnum = 0 });
        //    }
        //    catch (Exception ex)
        //    {
        //        obj.Exception = ex.Message;
        //        obj.StatusCode = StatusCodes.Status500InternalServerError;
        //        return obj;
        //    }
        //}

        [Authorize]
        [ProducesResponseType(typeof(ServiceResponse<string>), 200)]
        [HttpPost]
        public object AddUserFeedback(UserFeedbackModel model)
        {
            ServiceResponse<bool> obj = new ServiceResponse<bool>();
            bool IsSuccess = false;
            var Data = UtilityManager.PostValidator<UserFeedbackModel>.IsValid(ref IsSuccess, model);
            try
            {
                if (!IsSuccess) return Data;
                return _userFeedbackService.AddUpdate(model);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
        [Authorize]
        [HttpPost]
        public object GetBookmarkPostList(BookmarkSearchFilterModel filterModel)
        {
            return _bookmarkService.GetBookmarkPostListByUser(filterModel);
        }

        [Authorize]
        [HttpGet]
        public object UpdateFCMToken(string fcmToken)
        {
            return _userService.UpdateFCMToken(fcmToken);
        }

    }
}
