using CommonService.JWT;
using CommonService.Other;
using MasterMicroService.IService;
using ModelService.Model.MastersModel;
using Serilog;

namespace MasterMicroService.Service
{
    public class GroupMasterService : UtilityManager, IGroupMasterService
    {
        private readonly HelperService _helperService;
        private readonly JWTAuthManager _jWTAuthManager;
        private readonly ILoggerManager _logger;

        public GroupMasterService(HelperService helperService, JWTAuthManager jWTAuthManager, ILoggerManager logger)
        {
            _jWTAuthManager = jWTAuthManager;
            _logger = logger;
            _helperService = helperService;
        }
        public UtilityManager.ServiceResponse<int> AddUpdate(GroupMasterModel model)
        {
            try
            {
                ServiceResponse<int> response = new ServiceResponse<int>();
                model.UserId = _jWTAuthManager.User.Id;
                if (!string.IsNullOrEmpty(model.SlugUrl))
                    model.SlugUrl = SlugHelper.ToUrlSlug(model.SlugUrl);
                var result = Execute(QueriesPaths.GroupMasterQueries.QAddUpdate, _helperService.AddDynamicParameters(model), null);
                if (!result.IsSuccess && result.StatusCode == StatusCodes.Status409Conflict)
                {
                    response = SetResultStatus<int>(result.Data, MessageStatus.BlockTypeExist, result.IsSuccess);
                }
                else if (!result.IsSuccess && result.StatusCode == 408)
                {
                    response = SetResultStatus<int>(result.Data, MessageStatus.SlugUrlExist, result.IsSuccess);
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
                Log.Error(ex, CommonFunction.Errorstring("GroupMasterService.cs", "AddUpdate"));
                return SetResultStatus<int>(0, MessageStatus.Error, false);
            }
        }

        public UtilityManager.ServiceResponse<bool> Delete(int Id)
        {
            try
            {
                var deleteResult = ExecuteReturnData(QueriesPaths.GroupMasterQueries.QDelete, new {Id=Id,UserId=_jWTAuthManager.User.Id});
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
                Log.Error(ex, CommonFunction.Errorstring("GroupMasterService.cs", "Delete"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }

        public UtilityManager.ServiceResponse<GroupMasterModel> GetById(int Id)
        {
            try
            {
                var result = QueryFast<GroupMasterModel>(QueriesPaths.GroupMasterQueries.QGetById, new { Id = Id });
                if (result.Data != null)
                {
                    return SetResultStatus<GroupMasterModel>(result.Data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<GroupMasterModel>(result.Data, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("GroupMasterService.cs", "GetById"));
                return SetResultStatus<GroupMasterModel>(null, MessageStatus.Error, false, ex.Message);
            }
        }

        public ServiceResponse<List<GroupMasterViewModel>> GetPagination(GroupMasterFilterModel filterModel)
        {
            try
            {
                var result = QueryList<GroupMasterViewModel>(QueriesPaths.GroupMasterQueries.QPagination, _helperService.AddDynamicParameters(filterModel));
                if (result.Data != null && result.Data.Count() > 0)
                {
                    return SetResultStatus<List<GroupMasterViewModel>>(result.Data, MessageStatus.Success, true, "", "", result.Data[0].TotalRows);
                }
                else
                {
                    return SetResultStatus<List<GroupMasterViewModel>>(null, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("GroupMasterService.cs", "GetPagination"));
                return SetResultStatus<List<GroupMasterViewModel>>(null, MessageStatus.Error, false);
            }
        }

        public UtilityManager.ServiceResponse<bool> UpdateStatus(int Id)
        {
            try
            {
                var result = ExecuteReturnData(QueriesPaths.GroupMasterQueries.QUpdateStatus, new { Id = Id, UserId = _jWTAuthManager.User.Id });
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
                Log.Error(ex, CommonFunction.Errorstring("GroupMasterService.cs", "UpdateStatus"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }
    }
}
