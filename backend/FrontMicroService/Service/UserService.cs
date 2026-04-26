using CommonService.JWT;
using CommonService.Other;
using FrontMicroService.IService;
using ModelService.CommonModel;
using ModelService.Model.Front;
using Serilog;

namespace FrontMicroService.Service
{
    public class UserService : UtilityManager, IUserService
    {
        private readonly HelperService _helperService;
        private readonly JWTAuthManager _jWTAuthManager;
        private readonly FileUploader _fileUploader;

        public UserService(HelperService helperService, JWTAuthManager jWTAuthManager, FileUploader fileUploader)
        {
            _helperService = helperService;
            _jWTAuthManager = jWTAuthManager;
            _fileUploader = fileUploader;
        }
        public ServiceResponse<FrontUserViewModel> AddUpdate(FrontUserModel model, IFormFile profile = null)
        {
            try
            {

                FrontUserViewModel userData = new FrontUserViewModel();
                var user = _jWTAuthManager.FrontUser;
                if (user != null) { model.Id = user.Id; }
                if (model.Id > 0)
                {
                    userData = GetById(true, (int)model.Id, string.Empty).Data;
                }

                var result = Execute(@"Sp_FrontUserAddUpdate @Id,@Name,@Email,@MobileNumber,@DateOfBirth,@ProfileImg,@StateId,@AuthToken,@Provider,@FCMToken,@Platform,@UId,@FirstName,@LastName,@Language", _helperService.AddDynamicParameters(model));
                if (!result.IsSuccess && result.StatusCode == StatusCodes.Status409Conflict)
                {
                    return SetResultStatus<FrontUserViewModel>(null, MessageStatus.EmailisExits, result.IsSuccess);
                }
                if (result.IsSuccess)
                {

                    //delete profile
                    if (model.Id > 0 && profile != null)
                    {
                        var ProfileImgDeletePath = userData.ProfileImg;
                        if (!string.IsNullOrEmpty(ProfileImgDeletePath))
                            _fileUploader.DeleteFile(ProfileImgDeletePath);
                    }
                    if (profile != null)
                    {
                        FileUploadModel file = new FileUploadModel();
                        var datetime = DateTime.UtcNow;
                        file.file = profile;
                        file.filename = (model.Id > 0 ? model.Id : result.Data) + "_" + datetime.Second.ToString() + ".webp"; //System.IO.Path.GetExtension(file.file.FileName); //file.file.
                        file.path = "FrontUser";
                        model.ProfileImg = _fileUploader.PostFile(file);
                        if (!string.IsNullOrEmpty(model.ProfileImg))
                        {
                            var ids = (model.Id > 0 ? model.Id : result.Data);
                            Execute(@"Update frontUser set ProfileImg=@ProfileImg where Id=@Id", new { ProfileImg = model.ProfileImg, Id = ids }, null);
                        }
                    }
                    userData = GetUser(true, (int)model.Id, string.Empty).Data;
                    return SetResultStatus<FrontUserViewModel>(userData, MessageStatus.Update, result.IsSuccess);

                }
                else
                {
                    return SetResultStatus<FrontUserViewModel>(userData, MessageStatus.Error, result.IsSuccess);
                }


            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("UserService.cs", "AddUpdate"));
                return SetResultStatus<FrontUserViewModel>(null, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<FrontUserViewModel> GetById(bool isPrivate = false, int Id = 0, string email = "", string UId = "")
        {
            try
            {
                var user = _jWTAuthManager.FrontUser;

                if (user is null && !isPrivate)
                {
                    return SetResultStatus<FrontUserViewModel>(null, MessageStatus.UnauthorizedUser, false, "", "", 0, StatusCodes.Status401Unauthorized);
                }
                else
                {
                    Id = user is null ? Id : user.Id;
                    var result = QueryFast<FrontUserViewModel>(@"select  * from [FrontUser] where IsDelete = 0 And IsActive= 1 AND (Id =@Id AND @Id >0) OR (Email=@email AND @email <> '') OR (UId=@UId AND @UId <> '')", new { Id = Id, email = email, UId = UId });
                    if (result.Data != null)
                    {
                        if (!string.IsNullOrEmpty(result.Data.ProfileImg))
                        {
                            result.Data.ProfileImg = isPrivate ? result.Data.ProfileImg : result.Data.ProfileImg.ToAbsolutepathPath();
                        }
                        return SetResultStatus<FrontUserViewModel>(result.Data, MessageStatus.Success, true);
                    }
                    else
                    {
                        return SetResultStatus<FrontUserViewModel>(null, MessageStatus.NoRecord, true);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("UserService.cs", "GetById"));
                return SetResultStatus<FrontUserViewModel>(null, MessageStatus.Error, false, ex.Message);
            }
        }


        public ServiceResponse<FrontUserViewModel> GetUser(bool isPrivate = false, int Id = 0, string email = "", string UId = "")
        {
            try
            {
                var user = _jWTAuthManager.FrontUser;

                if (user is null && !isPrivate)
                {
                    return SetResultStatus<FrontUserViewModel>(null, MessageStatus.UnauthorizedUser, false, "", "", 0, StatusCodes.Status401Unauthorized);
                }
                else
                {
                    Id = user is null ? Id : user.Id;
                    var result = QueryFast<FrontUserViewModel>(@"select  * from [FrontUser] where IsDelete = 0 And IsActive= 1 AND (Id =@Id AND @Id >0) OR (Email=@email AND @email <> '') OR (UId=@UId AND @UId <> '')", new { Id = Id, email = email, UId = UId });
                    if (result.Data != null)
                    {
                        if (!string.IsNullOrEmpty(result.Data.ProfileImg))
                        {
                            result.Data.ProfileImg = result.Data.ProfileImg.ToAbsolutepathPath();
                        }
                        return SetResultStatus<FrontUserViewModel>(result.Data, MessageStatus.Success, true);
                    }
                    else
                    {
                        return SetResultStatus<FrontUserViewModel>(null, MessageStatus.NoRecord, true);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("UserService.cs", "GetUser"));
                return SetResultStatus<FrontUserViewModel>(null, MessageStatus.Error, false, ex.Message);
            }
        }

        public ServiceResponse<bool> UserDeactivate(int Id)
        {
            try
            {
                var deleteResult = ExecuteReturnData(@"update FrontUser set IsActive=0 where Id=@Id", new { id = Id });
                if (deleteResult.IsSuccess)
                {
                    if (deleteResult.StatusCode == StatusCodes.Status200OK)
                        return SetResultStatus<bool>(true, MessageStatus.StatusUpdate, true);
                    else if (deleteResult.StatusCode == StatusCodes.Status404NotFound)
                        return SetResultStatus<bool>(false, MessageStatus.NoRecord, false);
                    else
                        return SetResultStatus<bool>(false, MessageStatus.Error, true);
                }
                else
                {
                    return SetResultStatus<bool>(false, MessageStatus.Error, false);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("UserService.cs", "UserDeactivate"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<bool> UserDelete(int Id)
        {
            try
            {
                var deleteResult = ExecuteReturnData(@"update FrontUser set IsDelete=0 where Id=@Id", new { id = Id });
                if (deleteResult.IsSuccess)
                {
                    if (deleteResult.StatusCode == StatusCodes.Status200OK)
                        return SetResultStatus<bool>(true, MessageStatus.Delete, true);
                    else if (deleteResult.StatusCode == StatusCodes.Status404NotFound)
                        return SetResultStatus<bool>(false, MessageStatus.NoRecord, false);
                    else
                        return SetResultStatus<bool>(false, MessageStatus.Error, true);
                }
                else
                {
                    return SetResultStatus<bool>(false, MessageStatus.Error, false);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("UserService.cs", "UserDelete"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<FrontUserViewModel> GetUserLogin(FrontUserLoginModel model)
        {
            ServiceResponse<FrontUserViewModel> serviceResponse = new ServiceResponse<FrontUserViewModel>();
            try
            {
                var userResult = GetUser(true, 0, model.Email, model.UId);
                if (userResult.IsSuccess && userResult.Data != null)
                {
                    serviceResponse.Data = userResult.Data;
                    serviceResponse.Data.Token = _jWTAuthManager.FrontGetJWT(userResult.Data);
                    return SetResultStatus<FrontUserViewModel>(serviceResponse.Data, MessageStatus.Login, true);
                    //return serviceResponse;
                }
                else
                {
                    ServiceResponse<FrontUserViewModel> newUser = new ServiceResponse<FrontUserViewModel>();
                    FrontUserModel user = new FrontUserModel();
                    user.Id = 0;
                    user.Email = model.Email;
                    user.Name = model.Name + model.FirstName + " " + model.LastName;
                    user.Platform = model.Platform;
                    user.AuthToken = model.AuthToken;
                    user.Language = model.Language;
                    user.Provider = model.Provider;
                    user.MobileNumber = model.MobileNumber;
                    user.Platform = model.Platform;
                    user.UId = model.UId;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Language = model.Language;
                    if (!string.IsNullOrWhiteSpace(model.ProfileImg))
                    {
                        var stream = getProfileByGoogle(model.ProfileImg);
                        IFormFile file = new FormFile(stream, 0, stream.Length, user.FirstName, user.FirstName + ".png");
                        newUser = AddUpdate(user, file);
                    }
                    else
                    {
                        newUser = AddUpdate(user);
                    }


                    if (newUser.IsSuccess)
                    {
                        userResult = GetUser(true, 0, model.Email);
                        if (userResult.IsSuccess && userResult.Data != null)
                        {
                            serviceResponse.Data = userResult.Data;
                            serviceResponse.Data.Token = _jWTAuthManager.FrontGetJWT(userResult.Data);
                            return SetResultStatus<FrontUserViewModel>(serviceResponse.Data, MessageStatus.Login, true);
                        }
                        return SetResultStatus<FrontUserViewModel>(null, MessageStatus.UserNOTMAP, false, "");
                    }
                }
                return serviceResponse;
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("UserService.cs", "GetUserLogin"));
                return SetResultStatus<FrontUserViewModel>(null, MessageStatus.Error, false, "");
            }
        }

        public ServiceResponse<bool> UpdateFCMToken(string fcmToken)
        {
            if (!string.IsNullOrEmpty(fcmToken))
            {
                try
                {
                    var user = _jWTAuthManager.FrontUser;
                    if (user != null)
                    {
                        var userData = GetById(true, (int)user.Id, string.Empty).Data;
                        if (userData is null)
                        {
                            return SetResultStatus<bool>(false, MessageStatus.NotExist, false, "", "", 0, StatusCodes.Status404NotFound);
                        }
                        else
                        {
                            var result = Execute(@"Update frontUser set FCMToken=@FCMToken where Id=@Id", new { FCMToken = fcmToken, Id = user.Id }, null);
                            if (result.IsSuccess)
                            {
                                return SetResultStatus<bool>(result.IsSuccess, MessageStatus.Update, result.IsSuccess, "", "", 0, StatusCodes.Status200OK);
                            }
                            return SetResultStatus<bool>(false, MessageStatus.DataBaseError, false, "", "", 0, StatusCodes.Status409Conflict);
                        }
                    }
                    else
                    {
                        return SetResultStatus<bool>(false, MessageStatus.Update, false, "", "", 0, StatusCodes.Status401Unauthorized);
                    }

                }
                catch (Exception ex)
                {
                    Log.Error(ex, CommonFunction.Errorstring("UserService.cs", "UpdateFCMToken"));
                    return SetResultStatus<bool>(false, MessageStatus.Error, false, "", "", 0, StatusCodes.Status500InternalServerError);
                }
            }
            else
            {
                return SetResultStatus<bool>(false, MessageStatus.FCMToken, false, "", "", 0, StatusCodes.Status400BadRequest);
            }
        }

        private MemoryStream getProfileByGoogle(string url)
        {
            try
            {
                return _fileUploader.PostGoogleFile(url);
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("UserService.cs", "UpdateFCMToken"));
                return null;
            }
        }
    }
}
