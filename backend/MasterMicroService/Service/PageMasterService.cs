using CommonService.JWT;
using CommonService.Other;
using MasterMicroService.IService;
using ModelService.Model.MastersModel;
using Serilog;
using System.Data.SqlClient;

namespace MasterMicroService.Service
{
    public class PageMasterService : UtilityManager, IPageMasterService
    {
        private readonly JWTAuthManager _jWTAuthManager;
        private readonly ILoggerManager _logger;
        private readonly HelperService _helperService;
        public PageMasterService(JWTAuthManager jWTAuthManager, ILoggerManager logger, HelperService helperService)
        {
            _jWTAuthManager = jWTAuthManager;
            _logger = logger;
            _helperService = helperService;
        }
       
        public ServiceResponse<bool> Delete(int Id)
        {
            try
            { 
                var deleteResult = AddUpdate(@"Sp_PageMasterDeleteUpdateStatus @Type,@Id,@UserId", new { Type = "Delete", Id = Id, UserId = _jWTAuthManager.User.Id });
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
                Log.Error(ex, CommonFunction.Errorstring("PageMasterService.cs", "Delete"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<List<PageMasterViewModel>> GetList()
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<List<PageMasterViewModel>> GetPagination(PageMasterFilterModel filterModel)
        {
            try
            {
                var result = QueryList<PageMasterViewModel>(QueriesPaths.PageMasterQueries.QPagination, _helperService.AddDynamicParameters(filterModel));
                if (result.Data != null && result.Data.Count() > 0)
                {
                    return SetResultStatus<List<PageMasterViewModel>>(result.Data, MessageStatus.Success, true, "", "", result.Data[0].TotalRows);
                }
                else
                {
                    return SetResultStatus<List<PageMasterViewModel>>(null, MessageStatus.NoRecord, false);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("PageMasterService.cs", "GetPagination"));
                return SetResultStatus<List<PageMasterViewModel>>(null, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<bool> UpdateStatus(int Id)
        {
            try
            {
                var updateResult = ExecuteReturnData(QueriesPaths.PageMasterQueries.QUpdateStatus, new { Id = Id, UserId = _jWTAuthManager.User.Id });
                if (updateResult.IsSuccess)
                {
                    if (updateResult.StatusCode == StatusCodes.Status200OK)
                        return SetResultStatus<bool>(true, MessageStatus.StatusUpdate, true);
                    else if (updateResult.StatusCode == StatusCodes.Status404NotFound)
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
                Log.Error(ex, CommonFunction.Errorstring("PageMasterService.cs", "UpdateStatus"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<List<PagePermissionViewModel>> PagePermissionList(int userTypeCode)
        {
            try
            {
                var result = QueryList<PagePermissionViewModel>(QueriesPaths.PageMasterQueries.QPagePermissionList, new { UserTypeIdCode = userTypeCode });
                if (result.Data != null)
                {
                    return SetResultStatus<List<PagePermissionViewModel>>(result.Data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<List<PagePermissionViewModel>>(null, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("PageMasterService.cs", "PagePermissionList"));
                return SetResultStatus<List<PagePermissionViewModel>>(null, MessageStatus.Error, false, ex.Message);
            }
        }
        public ServiceResponse<bool> PageMasterPermissionModifiedById(List<int> model, int UserTypeCode)
        {
            try
            {
                ServiceResponse<int> updateResult = new ServiceResponse<int>();
                if (model != null)
                {
                    foreach (var item in model)
                    {
                        if (item > 0)
                        {
                            PagePermissionModel page = new PagePermissionModel();
                            page.Id = item;
                            page.UserTypeCode = UserTypeCode;
                            string[] spKeys = { "Id", "UserTypeCode" };
                            updateResult = AddUpdate(QueriesPaths.PageMasterQueries.QPageMasterPermissionModifiedById, _helperService.AddDynamicParameters(page, spKeys), null);
                            if (updateResult.IsSuccess)
                            {
                                Execute(QueriesPaths.UserMasterQueries.QIsAutoLoggout, new { IsAutoLoggedOut = true, UserTypeCode = UserTypeCode }, null);
                            }
                        }
                    }
                }
                if (updateResult.IsSuccess)
                {
                    if (updateResult.StatusCode == StatusCodes.Status200OK)
                        return SetResultStatus<bool>(true, MessageStatus.Update, true);
                    else if (updateResult.StatusCode == StatusCodes.Status404NotFound)
                        return SetResultStatus<bool>(false, MessageStatus.NoRecord, true);
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
                Log.Error(ex, CommonFunction.Errorstring("PageMasterService.cs", "PageMasterPermissionModifiedById"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }
        public ServiceResponse<int> AddUpdate(PageMasterModel model)
        {
            SqlTransaction lSqlTrans = GetNewTransaction();
            List<PageComponentActionModel> actionList = new List<PageComponentActionModel>();
            PageMasterModel pageModel = new PageMasterModel();
            try
            {
                string[] spKeys = { "Id", "Name", "Icon", "PageUrl", "MenuId", "UserId" };
                string[] otherKeys = { "UserId" };
                var Result = AddUpdate(QueriesPaths.PageMasterQueries.QAddUpdate, _helperService.AddDynamicParameters(model, spKeys, otherKeys), lSqlTrans);
                if (!Result.IsSuccess)
                {
                    lSqlTrans.Rollback();
                    return SetResultStatus<int>(0, MessageStatus.Error, false); ;
                }
                if (Result.IsSuccess)
                {
                    //update delete  PageComponent and PageComponentAction
                    if (model.PageComponents != null && model.PageComponents.Count > 0)
                    {
                        if (model.Id > 0)
                        {
                            var componentList = pageComponentList(model.Id).Data;
                            if (componentList != null)
                            {
                                var deleteComponentList = componentList.Select(x => x.Id).ToList().Where(z => !model.PageComponents.Where(a => a.Id != 0).ToList().Select(x => x.Id).Contains(z)).ToList();
                                if (deleteComponentList != null)
                                {
                                    foreach (var id in deleteComponentList)
                                    {
                                        pageComponentDelete(id);
                                    }
                                }
                            }
                            foreach (var item in model.PageComponents)
                            {
                                //update delete PageComponentAction
                                if (item.Id > 0)
                                {
                                    var componentActionList = pageComponentActionList(item.Id).Data;
                                    actionList.AddRange(componentActionList);
                                    var deleteActionComponentList = componentActionList.Where(z => !item.PageComponentsAction.Contains(z.Action)).Select(a => a.Id).ToList();
                                    if (deleteActionComponentList != null)
                                    {
                                        foreach (var id in deleteActionComponentList)
                                            pageComponentActionDelete(id);
                                    }
                                }
                            }
                        }

                        //add PageComponent
                        foreach (var item in model.PageComponents)
                        {
                            item.PageId = model.Id > 0 ? model.Id : Result.Data;
                            string[] spPMasterKeys = { "Id", "Name", "PageId" };
                            var pageComponentResult = Execute(@"Sp_PageComponentAddUpdate  @Id,@Name,@PageId", _helperService.AddDynamicParameters(item, spPMasterKeys), lSqlTrans);
                            if (pageComponentResult.StatusCode == StatusCodes.Status409Conflict)
                            {
                                lSqlTrans.Rollback();
                                return SetResultStatus<int>(0, MessageStatus.PageComponentExist, pageComponentResult.IsSuccess);
                            }
                            if (pageComponentResult.IsSuccess)
                            {
                                item.PageComponentsAction = item.PageComponentsAction.Where(z => !actionList.Where(z => z.ComponentId == item.Id).Select(a => a.Action).Contains(z)).ToList();
                                //Add PageComponentAction
                                foreach (var action in item.PageComponentsAction)
                                {
                                    PageComponentActionModel actionModel = new PageComponentActionModel();
                                    actionModel.Id = 0;
                                    actionModel.PageId = Result.Data;
                                    actionModel.ComponentId = item.Id > 0 ? item.Id : pageComponentResult.Data;
                                    actionModel.Action = action;
                                    string[] spPMActionKeys = { "Id", "PageId", "ComponentId", "Action" };
                                    var pageComponentActionResult = AddUpdate(@"Sp_PageComponentActionAddUpdate  @Id,@PageId,@ComponentId,@Action", _helperService.AddDynamicParameters(actionModel, spPMActionKeys), lSqlTrans);
                                    if (pageComponentActionResult.IsSuccess)
                                    {
                                        var pageComponentPermissonActionResult = AddUpdate(@"Sp_PageMasterComponentPermissionAddUpdate  @PageId,@MenuId,@PageComponentId,@PageCompActionId", new { PageId = Result.Data, MenuId = model.MenuId, PageComponentId = actionModel.ComponentId, PageCompActionId = pageComponentActionResult.Data }, lSqlTrans, null, false);
                                        if (!pageComponentPermissonActionResult.IsSuccess)
                                        {
                                            lSqlTrans.Rollback();
                                            return SetResultStatus<int>(0, MessageStatus.Error, false); ;
                                        }
                                    }
                                    if (!pageComponentActionResult.IsSuccess)
                                    {
                                        lSqlTrans.Rollback();
                                        return SetResultStatus<int>(0, MessageStatus.Error, false); ;
                                    }
                                }
                            }

                            if (!pageComponentResult.IsSuccess)
                            {
                                lSqlTrans.Rollback();
                                return SetResultStatus<int>(0, MessageStatus.Error, false); ;
                            }
                        }

                    }
                    lSqlTrans.Commit();
                    if (model.Id > 0)
                    {
                        return SetResultStatus<int>(Result.Data, MessageStatus.Update, Result.IsSuccess);
                    }
                    else
                    {
                        return SetResultStatus<int>(Result.Data, MessageStatus.Save, Result.IsSuccess);

                    }

                }
                else
                {
                    return SetResultStatus<int>(Result.Data, MessageStatus.Error, Result.IsSuccess);
                }


            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("PageMasterService.cs", "AddUpdate"));
                return SetResultStatus<int>(0, MessageStatus.Error, false);
            }
        }
        private ServiceResponse<List<PageComponentModel>> pageComponentList(int pageId)
        {
            try
            {
                var result = QueryList<PageComponentModel>(@"select * from PageComponent where PageId = @Id", new { Id = pageId });
                if (result.Data != null)
                {
                    return SetResultStatus<List<PageComponentModel>>(result.Data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<List<PageComponentModel>>(result.Data, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("PageMasterService.cs", "pageComponentList"));
                return SetResultStatus<List<PageComponentModel>>(null, MessageStatus.Error, false, ex.Message);
            }
        }
        private ServiceResponse<List<PageComponentActionModel>> pageComponentActionList(int ComponentId)
        {
            try
            {
                var result = QueryList<PageComponentActionModel>(@"select * from PageComponentAction where ComponentId = @Id", new { Id = ComponentId });
                if (result.Data != null)
                {
                    return SetResultStatus<List<PageComponentActionModel>>(result.Data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<List<PageComponentActionModel>>(result.Data, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("PageMasterService.cs", "pageComponentActionList"));
                return SetResultStatus<List<PageComponentActionModel>>(new List<PageComponentActionModel>(), MessageStatus.Error, false, ex.Message);
            }
        }
        private ServiceResponse<bool> pageComponentDelete(int Id)
        {
            try
            {
                var deleteResult = ExecuteReturnData(@"Sp_PageComponentDelete @Type='Delete',@Id=" + Id + "", null);
                if (deleteResult.IsSuccess)
                {
                    if (deleteResult.StatusCode == StatusCodes.Status200OK)
                        return SetResultStatus<bool>(true, MessageStatus.Delete, true);
                    else if (deleteResult.StatusCode == StatusCodes.Status404NotFound)
                        return SetResultStatus<bool>(false, MessageStatus.NoRecord, true);
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
                Log.Error(ex, CommonFunction.Errorstring("PageMasterService.cs", "pageComponentDelete"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }
        private ServiceResponse<bool> pageComponentActionDelete(int Id)
        {
            try
            {
                var deleteResult = ExecuteReturnData(@"Sp_PageComponentActionDelete @Type='Delete',@Id=" + Id + "", null);
                if (deleteResult.IsSuccess)
                {
                    if (deleteResult.StatusCode == StatusCodes.Status200OK)
                        return SetResultStatus<bool>(true, MessageStatus.Delete, true);
                    else if (deleteResult.StatusCode == StatusCodes.Status404NotFound)
                        return SetResultStatus<bool>(false, MessageStatus.NoRecord, true);
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
                Log.Error(ex, CommonFunction.Errorstring("PageMasterService.cs", "pageComponentActionDelete"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<PageMasterModel> GetById(int Id)
        {
            try
            {
                var result = QueryFast<PageMasterModel>(QueriesPaths.PageMasterQueries.QGetById, new { Id = Id });
                if (result.Data != null)
                {
                    var pageComponent = pageComponentList(Id).Data;
                    foreach (var item in pageComponent)
                    {
                        item.PageComponentsAction = pageComponentActionList(item.Id).Data.Select(x => x.Action).ToList();
                    }
                    result.Data.PageComponents = pageComponent != null ? pageComponent : new List<PageComponentModel>();
                    return SetResultStatus<PageMasterModel>(result.Data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<PageMasterModel>(result.Data, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("PageMasterService.cs", "GetById"));
                return SetResultStatus<PageMasterModel>(null, MessageStatus.Error, false, ex.Message);
            }
        }
        public ServiceResponse<List<PagePermissionViewModel>> PagePermissionListN(int userTypeCode)
        {
            try
            {
                var result = QueryList<PagePermissionViewModel>(QueriesPaths.PageMasterQueries.QPagePermissionList, new { UserTypeIdCode = userTypeCode });
                if (result.Data != null)
                {
                    return SetResultStatus<List<PagePermissionViewModel>>(result.Data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<List<PagePermissionViewModel>>(result.Data, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("PageMasterService.cs", "PagePermissionListN"));
                return SetResultStatus<List<PagePermissionViewModel>>(null, MessageStatus.Error, false, ex.Message);
            }
        }
    }
}
