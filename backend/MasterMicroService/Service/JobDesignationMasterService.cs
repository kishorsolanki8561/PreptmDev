using CommonService.JWT;
using CommonService.Other;
using MasterMicroService.IService;
using ModelService.Model.MastersModel;
using Serilog;

namespace MasterMicroService.Service
{
    public class JobDesignationMasterService : UtilityManager, IJobDesignationMasterService
    {
        private readonly HelperService _helperService;
        private readonly JWTAuthManager _jWTAuthManager;
        private readonly ILoggerManager _logger;
        public JobDesignationMasterService(HelperService helperService, JWTAuthManager jWTAuthManager, ILoggerManager logger)
        {
            _jWTAuthManager = jWTAuthManager;
            _logger = logger;
            _helperService = helperService;
        }

        public ServiceResponse<int> AddUpdate(JobDesignationMasterModel model)
        {
            try
            {
                ServiceResponse<int> response = new ServiceResponse<int>();
                model.UserId = _jWTAuthManager.User.Id;
                var result = Execute(QueriesPaths.JobDesignationMasterQueries.QAddUpdate, _helperService.AddDynamicParameters(model), null);
                if (result.IsSuccess)
                {
                    if (result.StatusCode == StatusCodes.Status409Conflict)
                    {
                        response = SetResultStatus<int>(result.Data, MessageStatus.jobDesignationExist, result.IsSuccess);
                    }
                    else if (result.StatusCode == StatusCodes.Status200OK)
                    {
                        if (model.Id > 0)
                            response = SetResultStatus<int>(result.Data, MessageStatus.Update, result.IsSuccess);

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
                Log.Error(ex, CommonFunction.Errorstring("JobDesignationMasterService.cs", "AddUpdate"));
                return SetResultStatus<int>(0, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<bool> Delete(int Id)
        {
            try
            {
                var deleteResult = ExecuteReturnData(QueriesPaths.JobDesignationMasterQueries.QDelete, new {Id=Id,UserId=_jWTAuthManager.User.Id});
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
                Log.Error(ex, CommonFunction.Errorstring("JobDesignationMasterService.cs", "Delete"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<JobDesignationMasterModel> GetById(int Id)
        {
            try
            {
                var result = QueryFast<JobDesignationMasterModel>(QueriesPaths.JobDesignationMasterQueries.QGetById, new { Id = Id });
                if (result.Data != null)
                {
                    return SetResultStatus<JobDesignationMasterModel>(result.Data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<JobDesignationMasterModel>(result.Data, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("JobDesignationMasterService.cs", "GetById"));
                return SetResultStatus<JobDesignationMasterModel>(null, MessageStatus.Error, false, ex.Message);
            }
        }

        public ServiceResponse<List<JobDesignationMasterViewModel>> GetList()
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<List<JobDesignationMasterViewModel>> GetPagination(JobDesignationMasterFilterModel filterModel)
        {
            try
            {
                var result = QueryList<JobDesignationMasterViewModel>(QueriesPaths.JobDesignationMasterQueries.QPagination, _helperService.AddDynamicParameters(filterModel));
                if (result.Data != null && result.Data.Count() > 0)
                {
                    return SetResultStatus<List<JobDesignationMasterViewModel>>(result.Data, MessageStatus.Success, true, "", "", result.Data[0].TotalRows);
                }
                else
                {
                    return SetResultStatus<List<JobDesignationMasterViewModel>>(null, MessageStatus.NoRecord, false);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("JobDesignationMasterService.cs", "GetPagination"));
                return SetResultStatus<List<JobDesignationMasterViewModel>>(null, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<bool> UpdateStatus(int Id)
        {
            try
            {
                var result = ExecuteReturnData(QueriesPaths.JobDesignationMasterQueries.QUpdateStatus, new { Id = Id, UserId = _jWTAuthManager.User.Id });
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
                Log.Error(ex, CommonFunction.Errorstring("JobDesignationMasterService.cs", "UpdateStatus"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }

        }
    }
}
