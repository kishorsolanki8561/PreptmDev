using CommonService.JWT;
using CommonService.Other;
using MasterMicroService.IService;
using ModelService.Model.MastersModel;
using Serilog;

namespace MasterMicroService.Service
{
    public class QualificationMasterService : UtilityManager, IQualificationMasterService
    {
        private readonly HelperService _helperService;
        private readonly JWTAuthManager _jWTAuthManager;
        private readonly ILoggerManager _logger;
        public QualificationMasterService(HelperService helperService, JWTAuthManager jWTAuthManager, ILoggerManager logger)
        {
            _jWTAuthManager = jWTAuthManager;
            _logger = logger;
            _helperService = helperService;
        }
        public ServiceResponse<int> AddUpdate(QualificationMasterModel model)
        {
            try
            {
                ServiceResponse<int> response = new ServiceResponse<int>();
                model.UserId = _jWTAuthManager.User.Id;
                var result = Execute(@"Sp_QualificationMasterAddUpdate @Id,@Title,@UserId,@TitleHindi", _helperService.AddDynamicParameters(model), null);
                if (result.IsSuccess)
                {
                    if (result.StatusCode == StatusCodes.Status409Conflict)
                    {
                        response = SetResultStatus<int>(result.Data, MessageStatus.QualificationExist, result.IsSuccess);
                    }
                    else if (result.StatusCode == StatusCodes.Status200OK)
                    {
                        if (model.Id > 0)
                            response = SetResultStatus<int>(result.Data, MessageStatus.Update, result.IsSuccess);

                        else
                            response = SetResultStatus<int>(result.Data, MessageStatus.Save, result.IsSuccess);
                    }
                    else
                    {
                        response = SetResultStatus<int>(result.Data, MessageStatus.Error, result.IsSuccess);
                    }
                    return response;
                }
                return response;
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("QualificationMasterService.cs", "AddUpdate"));
                return SetResultStatus<int>(0, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<bool> Delete(int Id)
        {
            try
            {
                var deleteResult = ExecuteReturnData(@"Sp_QualificationMasterDeleteUpdateStatus @Type='Delete',@Id=" + Id + ",@UserId=" + _jWTAuthManager.User.Id + "", null);
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
                Log.Error(ex, CommonFunction.Errorstring("QualificationMasterService.cs", "Delete"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<QualificationMasterModel> GetById(int Id)
        {
            try
            {
                var result = QueryFast<QualificationMasterModel>(@"select * from Vw_QualificationMaster where Id=@Id", new { Id = Id });
                if (result.Data != null)
                {
                    return SetResultStatus<QualificationMasterModel>(result.Data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<QualificationMasterModel>(result.Data, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("QualificationMasterService.cs", "GetById"));
                return SetResultStatus<QualificationMasterModel>(null, MessageStatus.Error, false, ex.Message);
            }
        }

        public ServiceResponse<List<QualificationMasterViewModel>> GetList()
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<List<QualificationMasterViewModel>> GetPagination(QualificationMasterFilterModel filterModel)
        {
            try
            {
                //string[] keys = { "Page", "PageSize", "Search", "OrderBy", "OrderByAsc", "IsActive", "FromDate", "ToDate", "MenuName", "HashChild", "ParentMenuId" };
                var result = QueryList<QualificationMasterViewModel>(@"Sp_QualificationMasterPagination @Page,@PageSize,@Search,@OrderBy,@OrderByAsc,@IsActive,@FromDate,@ToDate,@Title", _helperService.AddDynamicParameters(filterModel));
                if (result.Data != null && result.Data.Count() > 0)
                {
                    return SetResultStatus<List<QualificationMasterViewModel>>(result.Data, MessageStatus.Success, true, "", "", result.Data[0].TotalRows);
                }
                else
                {
                    return SetResultStatus<List<QualificationMasterViewModel>>(null, MessageStatus.NoRecord, false);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("QualificationMasterService.cs", "GetPagination"));
                return SetResultStatus<List<QualificationMasterViewModel>>(null, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<bool> UpdateStatus(int Id)
        {
            try
            {
                var result = ExecuteReturnData(@"Sp_QualificationMasterDeleteUpdateStatus @Type='Status',@Id=" + Id + ",@UserId=" + _jWTAuthManager.User.Id + "", null);
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
                Log.Error(ex, CommonFunction.Errorstring("QualificationMasterService.cs", "UpdateStatus"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }
    }
}
