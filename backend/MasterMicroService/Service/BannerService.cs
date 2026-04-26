using CommonService.JWT;
using CommonService.Other;
using MasterMicroService.IService;
using ModelService.CommonModel;
using ModelService.Model.MastersModel;
using Serilog;
using static CommonService.Other.UtilityManager;

namespace MasterMicroService.Service
{
    public class BannerService : UtilityManager, IBannerService
    {
        private readonly HelperService _helperService;
        private readonly JWTAuthManager _jWTAuthManager;
        private readonly ILoggerManager _logger;
        private readonly FileUploader _fileUploader;
        public BannerService(HelperService helperService, JWTAuthManager jWTAuthManager, ILoggerManager logger, FileUploader fileUploader)
        {
            _jWTAuthManager = jWTAuthManager;
            _logger = logger;
            _fileUploader = fileUploader;
            _helperService = helperService;
        }
        public ServiceResponse<int> AddUpdate(BannerModel model)
        {
            try
            {
                ServiceResponse<int> response = new ServiceResponse<int>();
                model.UserId = _jWTAuthManager.User.Id;
                var viewData = new BannerViewModel();
                if (model.Id > 0)
                {
                    viewData = GetById(model.Id).Data;
                }
                var result = Execute(QueriesPaths.BannerQueries.QAddUpdate, _helperService.AddDynamicParameters(model), null);
                if (result.IsSuccess)
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
                    //when any error then file delete  
                    if (!string.IsNullOrEmpty(model.AttachmentUrl))
                        _fileUploader.DeleteFile(model.AttachmentUrl);
                    response = SetResultStatus<int>(result.Data, MessageStatus.Error, result.IsSuccess, "", "", 0, StatusCodes.Status500InternalServerError, null);
                }
                return response;
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("BannerService.cs", "AddUpdate"));
                return SetResultStatus<int>(0, MessageStatus.Error, false, ex.Message, "", 0, StatusCodes.Status500InternalServerError, null);
            }
        }

        public ServiceResponse<bool> Delete(int Id)
        {
            try
            {
                var deleteResult = ExecuteReturnData(QueriesPaths.BannerQueries.QDelete, new { Id = Id, UserId = _jWTAuthManager.User.Id });
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
                Log.Error(ex, CommonFunction.Errorstring("BannerService.cs", "Delete"));
                return SetResultStatus<bool>(true, MessageStatus.Error, false, "", "", 0, StatusCodes.Status500InternalServerError, null);
            }
        }

        public ServiceResponse<BannerViewModel> GetById(int Id)
        {
            try
            {
                var result = QueryFast<BannerViewModel>(QueriesPaths.BannerQueries.QGetById, new { Id = Id });
                if (result.Data != null)
                {
                    return SetResultStatus<BannerViewModel>(result.Data, MessageStatus.Success, result.IsSuccess, "", "", 0, StatusCodes.Status200OK, null);
                }
                else
                {
                    return SetResultStatus<BannerViewModel>(result.Data, MessageStatus.NoRecord, result.IsSuccess, "", "", 0, StatusCodes.Status404NotFound, null);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("BannerService.cs", "GetById"));
                return SetResultStatus<BannerViewModel>(null, MessageStatus.NoRecord, false, ex.Message, ex.InnerException.Message, 0, StatusCodes.Status500InternalServerError, null);
            }
        }

        public ServiceResponse<List<BannerViewListModel>> GetPagination(BannerFilterModel filterModel)
        {
            try
            {
                var result = QueryList<BannerViewListModel>(QueriesPaths.BannerQueries.QPagination, _helperService.AddDynamicParameters(filterModel));
                if (result.Data != null && result.Data.Count() > 0)
                {
                    return SetResultStatus<List<BannerViewListModel>>(result.Data, MessageStatus.Success, result.IsSuccess, "", "", result.Data[0].TotalRows, StatusCodes.Status200OK, null);
                }
                else
                {
                    return SetResultStatus<List<BannerViewListModel>>(null, MessageStatus.NoRecord, result.IsSuccess, "", "", 0, StatusCodes.Status404NotFound, null);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("BannerService.cs", "GetPagination"));
                return SetResultStatus<List<BannerViewListModel>>(null, MessageStatus.Error, false, ex.Message, ex.InnerException.Message, 0, StatusCodes.Status500InternalServerError, null);
            }
        }

        public ServiceResponse<bool> UpdateStatus(int Id)
        {
            try
            {
                var result = ExecuteReturnData(QueriesPaths.BannerQueries.QUpdateStatus, new { Id = Id, UserId = _jWTAuthManager.User.Id });
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
                Log.Error(ex, CommonFunction.Errorstring("BannerService.cs", "UpdateStatus"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false, ex.Message, ex.InnerException.Message, 0, StatusCodes.Status500InternalServerError, null);
            }
        }
    }
}
