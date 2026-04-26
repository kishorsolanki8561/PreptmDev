using AutoMapper;
using CommonService.JWT;
using CommonService.Other;
using Dapper;
using MasterMicroService;
using MasterService.IService;
using ModelService.Model.MastersModel;
using Serilog;
using System.Data.SqlClient;

namespace MasterService.Service
{
    public class MenuMasterService : UtilityManager, IMenuMasterService
    {
        private readonly HelperService _helperService;
        private readonly JWTAuthManager _jWTAuthManager;
        private readonly ILoggerManager _logger;

        public MenuMasterService(HelperService helperService, JWTAuthManager jWTAuthManager, ILoggerManager logger)
        {
            _jWTAuthManager = jWTAuthManager;
            _logger = logger;
            _helperService = helperService;
        }
        public ServiceResponse<int> AddUpdate(MenuMasterModel model)
        {
            SqlTransaction lSqlTrans = GetNewTransaction();
            try
            {
                model.UserId = _jWTAuthManager.User.Id;
                var Result = AddUpdate(QueriesPaths.MenuMasterQueries.QAddUpdate, _helperService.AddDynamicParameters(model), lSqlTrans);
                if (!Result.IsSuccess)
                {
                    lSqlTrans.Rollback();
                    return SetResultStatus<int>(0, MessageStatus.Error, false); ;
                }

                if (Result.IsSuccess)
                {
                    // addupdate mapping table data 
                    if (model.UserTypeCodes != null && model.UserTypeCodes.Count > 0)
                    {
                        var mappinglist = MenuMasterMappingListByMenuId(model.Id);
                        if (model.Id > 0)
                        {
                            if (mappinglist != null && mappinglist.Count > 0)
                            {
                                var deleteMapList = mappinglist.Select(x => x.UserTypeCode).ToList().Where(z => !model.UserTypeCodes.Contains(z)).ToList();
                                foreach (var map in deleteMapList)
                                {
                                    DeleteMenuMapping(map, model.Id);
                                }
                            }
                        }
                        if (mappinglist != null && mappinglist.Count > 0)
                            model.UserTypeCodes = model.UserTypeCodes.Where(z => !mappinglist.Select(a => a.UserTypeCode).Contains(z)).ToList();
                        foreach (var item in model.UserTypeCodes)
                        {
                            MenuMasterMappingModel mapmodel = new MenuMasterMappingModel();
                            mapmodel.Id = 0;
                            mapmodel.MenuId = Result.Data;
                            mapmodel.UserTypeCode = item;
                            string[] spMapKeys = { "Id", "MenuId", "UserTypeCode" };
                            var mappingResult = AddUpdate(QueriesPaths.MenuMasterQueries.QMappingAddUpdate, _helperService.AddDynamicParameters(mapmodel, spMapKeys), lSqlTrans);
                            if (!mappingResult.IsSuccess)
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
                    lSqlTrans.Rollback();
                    return SetResultStatus<int>(Result.Data, MessageStatus.Error, Result.IsSuccess);
                }


            }
            catch (Exception ex)
            {
                lSqlTrans.Rollback();
                Log.Error(ex, CommonFunction.Errorstring("MenuMasterService.cs", "MenuMasterService"));
                return SetResultStatus<int>(0, MessageStatus.Error, false);

            }
        }

        public ServiceResponse<bool> Delete(int Id)
        {
            try
            {
                var deleteResult = ExecuteReturnData(QueriesPaths.MenuMasterQueries.QDelete, new { Id = Id, UserId = _jWTAuthManager.User.Id });
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
                Log.Error(ex, CommonFunction.Errorstring("MenuMasterService.cs", "Delete"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<MenuMasterModel> GetById(int Id)
        {
            try
            {
                var result = QueryFast<MenuMasterModel>(QueriesPaths.MenuMasterQueries.QGetById, new { Id = Id });
                if (result.Data != null)
                {
                    result.Data.UserTypeCodes = MenuMasterMappingListByMenuId(Id).Select(s => s.UserTypeCode).ToList();
                    return SetResultStatus<MenuMasterModel>(result.Data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<MenuMasterModel>(null, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("MenuMasterService.cs", "GetById"));
                return SetResultStatus<MenuMasterModel>(null, MessageStatus.Error, false, ex.Message);
            }
        }

        public ServiceResponse<List<MenuMasterViewModel>> GetList()
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<List<MenuMasterViewModel>> GetPagination(MenuMasterFilterModel filterModel)
        {
            try
            {
                var result = QueryList<MenuMasterViewModel>(QueriesPaths.MenuMasterQueries.QPagination, _helperService.AddDynamicParameters(filterModel));
                if (result.Data != null && result.Data.Count() > 0)
                {
                    return SetResultStatus<List<MenuMasterViewModel>>(result.Data, MessageStatus.Success, true, "", "", result.Data[0].TotalRows);
                }
                else
                {
                    return SetResultStatus<List<MenuMasterViewModel>>(null, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("MenuMasterService.cs", "GetPagination"));
                return SetResultStatus<List<MenuMasterViewModel>>(null, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<bool> UpdateStatus(int Id)
        {
            try
            {
                var result = ExecuteReturnData(QueriesPaths.MenuMasterQueries.QUpdateStatus, new { Id = Id, UserId = _jWTAuthManager.User.Id }); if (result.IsSuccess)
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
                Log.Error(ex, CommonFunction.Errorstring("MenuMasterService.cs", "UpdateStatus"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }

        private List<MenuMasterMappingModel> MenuMasterMappingListByMenuId(int menuId)
        {
            List<MenuMasterMappingModel> result = new List<MenuMasterMappingModel>();
            try
            {
                result = QueryList<MenuMasterMappingModel>(QueriesPaths.MenuMasterQueries.QMenuMasterMapping, new { menuId = menuId }).Data.ToList();
                return result;

            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("MenuMasterService.cs", "MenuMasterMappingListByMenuId"));
                return result;
            }
        }

        private ServiceResponse<bool> DeleteMenuMapping(int tpyeCode, int menuId)
        {
            try
            {
                var deleteResult = ExecuteReturnData(QueriesPaths.MenuMasterQueries.QMenuMasterMappingDelete, new { tpyeCode = tpyeCode, MenuId = menuId });
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
                Log.Error(ex, CommonFunction.Errorstring("MenuMasterService.cs", "DeleteMenuMapping"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<List<DynamicMenuModel>> GetDynamicMenuList(int UserTypeCode)
        {
            List<DynamicMenuModel> model = new List<DynamicMenuModel>();
            try
            {
                model = QueryList<DynamicMenuModel>(QueriesPaths.MenuMasterQueries.QDynamicMenu, new { UserTypeCode = UserTypeCode }).Data;
                model = model.Where(x => x.ParentId == null).Select(z => new DynamicMenuModel()
                {
                    IconClass = z.IconClass,
                    Id = z.Id,
                    HashChild = z.HashChild,
                    MenuName = z.MenuName,
                    Position = z.Position,
                    ParentId = z.ParentId,
                    ParentMenuName = z.ParentId > 0 ? z.ParentMenuName : string.Empty,
                    Pages = QueryList<DynamicPageMasterModel>(QueriesPaths.MenuMasterQueries.QDynamicPage, new { menuid = z.Id, UserTypeIdCode = UserTypeCode })
                                 .Data.Select(s => new DynamicPageMasterModel()
                                 {
                                     Id = s.Id,
                                     MenuId = s.MenuId,
                                     Name = s.Name,
                                     Icon = s.Icon,
                                     MenuName = s.MenuName,
                                     PageUrl = s.PageUrl,
                                 }).ToList(),
                    ChildParentMenuList = z.Id > 0 ? GetChildren(model, z.Id, UserTypeCode) : new List<DynamicMenuModel>()
                })
                            .ToList();
                if (model != null && model.Count > 0)
                {
                    return SetResultStatus<List<DynamicMenuModel>>(model, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<List<DynamicMenuModel>>(model, MessageStatus.NoRecord, true);
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("MenuMasterService.cs", "GetDynamicMenuList"));
                return SetResultStatus<List<DynamicMenuModel>>(null, MessageStatus.Error, false, ex.Message);
            }
        }
        private List<DynamicMenuModel> GetChildren(List<DynamicMenuModel> comments, int parentId, int userTypeCode)
        {
            try
            {
                return comments
                                    .Where(c => c.ParentId == parentId)
                                    .Select(z => new DynamicMenuModel
                                    {
                                        IconClass = z.IconClass,
                                        Id = z.Id,
                                        HashChild = z.HashChild,
                                        MenuName = z.MenuName,
                                        Position = z.Position,
                                        ParentId = z.ParentId,
                                        ParentMenuName = z.ParentId > 0 ? z.ParentMenuName : string.Empty,
                                        Pages = QueryList<DynamicPageMasterModel>(QueriesPaths.MenuMasterQueries.QDynamicPage, new { menuid = z.Id, UserTypeIdCode = userTypeCode })
                                        .Data.Select(s => new DynamicPageMasterModel()
                                        {
                                            Id = s.Id,
                                            MenuId = s.MenuId,
                                            Name = s.Name,
                                            Icon = s.Icon,
                                            MenuName = s.MenuName,
                                            PageUrl = s.PageUrl,
                                        }).ToList(),
                                        ChildParentMenuList = GetChildren(comments, z.Id, userTypeCode)
                                    })
                                    .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
