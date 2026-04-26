using CommonService.JWT;
using CommonService.Other;
using MasterMicroService.IService;
using ModelService.CommonModel;
using ModelService.Model.MastersModel;
using Serilog;

namespace MasterMicroService.Service
{
    public class DepartmentMasterService : UtilityManager, IDepartmentMasterService
    {
        private readonly HelperService _helperService;
        private readonly JWTAuthManager _jWTAuthManager;
        private readonly ILoggerManager _logger;
        private readonly FileUploader _fileUploader;
        public DepartmentMasterService(HelperService helperService, JWTAuthManager jWTAuthManager, ILoggerManager logger, FileUploader fileUploader)
        {
            _jWTAuthManager = jWTAuthManager;
            _logger = logger;
            _helperService = helperService;
            _fileUploader = fileUploader;
        }

        public ServiceResponse<int> AddUpdate(DepartmentMasterModel model)
        {
            try
            {
                ServiceResponse<int> response = new ServiceResponse<int>();
                DepartmentMasterModel departmentMasterData = new DepartmentMasterModel();
                model.UserId = _jWTAuthManager.User.Id;
                if (!string.IsNullOrEmpty(model.SlugUrl))
                    model.SlugUrl = SlugHelper.ToUrlSlug(model.SlugUrl);
                if (model.Id > 0)
                {
                    departmentMasterData = GetById((int)model.Id).Data;
                }
                var result = Execute(QueriesPaths.DepartmentMasterQueries.QAddUpdate, _helperService.AddDynamicParameters(model), null);
                if (!result.IsSuccess && result.StatusCode == StatusCodes.Status409Conflict)
                {
                    response = SetResultStatus<int>(result.Data, MessageStatus.DepartmentExist, result.IsSuccess);
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
                Log.Error(ex, CommonFunction.Errorstring("DepartmentMasterService.cs", "AddUpdate"));
                return SetResultStatus<int>(0, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<bool> Delete(int Id)
        {
            try
            {
                var deleteResult = ExecuteReturnData(QueriesPaths.DepartmentMasterQueries.QDelete, new { Id = Id, UserId = _jWTAuthManager.User.Id });
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
                Log.Error(ex, CommonFunction.Errorstring("DepartmentMasterService.cs", "Delete"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<DepartmentMasterModel> GetById(int Id)
        {
            try
            {
                var result = QueryFast<DepartmentMasterModel>(QueriesPaths.DepartmentMasterQueries.QGetById, new { Id = Id });
                if (result.Data != null)
                {
                    //result.Data.Logo = result.Data.Logo.ToAbsolutepathPath();
                    return SetResultStatus<DepartmentMasterModel>(result.Data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<DepartmentMasterModel>(result.Data, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("DepartmentMasterService.cs", "GetById"));
                return SetResultStatus<DepartmentMasterModel>(null, MessageStatus.Error, false, ex.Message);
            }
        }

        public ServiceResponse<List<DepartmentMasterViewModel>> GetList()
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<List<DepartmentMasterViewModel>> GetPagination(DepartmentMasterFilterModel filterModel)
        {
            try
            {
                var result = QueryList<DepartmentMasterViewModel>(QueriesPaths.DepartmentMasterQueries.QPagination, _helperService.AddDynamicParameters(filterModel));
                if (result.Data != null && result.Data.Count() > 0)
                {
                    result.Data.Select(s => s.Logo.ToAbsolutepathPath()).ToList();
                    return SetResultStatus<List<DepartmentMasterViewModel>>(result.Data, MessageStatus.Success, true, "", "", result.Data[0].TotalRows);
                }
                else
                {
                    return SetResultStatus<List<DepartmentMasterViewModel>>(null, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("DepartmentMasterService.cs", "GetPagination"));
                return SetResultStatus<List<DepartmentMasterViewModel>>(null, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<bool> UpdateStatus(int Id)
        {
            try
            {
                var result = Execute(QueriesPaths.DepartmentMasterQueries.QUpdateStatus, new { Id = Id, UserId = _jWTAuthManager.User.Id });
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
                Log.Error(ex, CommonFunction.Errorstring("DepartmentMasterService.cs", "UpdateStatus"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }
    }
}
