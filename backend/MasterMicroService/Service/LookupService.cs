using CommonService.JWT;
using CommonService.Other;
using MasterMicroService.IService;
using ModelService.Model.MastersModel;
using Serilog;

namespace MasterMicroService.Service
{
    public class LookupService: UtilityManager,ILookupService
    {
        private readonly HelperService _helperService;
        private readonly JWTAuthManager _jWTAuthManager;
        private readonly ILoggerManager _logger;
        public LookupService(HelperService helperService, JWTAuthManager jWTAuthManager, ILoggerManager logger)
        {
            _jWTAuthManager = jWTAuthManager;
            _logger = logger;
            _helperService = helperService;
        }

        public ServiceResponse<int> AddUpdate(LookupModel model)
        {
            try
            {
                ServiceResponse<int> response = new ServiceResponse<int>();
                model.UserId = _jWTAuthManager.User.Id;
                if (!string.IsNullOrEmpty(model.Slug))
                    model.Slug = SlugHelper.ToUrlSlug(model.Slug);

                var result = Execute(QueriesPaths.LookupQueries.QAddUpdate, _helperService.AddDynamicParameters(model), null);
                if (!result.IsSuccess && result.StatusCode == StatusCodes.Status409Conflict)
                {
                    response = SetResultStatus<int>(result.Data, MessageStatus.LookupExist, result.IsSuccess, "", "", 0, StatusCodes.Status409Conflict, null);
                }
                else if (!result.IsSuccess && result.StatusCode == 408)
                {
                    response = SetResultStatus<int>(result.Data, MessageStatus.SlugUrlExist, result.IsSuccess, "", "", 0, StatusCodes.Status408RequestTimeout, null);
                }
                else if (result.IsSuccess)
                {
                    if (result.StatusCode == StatusCodes.Status200OK)
                    {
                        if (model.Id > 0)
                            response = SetResultStatus<int>(result.Data, MessageStatus.Update, result.IsSuccess, "", "", 0, StatusCodes.Status200OK, null);
                        else
                            response = SetResultStatus<int>(result.Data, MessageStatus.Save, result.IsSuccess, "", "", 0, StatusCodes.Status200OK, null);
                    }
                    return response;
                }
                else
                {
                    response = SetResultStatus<int>(result.Data, MessageStatus.Error, result.IsSuccess, "", "", 0, StatusCodes.Status500InternalServerError, null);
                }
                return response;
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("LookupService.cs", "AddUpdate"));
                return SetResultStatus<int>(0, MessageStatus.Error, false, ex.Message, "", 0, StatusCodes.Status500InternalServerError, null);
            }
        }

        public ServiceResponse<bool> Delete(int Id)
        {
            try
            {
                var deleteResult = ExecuteReturnData(QueriesPaths.LookupQueries.QDelete, new { Id = Id, UserId = _jWTAuthManager.User.Id });
                if (deleteResult.IsSuccess)
                {
                    if (deleteResult.StatusCode == StatusCodes.Status200OK)
                        return SetResultStatus<bool>(deleteResult.IsSuccess, MessageStatus.Delete, deleteResult.IsSuccess, "", "", 0, StatusCodes.Status200OK, null);
                    else if (deleteResult.StatusCode == StatusCodes.Status404NotFound)
                        return SetResultStatus<bool>(deleteResult.IsSuccess, MessageStatus.NoRecord, !deleteResult.IsSuccess, "", "", 0, StatusCodes.Status404NotFound, null);
                    else
                        return SetResultStatus<bool>(!deleteResult.IsSuccess, MessageStatus.Error, !deleteResult.IsSuccess, "", "", 0, StatusCodes.Status500InternalServerError, null);
                }
                else
                {
                    return SetResultStatus<bool>(!deleteResult.IsSuccess, MessageStatus.Error, !deleteResult.IsSuccess, "", "", 0, StatusCodes.Status500InternalServerError, null);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return SetResultStatus<bool>(true, MessageStatus.Error, false, "", "", 0, StatusCodes.Status500InternalServerError, null);
            }
        }

        public ServiceResponse<LookupViewModel> GetById(int Id)
        {
            try
            {
                var result = QueryFast<LookupViewModel>(QueriesPaths.LookupQueries.QGetById, new { Id = Id });
                if (result.Data != null)
                {
                    return SetResultStatus<LookupViewModel>(result.Data, MessageStatus.Success, result.IsSuccess, "", "", 0, StatusCodes.Status200OK, null);
                }
                else
                {
                    return SetResultStatus<LookupViewModel>(result.Data, MessageStatus.NoRecord, result.IsSuccess, "", "", 0, StatusCodes.Status404NotFound, null);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("LookupService.cs", "GetById"));
                return SetResultStatus<LookupViewModel>(null, MessageStatus.NoRecord, false, ex.Message, ex.InnerException.Message, 0, StatusCodes.Status500InternalServerError, null);
            }
        }

        public ServiceResponse<List<LookupViewListModel>> GetPagination(LookupFilterModel filterModel)
        {
            try
            {
                var result = QueryList<LookupViewListModel>(QueriesPaths.LookupQueries.QPagination, _helperService.AddDynamicParameters(filterModel));
                if (result.Data != null && result.Data.Count() > 0)
                {
                    return SetResultStatus<List<LookupViewListModel>>(result.Data, MessageStatus.Success, result.IsSuccess, "", "", result.Data[0].TotalRows, StatusCodes.Status200OK, null);
                }
                else
                {
                    return SetResultStatus<List<LookupViewListModel>>(null, MessageStatus.NoRecord, result.IsSuccess, "", "", 0, StatusCodes.Status404NotFound, null);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("LookupService.cs", "GetPagination"));
                return SetResultStatus<List<LookupViewListModel>>(null, MessageStatus.Error, false, ex.Message, ex.InnerException.Message, 0, StatusCodes.Status500InternalServerError, null);
            }
        }

        public ServiceResponse<bool> UpdateStatus(int Id)
        {
            try
            {
                var result = ExecuteReturnData(QueriesPaths.LookupQueries.QUpdateStatus, new { Id = Id, UserId = _jWTAuthManager.User.Id });
                if (result.IsSuccess)
                {
                    if (result.StatusCode == StatusCodes.Status200OK)
                        return SetResultStatus<bool>(result.IsSuccess, MessageStatus.StatusUpdate, result.IsSuccess, "", "", 0, StatusCodes.Status200OK, null);
                    else if (result.StatusCode == StatusCodes.Status404NotFound)
                        return SetResultStatus<bool>(result.IsSuccess, MessageStatus.NoRecord, result.IsSuccess, "", "", 0, StatusCodes.Status404NotFound, null);
                    else
                        return SetResultStatus<bool>(!result.IsSuccess, MessageStatus.Error, !result.IsSuccess, "", "", 0, StatusCodes.Status500InternalServerError, null);
                }
                else
                {
                    return SetResultStatus<bool>(!result.IsSuccess, MessageStatus.Error, !result.IsSuccess, "", "", 0, StatusCodes.Status500InternalServerError, null);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("LookupService.cs", "UpdateStatus"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false, ex.Message, ex.InnerException.Message, 0, StatusCodes.Status500InternalServerError, null);
            }
        }
    }
}
