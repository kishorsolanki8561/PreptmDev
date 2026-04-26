using CommonService.JWT;
using CommonService.Other;
using MasterService.IService;
using System.Net;
using AutoMapper;
using Dapper;
using MasterMicroService;
using ModelService.Model.Front;
using Newtonsoft.Json;
using Serilog;
using ModelService.Model.MastersModel;
using System.ComponentModel;

namespace MasterService.Service
{
    public class UserService : UtilityManager, IUserService
    {
        private readonly HelperService _helperService;
        private readonly JWTAuthManager _jWTAuthManager;
        private readonly ILoggerManager _logger;
        private readonly IMenuMasterService _menuMasterService;
        private IMapper Mapper { get; }
        public UserService(HelperService helperService, JWTAuthManager jWTAuthManager, IMapper mapper, ILoggerManager logger, IMenuMasterService menuMasterService)
        {
            _helperService = helperService;
            _jWTAuthManager = jWTAuthManager;
            Mapper = mapper;
            _logger = logger;
            _menuMasterService = menuMasterService;
        }

        public ServiceResponse<int> AddUpdate(UserMasterModel model)
        {
            try
            {

                if (model.Id == 0)
                {
                    model.Password = _helperService.GetPwHash(model.Password);
                    model.CreatedBy = _jWTAuthManager.User.Id;
                }
                else
                {
                    model.Password = _helperService.GetPwHash(model.Password);
                    model.ModifiedBy = _jWTAuthManager.User.Id;
                }
                string[] spKeys = { "Id", "Name", "Email", "Password", "UserTypeCode", "UserId" };
                string[] otherKeys = { "UserId" };
                var Result = Execute(QueriesPaths.UserMasterQueries.QAddUpdate, _helperService.AddDynamicParameters(model, spKeys, otherKeys));
                if (Result.IsSuccess)
                {
                    if (model.Id > 0)
                    {
                        return SetResultStatus<int>(Result.Data, MessageStatus.Update, Result.IsSuccess);
                    }
                    if (Result.IsSuccess && Result.Data == StatusCodes.Status409Conflict)
                    {
                        return SetResultStatus<int>(Result.Data, MessageStatus.EmailisExits, !Result.IsSuccess, "", "", 0, StatusCodes.Status409Conflict);
                    }
                    else
                    {
                        return SetResultStatus<int>(Result.Data, MessageStatus.Save, Result.IsSuccess);

                    }

                }
                else
                {
                    if (!Result.IsSuccess && Result.Data == StatusCodes.Status409Conflict)
                    {
                        return SetResultStatus<int>(Result.Data, MessageStatus.EmailisExits, Result.IsSuccess, "", "", 0, StatusCodes.Status409Conflict);
                    }
                    else
                    {
                        return SetResultStatus<int>(Result.Data, MessageStatus.Error, Result.IsSuccess);
                    }
                }


            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("UserService.cs", "AddUpdate"));
                return SetResultStatus<int>(0, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<bool> Delete(int Id)
        {
            try
            {
                var deleteResult = ExecuteReturnData(QueriesPaths.UserMasterQueries.QDelete, new { id = Id, @UserId = _jWTAuthManager.User.Id });
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
                Log.Error(ex, CommonFunction.Errorstring("UserService.cs", "Delete"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<UserViewModel> GetById(int Id)
        {
            try
            {
                var result = QueryFast<UserViewModel>(QueriesPaths.UserMasterQueries.QGetById, new { Id = Id });
                if (result.Data != null)
                {
                    return SetResultStatus<UserViewModel>(result.Data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<UserViewModel>(result.Data, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("UserService.cs", "GetById"));
                return SetResultStatus<UserViewModel>(null, MessageStatus.Error, false, ex.Message);
            }
        }

        public ServiceResponse<List<UserViewModel>> GetList()
        {
            try
            {
                var result = QueryList<UserViewModel>(@"select * from Vw_User", null);
                if (result.Data.Count > 0)
                {
                    return SetResultStatus<List<UserViewModel>>(result.Data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<List<UserViewModel>>(result.Data, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return SetResultStatus<List<UserViewModel>>(null, MessageStatus.Error, false, ex.Message);
            }
        }

        public ServiceResponse<List<UserMasterPaginationModel>> GetPagination(UserFilterModel filterModel)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Page", filterModel.Page > 0 ? filterModel.Page : 0);
                parameters.Add("@PageSize", filterModel.PageSize > 0 ? filterModel.PageSize : 0);
                parameters.Add("@OrderByAsc", filterModel.OrderByAsc);
                parameters.Add("@Search", !string.IsNullOrWhiteSpace(filterModel.Search) ? filterModel.Search : string.Empty);
                parameters.Add("@OrderBy", !string.IsNullOrWhiteSpace(filterModel.OrderBy) ? filterModel.OrderBy : "CreateDate");
                parameters.Add("@IsActive", filterModel.IsActive);
                parameters.Add("@Name", !string.IsNullOrWhiteSpace(filterModel.Name) ? filterModel.Name : string.Empty);
                parameters.Add("@Email", !string.IsNullOrWhiteSpace(filterModel.Email) ? filterModel.Email : string.Empty);
                parameters.Add("@FromDate", !string.IsNullOrWhiteSpace(filterModel.FromDate) ? filterModel.FromDate : string.Empty);
                parameters.Add("@ToDate", !string.IsNullOrWhiteSpace(filterModel.ToDate) ? filterModel.ToDate : string.Empty);


                var result = QueryList<UserMasterPaginationModel>(QueriesPaths.UserMasterQueries.QPagination, parameters);
                if (result.Data != null && result.Data.Count() > 0)
                {
                    return SetResultStatus<List<UserMasterPaginationModel>>(result.Data, MessageStatus.Success, true, "", "", result.Data[0].TotalRows);
                }
                else
                {
                    return SetResultStatus<List<UserMasterPaginationModel>>(null, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("UserService.cs", "GetPagination"));
                return SetResultStatus<List<UserMasterPaginationModel>>(null, MessageStatus.Error, false);
            }
        }
        public ServiceResponse<bool> UpdateStatus(int Id)
        {
            try
            {
                var deleteResult = ExecuteReturnData(QueriesPaths.UserMasterQueries.QUpdateStatus, new { id = Id, @UserId = _jWTAuthManager.User.Id });
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
                Log.Error(ex, CommonFunction.Errorstring("UserService.cs", "UpdateStatus"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }
        #region UserLogin
        public ServiceResponse<UserMasterViewModel> GetUserLogin(LoginModel model)
        {
            try
            {
                model.Password = _helperService.GetPwHash(model.Password);
                var result = QueryFast<UserMasterViewModel>(QueriesPaths.UserMasterQueries.QGetUserLogin, new { Email = model.Email, password = model.Password });//_helperService.GetPwHash(model.Password) });
                if (result.Data != null)
                {
                    result.Data.Token = _jWTAuthManager.GetJWT(result.Data);
                    if (result.Data != null && result.Data.UserTypeCode > 0)
                    {
                        result.Data.MenuList = _menuMasterService.GetDynamicMenuList(result.Data.UserTypeCode).Data;
                        var obj = new
                        {
                            UserTypeIdCode = result.Data.UserTypeCode
                        };
                        result.Data.PageComponents = QueryList<PageComponentActionByLoginModel>(QueriesPaths.UserMasterQueries.QagePermissionUrlsByUserType, _helperService.AddDynamicParameters(obj)).Data.ToList();
                    }
                    if (result.Data.IsAutoLoggedOut)
                    {
                        Execute(QueriesPaths.UserMasterQueries.QIsAutoLoggoutByUserId, new { IsAutoLoggedOut = false, Id = result.Data.Id }, null);
                    }
                    return SetResultStatus<UserMasterViewModel>(result.Data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<UserMasterViewModel>(null, MessageStatus.PSWDNOTMATCH, false, "", "", 0, StatusCodes.Status401Unauthorized);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("UserService.cs", "GetUserLogin"));
                return SetResultStatus<UserMasterViewModel>(null, MessageStatus.Error, false, ex.Message);
            }
        }
        #endregion

        #region Front Report
        public ServiceResponse<List<FrontUserListModel>> GetFrontUserReport(UserFilterModel filterModel)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Page", filterModel.Page > 0 ? filterModel.Page : 0);
                parameters.Add("@PageSize", filterModel.PageSize > 0 ? filterModel.PageSize : 0);
                parameters.Add("@OrderByAsc", filterModel.OrderByAsc);
                parameters.Add("@Search", !string.IsNullOrWhiteSpace(filterModel.Search) ? filterModel.Search : string.Empty);
                parameters.Add("@OrderBy", !string.IsNullOrWhiteSpace(filterModel.OrderBy) ? filterModel.OrderBy : "CreateDate");
                parameters.Add("@IsActive", filterModel.IsActive);
                parameters.Add("@Name", !string.IsNullOrWhiteSpace(filterModel.Name) ? filterModel.Name : string.Empty);
                parameters.Add("@Email", !string.IsNullOrWhiteSpace(filterModel.Email) ? filterModel.Email : string.Empty);
                parameters.Add("@FromDate", !string.IsNullOrWhiteSpace(filterModel.FromDate) ? filterModel.FromDate : string.Empty);
                parameters.Add("@ToDate", !string.IsNullOrWhiteSpace(filterModel.ToDate) ? filterModel.ToDate : string.Empty);


                var result = QueryList<FrontUserListModel>(@"Sp_FrontUserReport @Page,@PageSize,@Search,@OrderBy,@OrderByAsc,@IsActive,@Name,@Email,@FromDate,@ToDate", parameters);
                if (result.Data != null && result.Data.Count() > 0)
                {
                    return SetResultStatus<List<FrontUserListModel>>(result.Data, MessageStatus.Success, true, "", "", result.Data[0].TotalRows);
                }
                else
                {
                    return SetResultStatus<List<FrontUserListModel>>(null, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("UserService.cs", "GetFrontUserReport"));
                return SetResultStatus<List<FrontUserListModel>>(null, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<List<UserFeedbackViewListModel>> GetFrontUserFeedbackReport(UserFeedbackFilterModel filterModel)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Page", filterModel.Page > 0 ? filterModel.Page : 0);
                parameters.Add("@PageSize", filterModel.PageSize > 0 ? filterModel.PageSize : 0);
                parameters.Add("@OrderByAsc", filterModel.OrderByAsc);
                parameters.Add("@Search", !string.IsNullOrWhiteSpace(filterModel.Search) ? filterModel.Search : string.Empty);
                parameters.Add("@OrderBy", !string.IsNullOrWhiteSpace(filterModel.OrderBy) ? filterModel.OrderBy : "CreateDate");
                parameters.Add("@IsActive", filterModel.IsActive);
                parameters.Add("@FromDate", !string.IsNullOrWhiteSpace(filterModel.FromDate) ? filterModel.FromDate : string.Empty);
                parameters.Add("@ToDate", !string.IsNullOrWhiteSpace(filterModel.ToDate) ? filterModel.ToDate : string.Empty);
                parameters.Add("@Type", filterModel.Type > 0 ? filterModel.Type : 0);
                parameters.Add("@Status", filterModel.Status > 0 ? filterModel.Status : 0);

                var result = QueryList<UserFeedbackViewListModel>(@"Sp_FeedbackPagination @Page,@PageSize,@Search,@OrderBy,@OrderByAsc,@IsActive,@FromDate,@ToDate,@Type,@Status", parameters);
                if (result.Data != null && result.Data.Count() > 0)
                {
                    return SetResultStatus<List<UserFeedbackViewListModel>>(result.Data, MessageStatus.Success, true, "", "", result.Data[0].TotalRows);
                }
                else
                {
                    return SetResultStatus<List<UserFeedbackViewListModel>>(null, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("UserService.cs", "GetFrontUserFeedbackReport"));

                return SetResultStatus<List<UserFeedbackViewListModel>>(null, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<bool> UserFeedbackProgressStatus(int Id, int ProgressStatus)
        {
            try
            {
                if (Id > 0 && ProgressStatus > 0)
                {
                    var result = ExecuteReturnData(@"Sp_UserFeedbackDeleteUpdateStatus @Type='FeedbackStatus',@StatusCode=" + ProgressStatus + ",@Id=" + Id + ",@UserId=" + _jWTAuthManager.User.Id + "", null);
                    if (result.IsSuccess)
                    {
                        if (result.StatusCode == StatusCodes.Status200OK)
                            return SetResultStatus<bool>(true, MessageStatus.StatusProgressUpdate, true);

                        else if (result.StatusCode == StatusCodes.Status203NonAuthoritative)
                            return SetResultStatus<bool>(true, MessageStatus.StatusProgressNotValidCodeUpdate, true);
                        else
                            return SetResultStatus<bool>(false, MessageStatus.StatusProgressRequiredUpdate, false, "", null, StatusCodes.Status500InternalServerError);

                    }
                    else
                    {
                        return SetResultStatus<bool>(false, MessageStatus.StatusProgressRequiredUpdate, false, "", null, StatusCodes.Status500InternalServerError);
                    }

                }
                else
                {
                    return SetResultStatus<bool>(false, MessageStatus.StatusProgressRequiredUpdate, false, "", null, StatusCodes.Status500InternalServerError);
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("UserService.cs", "UserFeedbackProgressStatus"));
                return SetResultStatus<bool>(false, MessageStatus.StatusProgressRequiredUpdate, false, "", null, StatusCodes.Status500InternalServerError);
            }
        }
        #endregion
    }
}
