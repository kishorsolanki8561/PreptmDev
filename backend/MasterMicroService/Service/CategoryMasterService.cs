using CommonService.JWT;
using CommonService.Other;
using MasterMicroService.IService;
using ModelService.CommonModel;
using ModelService.Model.MastersModel;
using Serilog;

namespace MasterMicroService.Service
{
    public class CategoryMasterService : UtilityManager, ICategoryMasterService
    {
        private readonly HelperService _helperService;
        private readonly JWTAuthManager _jWTAuthManager;
        private readonly ILoggerManager _logger;
        private readonly FileUploader _fileUploader;
        public CategoryMasterService(HelperService helperService, JWTAuthManager jWTAuthManager, ILoggerManager logger, FileUploader fileUploader)
        {
            _jWTAuthManager = jWTAuthManager;
            _logger = logger;
            _helperService = helperService;
            _fileUploader = fileUploader;
        }

        public ServiceResponse<int> AddUpdate(CategoryMasterModel model)
        {
            try
            {
                ServiceResponse<int> response = new ServiceResponse<int>();
                CategoryMasterModel CategoryMasterData = new CategoryMasterModel();
                model.UserId = _jWTAuthManager.User.Id;
                if (!string.IsNullOrEmpty(model.SlugUrl))
                    model.SlugUrl = SlugHelper.ToUrlSlug(model.SlugUrl);
                if (model.Id > 0)
                {
                    CategoryMasterData = GetById((int)model.Id).Data;

                }
                var result = Execute(QueriesPaths.CategoryMasterQueries.QAddUpdate, _helperService.AddDynamicParameters(model), null);
                if (!result.IsSuccess && result.StatusCode == StatusCodes.Status409Conflict)
                {
                    response = SetResultStatus<int>(result.Data, MessageStatus.CategoryExist, result.IsSuccess);
                }
                else if (!result.IsSuccess && result.StatusCode == 408)
                {
                    response = SetResultStatus<int>(result.Data, MessageStatus.SlugUrlExist, result.IsSuccess);
                }
                else if (result.IsSuccess)
                {
                    if (result.StatusCode == StatusCodes.Status200OK)
                    {
                        ////delete logo
                        //if (model.Id > 0 && string.IsNullOrEmpty(model.Icon))
                        //{
                        //    var logoDeletePath = CategoryMasterData.Icon;
                        //    if (!string.IsNullOrEmpty(logoDeletePath))
                        //        _fileUploader.DeleteFile(logoDeletePath);
                        //}
                        //if (icon != null)
                        //{
                        //    FileUploadModel file = new FileUploadModel();
                        //    var datetime = DateTime.UtcNow;
                        //    file.file = icon;
                        //    file.filename = (model.Id > 0 ? model.Id : result.Data) + "_" + datetime.Second.ToString() + ".webp";//System.IO.Path.GetExtension(file.file.FileName); //file.file.
                        //    file.path = QueriesPaths.CategoryMasterQueries.QDirectoryPath;
                        //    model.Icon = _fileUploader.PostFile(file);
                        //    if (!string.IsNullOrEmpty(model.Icon))
                        //    {
                        //        var ids = (model.Id > 0 ? model.Id : result.Data);
                        //        Execute(QueriesPaths.CategoryMasterQueries.QUpdatePath, new { icon = model.Icon, Id = ids }, null);
                        //    }
                        //}
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
                Log.Error(ex, CommonFunction.Errorstring("CategoryMasterService.cs", "AddUpdate"));
                return SetResultStatus<int>(0, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<bool> Delete(int Id)
        {
            try
            {
                var deleteResult = ExecuteReturnData(QueriesPaths.CategoryMasterQueries.QDelete, new { Id = Id, UserId = _jWTAuthManager.User.Id });
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
                Log.Error(ex, CommonFunction.Errorstring("CategoryMasterService.cs", "Delete"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<CategoryMasterModel> GetById(int Id)
        {
            try
            {
                var result = QueryFast<CategoryMasterModel>(QueriesPaths.CategoryMasterQueries.QGetById, new { Id = Id });
                if (result.Data != null)
                {
                    return SetResultStatus<CategoryMasterModel>(result.Data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<CategoryMasterModel>(result.Data, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("CategoryMasterService.cs", "GetById"));
                return SetResultStatus<CategoryMasterModel>(null, MessageStatus.Error, false, ex.Message);
            }
        }

        public ServiceResponse<List<CategoryMasterViewModel>> GetList()
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<List<CategoryMasterViewModel>> GetPagination(CategoryMasterFilterModel filterModel)
        {
            try
            {
                //string[] keys = { "Page", "PageSize", "Search", "OrderBy", "OrderByAsc", "IsActive", "FromDate", "ToDate", "MenuName", "HashChild", "ParentMenuId" };
                var result = QueryList<CategoryMasterViewModel>(QueriesPaths.CategoryMasterQueries.QPagination, _helperService.AddDynamicParameters(filterModel));
                if (result.Data != null && result.Data.Count() > 0)
                {
                    return SetResultStatus<List<CategoryMasterViewModel>>(result.Data, MessageStatus.Success, true, "", "", result.Data[0].TotalRows);
                }
                else
                {
                    return SetResultStatus<List<CategoryMasterViewModel>>(null, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("CategoryMasterService.cs", "GetPagination"));
                return SetResultStatus<List<CategoryMasterViewModel>>(null, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<bool> UpdateStatus(int Id)
        {
            try
            {
                var result = ExecuteReturnData(QueriesPaths.CategoryMasterQueries.QUpdateStatus, new { Id = Id, UserId = _jWTAuthManager.User.Id });
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
                Log.Error(ex, CommonFunction.Errorstring("CategoryMasterService.cs", "UpdateStatus"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }
    }
}
