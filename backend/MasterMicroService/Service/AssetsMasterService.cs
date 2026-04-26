using CommonService.JWT;
using CommonService.Other;
using MasterMicroService.IService;
using ModelService.CommonModel;
using ModelService.Model.MastersModel;
using Serilog;

namespace MasterMicroService.Service
{
    public class AssetsMasterService : UtilityManager, IAssetsMasterService
    {
        private readonly HelperService _helperService;
        private readonly JWTAuthManager _jWTAuthManager;
        private readonly ILoggerManager _logger;
        private readonly FileUploader _fileUploader;

        public AssetsMasterService(HelperService helperService, JWTAuthManager jWTAuthManager, ILoggerManager logger, FileUploader fileUploader)
        {
            _jWTAuthManager = jWTAuthManager;
            _logger = logger;
            _helperService = helperService;
            _fileUploader = fileUploader;
        }

        public ServiceResponse<int> AddUpdate(AssetsMasterModel model)
        {
            try
            {
                AssetsMasterModel assetsMasterData = new AssetsMasterModel();
                ServiceResponse<int> response = new ServiceResponse<int>();
                model.UserId = _jWTAuthManager.User.Id;
                if (model.Id > 0)
                {
                    assetsMasterData = GetById((int)model.Id).Data;
                }
                var result = Execute(QueriesPaths.AssetsMasterQueries.QAddUpdate, _helperService.AddDynamicParameters(model), null);
                if (!result.IsSuccess && result.StatusCode == StatusCodes.Status409Conflict)
                {
                    response = SetResultStatus<int>(result.Data, MessageStatus.TitleExist, result.IsSuccess);
                }
                else if (result.IsSuccess)
                {
                    if (result.StatusCode == StatusCodes.Status200OK)
                    {
                        if (model.Id > 0)
                            response = SetResultStatus<int>(model.Id, MessageStatus.Update, result.IsSuccess);
                        else
                            response = SetResultStatus<int>(result.Data, MessageStatus.Save, result.IsSuccess);
                    }
                    return response;
                }
                else
                {
                    response = SetResultStatus<int>(result.Data, MessageStatus.Error, result.IsSuccess);
                }
                return response;
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("AssetsMasterService.cs", "AddUpdate"));
                return SetResultStatus<int>(0, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<AssetsMasterModel> GetById(int Id)
        {
            try
            {
                var result = QueryFast<AssetsMasterModel>(QueriesPaths.AssetsMasterQueries.QGetById, new { Id = Id });
                if (result.Data != null)
                {
                    return SetResultStatus<AssetsMasterModel>(result.Data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<AssetsMasterModel>(null, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("AssetsMasterService.cs", "GetById"));
                return SetResultStatus<AssetsMasterModel>(null, MessageStatus.Error, false, "");
            }
        }
        public ServiceResponse<List<AssetsMasterViewModel>> GetPagination(AssetsMasterFilterModel filterModel)
        {
            try
            {
                var result = QueryList<AssetsMasterViewModel>(QueriesPaths.AssetsMasterQueries.QPagination, _helperService.AddDynamicParameters(filterModel));
                if (result.Data != null && result.Data.Count() > 0)
                {
                    return SetResultStatus<List<AssetsMasterViewModel>>(result.Data, MessageStatus.Success, true, String.Empty, String.Empty, result.Data[0].TotalRows);
                }
                else
                {
                    return SetResultStatus<List<AssetsMasterViewModel>>(null, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("AssetsMasterService.cs", "GetPagination"));
                return SetResultStatus<List<AssetsMasterViewModel>>(null, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<bool> Delete(int Id)
        {
            try
            {
                var deleteResult = ExecuteReturnData(QueriesPaths.AssetsMasterQueries.QDelete, new { Id = Id, UserId = _jWTAuthManager.User.Id });
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
                Log.Error(ex, CommonFunction.Errorstring("AssetsMasterService.cs", "Delete"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<bool> UpdateStatus(int Id)
        {
            try
            {
                var result = ExecuteReturnData(QueriesPaths.AssetsMasterQueries.QUpdateStatus, new { Id = Id, UserId = _jWTAuthManager.User.Id });
                if (result.IsSuccess)
                {
                    if (result.StatusCode == StatusCodes.Status200OK)
                        return SetResultStatus<bool>(true, MessageStatus.StatusUpdate, true);
                    else if (result.StatusCode == StatusCodes.Status404NotFound)
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
                Log.Error(ex, CommonFunction.Errorstring("AssetsMasterService.cs", "UpdateStatus"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }
    }
}
