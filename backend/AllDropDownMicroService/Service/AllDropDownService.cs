using CommonService.JWT;
using CommonService.Other;
using ModelService.Model.MastersModel;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using static AllDropDownMicroService.Service.DdlKeysEnum;
using static CommonService.Other.HelperService;
using static CommonService.Other.UtilityManager;

namespace AllDropDownMicroService.Service
{
    public class AllDropDownService : UtilityManager, IAllDropDownServcie
    {
        private readonly JWTAuthManager _jWTAuthManager;
        public AllDropDownService(JWTAuthManager jWTAuthManager)
        {
            _jWTAuthManager = jWTAuthManager;
        }
        public UtilityManager.ServiceResponse<IDictionary<string, object>> AllDropDown(string keys, string userType = "", int? userId = 0)
        {
            ServiceResponse<IDictionary<string, object>> objReturn = new ServiceResponse<IDictionary<string, object>>();
            IDictionary<string, object> Data = new Dictionary<string, object>();
            try
            {
                if (!string.IsNullOrEmpty(keys))
                {
                    foreach (var item in keys.Split(','))
                    {
                        var temp = Data.Where(x => x.Key == item).FirstOrDefault().Key;
                        if (temp == null)
                        {
                            if (item.Equals(DdlKeys.ddlUserType.ToString()))
                            {
                                List<SelectListsItem> obj = dllGetUserType().Data;
                                Data.Add(item, obj != null ? obj : new List<SelectListsItem>());
                            }
                            else if (item.Equals(DdlKeys.ddlMenu.ToString()))
                            {
                                List<SelectListsItem> obj = dllGetMenu().Data;
                                Data.Add(item, obj != null ? obj : new List<SelectListsItem>());
                            }
                            else if (item.Equals(DdlKeys.ddlDepartment.ToString()))
                            {
                                List<SelectListsItem> obj = ddlDepartment().Data;
                                Data.Add(item, obj != null ? obj : new List<SelectListsItem>());
                            }
                            else if (item.Equals(DdlKeys.ddlPublisher.ToString()))
                            {
                                List<SelectListsItem> obj = ddlPublisher().Data;
                                Data.Add(item, obj != null ? obj : new List<SelectListsItem>());
                            }
                            else if (item.Equals(DdlKeys.ddlJobDesignation.ToString()))
                            {
                                List<SelectListsItem> obj = ddlJobDesignation().Data;
                                Data.Add(item, obj != null ? obj : new List<SelectListsItem>());
                            }
                            else if (item.Equals(DdlKeys.ddlQualification.ToString()))
                            {
                                List<SelectListsItem> obj = ddlQualification().Data;
                                Data.Add(item, obj != null ? obj : new List<SelectListsItem>());
                            }
                            else if (item.Equals(DdlKeys.ddlState.ToString()))
                            {
                                List<SelectListsItem> obj = ddlState().Data;
                                Data.Add(item, obj != null ? obj : new List<SelectListsItem>());
                            }
                            else if (item.Equals(DdlKeys.ddlCategory.ToString()))
                            {
                                List<SelectListsItem> obj = ddlCategory().Data;
                                Data.Add(item, obj != null ? obj : new List<SelectListsItem>());
                            }
                            else if (item.Equals(DdlKeys.ddlSubCategory.ToString()))
                            {
                                List<SelectListsItem> obj = GetSubCategory().Data;
                                Data.Add(item, obj != null ? obj : new List<SelectListsItem>());
                            }
                            else if (item.Equals(DdlKeys.ddlRecruitment.ToString()))
                            {
                                List<SelectListsItem> obj = ddlRecruitment().Data;
                                Data.Add(item, obj != null ? obj : new List<SelectListsItem>());
                            }
                            else if (item.Equals(DdlKeys.ddlBlockType.ToString()))
                            {
                                List<SelectListsItem> obj = ddlBlockType().Data;
                                Data.Add(item, obj != null ? obj : new List<SelectListsItem>());
                            }
                            else if (item.Equals(DdlKeys.ddlBlockTypeByForRecruitment.ToString()))
                            {
                                List<SelectListsItem> obj = GetBlockTypeByForRecruitment().Data;
                                Data.Add(item, obj != null ? obj : new List<SelectListsItem>());
                            }
                            else if (item.Equals(DdlKeys.ddlGroup.ToString()))
                            {
                                List<SelectListsItem> obj = ddlGroup().Data;
                                Data.Add(item, obj != null ? obj : new List<SelectListsItem>());
                            }
                            else if (item.Equals(DdlKeys.ddlLookupType.ToString()))
                            {
                                List<SelectListsItem> obj = ddlLookupType().Data;
                                Data.Add(item, obj != null ? obj : new List<SelectListsItem>());
                            }
                        }
                    }
                    objReturn.Data = Data;
                    objReturn.IsSuccess = true;
                    objReturn.StatusCode = StatusCodes.Status200OK;
                    objReturn.Message = MessageStatus.Success;

                    return objReturn;
                }
                return objReturn;
            }
            catch (Exception ex)
            {
                objReturn.Data = null;
                objReturn.IsSuccess = false;
                objReturn.StatusCode = StatusCodes.Status500InternalServerError;
                objReturn.Message = MessageStatus.Error;
                Log.Error(ex, CommonFunction.Errorstring("AllDropDownService.cs", "AllDropDown"));
                return objReturn;
            }
        }


        #region AllFuctions
        private ServiceResponse<List<SelectListsItem>> dllGetUserType()
        {
            try
            {
                var result = QueryList<SelectListsItem>(@"select Id as Value, TypeName as Text from UserType where IsActive=1 And Isdelete =0 order by typeName", null);
                if (result.Data.Count > 0)
                {
                    return SetResultStatus<List<SelectListsItem>>(result.Data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<List<SelectListsItem>>(result.Data, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("AllDropDownService.cs", "dllGetUserType"));
                return SetResultStatus<List<SelectListsItem>>(null, MessageStatus.Error, false, ex.Message);
            }
        }

        private ServiceResponse<List<SelectListsItem>> dllGetMenu()
        {
            try
            {
                List<MenuMasterModel> result = QueryList<MenuMasterModel>(@"select * from MenuMaster where IsActive=1 And Isdelete =0 order by DisplayName", null).Data;
                if (result != null && result.Count > 0)
                {
                    var data = result.Select(s => new SelectListsItem()
                    {
                        Value = s.Id,
                        Text = s.DisplayName + (s.HashChild == false ? " - Page" : string.Empty)
                    }).ToList();
                    return SetResultStatus<List<SelectListsItem>>(data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<List<SelectListsItem>>(new List<SelectListsItem>(), MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("AllDropDownService.cs", "dllGetMenu"));
                return SetResultStatus<List<SelectListsItem>>(null, MessageStatus.Error, false, ex.Message);
            }
        }

        private ServiceResponse<List<SelectListsItem>> ddlState()
        {
            try
            {
                var result = QueryList<SelectListsItem>(@"select StateId as Value, StateName as Text from State order by StateName", null);
                if (result.Data.Count > 0)
                {
                    return SetResultStatus<List<SelectListsItem>>(result.Data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<List<SelectListsItem>>(new List<SelectListsItem>(), MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("AllDropDownService.cs", "ddlState"));
                return SetResultStatus<List<SelectListsItem>>(null, MessageStatus.Error, false, ex.Message);
            }
        }
        private ServiceResponse<List<SelectListsItem>> ddlDepartment()
        {
            try
            {
                List<DepartmentMasterModel> result = QueryList<DepartmentMasterModel>(@"select Id,Name,NameHindi from Vw_DepartmentMaster where isactive=1 order by [Name]", null).Data.ToList();
                if (result.Count > 0)
                {
                    var data = result.Select(s => new SelectListsItem()
                    {
                        Value = s.Id,
                        Text = _jWTAuthManager.LanguageCode == "hi" ? s.NameHindi : s.Name,
                        OtherData = new Dictionary<string, object>()
                        {
                            //{"NameHindi",s.NameHindi },
                            {"slugUrl",s.SlugUrl },
                        }
                    }).ToList();
                    return SetResultStatus<List<SelectListsItem>>(data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<List<SelectListsItem>>(new List<SelectListsItem>(), MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("AllDropDownService.cs", "ddlDepartment"));
                return SetResultStatus<List<SelectListsItem>>(null, MessageStatus.Error, false, ex.Message);
            }
        }

        private ServiceResponse<List<SelectListsItem>> ddlPublisher()
        {
            try
            {
                var result = QueryList<SelectListsItem>(@"select Id as Value, [Name] as Text from Vw_User order by [Name]", null);
                if (result.Data.Count > 0)
                {
                    return SetResultStatus<List<SelectListsItem>>(result.Data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<List<SelectListsItem>>(new List<SelectListsItem>(), MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("AllDropDownService.cs", "ddlPublisher"));
                return SetResultStatus<List<SelectListsItem>>(null, MessageStatus.Error, false, ex.Message);
            }
        }

        private ServiceResponse<List<SelectListsItem>> ddlJobDesignation()
        {
            try
            {
                List<JobDesignationMasterModel> result = QueryList<JobDesignationMasterModel>(@"select Id,[Name],NameHindi from Vw_JobDesignationMaster order by [Name]", null).Data.ToList();
                if (result.Count > 0)
                {
                    var data = result.Select(s => new SelectListsItem()
                    {
                        Value = s.Id,
                        Text = _jWTAuthManager.LanguageCode=="hi" ? s.NameHindi: s.Name,
                        OtherData = new Dictionary<string, object>()
                        {
                            //{"NameHindi",s.NameHindi }
                        }
                    }).ToList();
                    return SetResultStatus<List<SelectListsItem>>(data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<List<SelectListsItem>>(new List<SelectListsItem>(), MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("AllDropDownService.cs", "ddlJobDesignation"));
                return SetResultStatus<List<SelectListsItem>>(null, MessageStatus.Error, false, ex.Message);
            }
        }

        private ServiceResponse<List<SelectListsItem>> ddlQualification()
        {
            try
            {
                List<QualificationMasterModel> result = QueryList<QualificationMasterModel>(@"select Id,Title,TitleHindi from Vw_QualificationMaster order by Title", null).Data.ToList();
                if (result.Count > 0)
                {
                    var data = result.Select(s => new SelectListsItem()
                    {
                        Value = s.Id,
                        Text = s.Title,
                        OtherData = new Dictionary<string, object>()
                        {
                            {"NameHindi",s.Title }
                        }
                    }).ToList();
                    return SetResultStatus<List<SelectListsItem>>(data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<List<SelectListsItem>>(new List<SelectListsItem>(), MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("AllDropDownService.cs", "ddlQualification"));
                return SetResultStatus<List<SelectListsItem>>(null, MessageStatus.Error, false, ex.Message);
            }
        }

        private ServiceResponse<List<SelectListsItem>> ddlCategory()
        {
            try
            {
                List<CategoryMasterModel> result = QueryList<CategoryMasterModel>(@"select * from GetCategores", null).Data.ToList();
                if (result.Count > 0)
                {
                    var data = result.Select(s => new SelectListsItem()
                    {
                        Value = s.Id,
                        Text = _jWTAuthManager.LanguageCode == "hi" ? s.NameHindi : s.Name,
                        OtherData = new Dictionary<string, object>()
                        {
                            {"icon",s.Icon.ToAbsolutepathPath() },
                            //{"CategoryNameHindi",s.NameHindi },
                            {"slugUrl",s.SlugUrl },
                            //{"subCategoryCount",QueryFast<int>(@"select count(1) as SubCategoryCount from SubCategory where IsActive=1 and IsDelete =0 And CategoryId =@CategoryId",new{CategoryId=s.Id}).Data }
                        }
                    }).ToList();
                    return SetResultStatus<List<SelectListsItem>>(data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<List<SelectListsItem>>(new List<SelectListsItem>(), MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("AllDropDownService.cs", "ddlCategory"));
                return SetResultStatus<List<SelectListsItem>>(null, MessageStatus.Error, false, ex.Message);
            }
        }

        private ServiceResponse<List<SelectListsItem>> ddlBlockType()
        {
            try
            {
                List<BlockTypeModel> result = QueryList<BlockTypeModel>(@"select * from BlockType where isactive =1 and isdelete=0 and ForRecruitment =0  order by [Name]", null).Data;
                if (result.Count > 0)
                {
                    var data = result.Select(s => new SelectListsItem()
                    {
                        Value = s.Id,
                        Text = s.Name,
                        OtherData = new Dictionary<string, object>()
                        {
                            {"SlugUrl",s.SlugUrl },
                            {"NameHindi",s.NameHindi }

                        }
                    }).ToList();

                    return SetResultStatus<List<SelectListsItem>>(data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<List<SelectListsItem>>(new List<SelectListsItem>(), MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("AllDropDownService.cs", "ddlBlockType"));
                return SetResultStatus<List<SelectListsItem>>(null, MessageStatus.Error, false, ex.Message);
            }
        }

        private ServiceResponse<List<SelectListsItem>> ddlRecruitment()
        {
            try
            {
                var result = QueryList<SelectListsItem>(@"select Id as Value, [Title] as Text from Recruitment where isactive =1 and isdelete=0 order by [Title]", null);
                if (result.Data.Count > 0)
                {
                    return SetResultStatus<List<SelectListsItem>>(result.Data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<List<SelectListsItem>>(new List<SelectListsItem>(), MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("AllDropDownService.cs", "ddlRecruitment"));
                return SetResultStatus<List<SelectListsItem>>(null, MessageStatus.Error, false, ex.Message);
            }
        }
        private ServiceResponse<List<SelectListsItem>> ddlGroup()
        {
            try
            {
                List<GroupMasterModel> result = QueryList<GroupMasterModel>(@"select * from GroupMaster where isactive =1 and isdelete=0 order by [Name]", null).Data;
                if (result.Count > 0)
                {
                    var data = result.Select(s => new SelectListsItem()
                    {
                        Value = s.Id,
                        Text = _jWTAuthManager.LanguageCode == "hi" ? s.NameHindi : s.Name,
                        OtherData = new Dictionary<string, object>()
                        {
                            {"slugUrl",s.SlugUrl },
                        }
                    }).ToList();
                    return SetResultStatus<List<SelectListsItem>>(data, MessageStatus.Success, true);
                }

                else
                {
                    return SetResultStatus<List<SelectListsItem>>(new List<SelectListsItem>(), MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("AllDropDownService.cs", "ddlGroup"));
                return SetResultStatus<List<SelectListsItem>>(null, MessageStatus.Error, false, ex.Message);
            }
        }
        private ServiceResponse<List<SelectListsItem>> GetBlockTypeByForRecruitment()
        {
            try
            {
                List<BlockTypeModel> result = QueryList<BlockTypeModel>(@"select * from BlockType where isactive =1 and isdelete=0 and ForRecruitment =1 order by [Name]", null).Data;
                if (result.Count > 0)
                {
                    var data = result.Select(s => new SelectListsItem()
                    {
                        Value = s.Id,
                        Text = s.Name,
                        OtherData = new Dictionary<string, object>()
                        {
                            {"SlugUrl",s.SlugUrl },
                            {"NameHindi",s.NameHindi }

                        }
                    }).ToList();

                    return SetResultStatus<List<SelectListsItem>>(data, MessageStatus.Success, true, "", "", 0, StatusCodes.Status200OK, null);
                }
                else
                {
                    return SetResultStatus<List<SelectListsItem>>(null, MessageStatus.NoRecord, true, "", "", 0, StatusCodes.Status404NotFound, null);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("AllDropDownService.cs", "GetBlockTypeByForRecruitment"));
                return SetResultStatus<List<SelectListsItem>>(null, MessageStatus.Error, false, ex.Message);
            }
        }

        public ServiceResponse<List<SelectListsItem>> GetSubCategory(string SlugUrl = "", int cateCode = 0)
        {
            try
            {
                List<DDLSubCategoryModel> result = QueryList<DDLSubCategoryModel>(@"Sp_DDLSubCategoryByCategoryIdAndSlug @CategoryId,@SlugUrl", new { CategoryId = cateCode, SlugUrl = SlugUrl }).Data.ToList();
                if (result.Count > 0)
                {
                    var data = result.Select(s => new SelectListsItem()
                    {
                        Value = s.Id,
                        Text = s.Name,
                        OtherData = new Dictionary<string, object>()
                        {
                            {"icon",s.Icon.ToAbsolutepathPath() },
                            {"SubCategoryNameHindi",s.NameHindi },
                            {"slugUrl",s.SlugUrl }
                        }
                    }).ToList();
                    var categoryData = result[0];
                    var OtherData = new Dictionary<string, object>()
                         {
                            {"CategoryName",categoryData.CategoryName },
                            {"CategoryNameHindi",categoryData.CategoryNameHindi },
                         };
                    return SetResultStatus<List<SelectListsItem>>(data, MessageStatus.Success, true, "", "", 0, StatusCodes.Status200OK, OtherData);
                }
                return SetResultStatus<List<SelectListsItem>>(new List<SelectListsItem>(), MessageStatus.NoRecord, true);
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("AllDropDownService.cs", "GetSubCategory"));
                return SetResultStatus<List<SelectListsItem>>(null, MessageStatus.Error, false, ex.Message);
            }
        }
        
        public ServiceResponse<IDictionary<string, object>> GetDDLLookupDataByLookupTypeIdAndLookupType(string SlugUrl, string LookupType = "", string LookupTypeId = "")
        {
            ServiceResponse<IDictionary<string, object>> objReturn = new ServiceResponse<IDictionary<string, object>>();
            IDictionary<string, object> Data = new Dictionary<string, object>();
            try
            {
                if (!string.IsNullOrEmpty(SlugUrl))
                {
                    string[] slug = SlugUrl.Split('|', ',');
                    //string[] nums = Array.ConvertAll(SlugUrl.Split(','), string.);
                    foreach (var item in slug)
                    {
                        List<DDLLookupViewModel> result = QueryList<DDLLookupViewModel>(@"Sp_GetDDLLookupDataByLookupTypeIdAndLookupType @LookupTypeId,@LookupType,@SlugUrl", new { LookupTypeId = 0, LookupType = "", SlugUrl = item }).Data.ToList();
                        if (result.Count > 0)
                        {
                            var data = result.Select(s => new SelectListsItem()
                            {
                                Value = s.Id,
                                Text = _jWTAuthManager.LanguageCode == "hi" ? s.TitleHindi : s.Title,
                                OtherData = new Dictionary<string, object>()
                        {
                            {"slugUrl",s.Slug},
                            {"Description",_jWTAuthManager.LanguageCode == "hi" ? s.DescriptionHindi : s.Description},
                            {"LookupTypeName",s.LookupTypeName}
                        }
                            }).ToList();
                            Data.Add(item, data != null ? data : new List<SelectListsItem>());
                        }
                    }
                    return SetResultStatus<IDictionary<string, object>>(Data, MessageStatus.Success, true, "", "", 0, StatusCodes.Status200OK, null);

                }

                if (!string.IsNullOrEmpty(LookupType))
                {
                    string[] lookupType = LookupType.Split('|', ',');
                    //string[] nums = Array.ConvertAll(SlugUrl.Split(','), string.);
                    foreach (var item in lookupType)
                    {
                        List<DDLLookupViewModel> result = QueryList<DDLLookupViewModel>(@"Sp_GetDDLLookupDataByLookupTypeIdAndLookupType @LookupTypeId,@LookupType,@SlugUrl", new { LookupTypeId = 0, LookupType = lookupType, SlugUrl = "" }).Data.ToList();
                        {
                            var data = result.Select(s => new SelectListsItem()
                            {
                                Value = s.Id,
                                Text = _jWTAuthManager.LanguageCode == "hi" ? s.TitleHindi : s.Title,
                                OtherData = new Dictionary<string, object>()
                        {
                            {"slugUrl",s.Slug},
                            {"Description",_jWTAuthManager.LanguageCode == "hi" ? s.DescriptionHindi : s.Description},
                            {"LookupTypeName",s.LookupTypeName}
                        }
                            }).ToList();
                            Data.Add(item, data != null ? data : new List<SelectListsItem>());
                        }
                    }
                    return SetResultStatus<IDictionary<string, object>>(Data, MessageStatus.Success, true, "", "", 0, StatusCodes.Status200OK, null);

                }

                if (!string.IsNullOrEmpty(LookupTypeId))
                {
                    string[] Lookupsids = LookupTypeId.Split('|', ',');
                    foreach (var item in Lookupsids)
                    {
                        List<DDLLookupViewModel> result = QueryList<DDLLookupViewModel>(@"Sp_GetDDLLookupDataByLookupTypeIdAndLookupType @LookupTypeId,@LookupType,@SlugUrl", new { LookupTypeId = Convert.ToInt32(item), LookupType = "", SlugUrl = "" }).Data.ToList();
                        {
                            var data = result.Select(s => new SelectListsItem()
                            {
                                Value = s.Id,
                                Text = _jWTAuthManager.LanguageCode == "hi" ? s.TitleHindi : s.Title,
                                OtherData = new Dictionary<string, object>()
                        {
                            {"slugUrl",s.Slug},
                            {"Description",_jWTAuthManager.LanguageCode == "hi" ? s.DescriptionHindi : s.Description},
                            {"LookupTypeName",s.LookupTypeName}
                        }
                            }).ToList();
                            Data.Add(item, data != null ? data : new List<SelectListsItem>());
                        }
                    }
                    return SetResultStatus<IDictionary<string, object>>(Data, MessageStatus.Success, true, "", "", 0, StatusCodes.Status200OK, null);
                }

                return SetResultStatus<IDictionary<string, object>>(Data, MessageStatus.NoRecord, true);
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("AllDropDownService.cs", "GetDDLLookupDataByLookupTypeIdAndLookupType"));
                return SetResultStatus<IDictionary<string, object>>(null, MessageStatus.Error, false, ex.Message);
            }
        }
        private ServiceResponse<List<SelectListsItem>> ddlLookupType()
        {
            try
            {
                List<LookupTypeModel> result = QueryList<LookupTypeModel>(@"select Id,Title from LookupType where isactive =1 and isdelete=0 order by [Title]", null).Data;
                if (result.Count > 0)
                {
                    var data = result.Select(s => new SelectListsItem()
                    {
                        Value = s.Id,
                        Text = s.Title,
                        OtherData = new Dictionary<string, object>()
                        {
                            {"SlugUrl",s.Slug },
                            {"TitleHindi",s.TitleHindi }

                        }
                    }).ToList();

                    return SetResultStatus<List<SelectListsItem>>(data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<List<SelectListsItem>>(new List<SelectListsItem>(), MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("AllDropDownService.cs", "ddlLookupType"));
                return SetResultStatus<List<SelectListsItem>>(null, MessageStatus.Error, false, ex.Message);
            }
        }
        #endregion
    }
}
