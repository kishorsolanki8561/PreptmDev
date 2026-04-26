using CommonService.JWT;
using CommonService.Other;
using FrontMicroService.IService;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Caching.Memory;
using ModelService.Model;
using ModelService.Model.MastersModel;
using ModelService.Model.Translation;
using ModelService.OtherModels;
using Newtonsoft.Json;
using Serilog;

namespace FrontMicroService.Service
{
    public class RecruitmentService : UtilityManager, IRecruitmentService
    {
        private readonly HelperService _helperService;
        private readonly JWTAuthManager _jWTAuthManager;
        private readonly IBlockContentService _blockContentService;
        private readonly IMemoryCache _cache;

        private static readonly MemoryCacheEntryOptions _cacheOpts =
            new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                .SetSize(1);

        public RecruitmentService(HelperService helperService, IBlockContentService blockContentService, JWTAuthManager jWTAuthManager, IMemoryCache cache)
        {
            _helperService = helperService;
            _blockContentService = blockContentService;
            _jWTAuthManager = jWTAuthManager;
            _cache = cache;
        }
        public ServiceResponse<List<DashboardRecentAndPopularPostModel>> GetFrontRecruitmentList(RecruitmentFilterModel filterModel)
        {
            try
            {
                filterModel.IsActive = 1;
                var result = QueryList<DashboardRecentAndPopularPostModel>(@"Sp_FrontRecruitmentPagination @Page,@PageSize,@Search,@OrderBy,@OrderByAsc,@FromDate,@ToDate,@Title,@DepartmentId,@QualificationId,@JobDesignationId,@CategoryId,@SubCategoryId,@UserId,@RecruitmentId", _helperService.AddDynamicParameters(filterModel));
                if (result.Data != null && result.Data.Count() > 0)
                {
                    return SetResultStatus<List<DashboardRecentAndPopularPostModel>>(result.Data, MessageStatus.Success, true, "", "", (int)result.Data[0].TotalRows);
                }
                else
                {
                    return SetResultStatus<List<DashboardRecentAndPopularPostModel>>(null, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("RecruitmentService.cs", "GetFrontRecruitmentList"));
                return SetResultStatus<List<DashboardRecentAndPopularPostModel>>(null, MessageStatus.Error, false);
            }
        }
        public ServiceResponse<object> GetModuleWiseDataByIdAndSlug(int? id, string? slugUrl, bool isRecruitment = false)
        {
            RecruitmentFrontViewModel resultModel = new RecruitmentFrontViewModel();
            try
            {
                var user = _jWTAuthManager.FrontUser is null ? null : _jWTAuthManager.FrontUser;
                if (isRecruitment)
                {
                    var recruitmentResult = QueryMultiple(@"Sp_FrontRecruitmentDetailsOfIdAndSlug @slugUrl,@Id,@UserId,@LanguageCode", new { SlugUrl = slugUrl, Id = id, UserId = user?.Id, LanguageCode = _jWTAuthManager.LanguageCode ?? "en" });

                    if (recruitmentResult.Data is not null && SPResultHandler.GetCount(recruitmentResult.Data[0]) > 0)
                    {
                        resultModel = SPResultHandler.GetObject<RecruitmentFrontViewModel>(recruitmentResult.Data[0]) ?? null;
                        if (resultModel is not null && SPResultHandler.GetCount(recruitmentResult.Data[1]) > 0)
                            resultModel.Designation = SPResultHandler.GetList<RecruitmentJobDesignationModel>(recruitmentResult.Data[1]).Select(s => s.Name).ToList();
                        if (resultModel is not null && SPResultHandler.GetCount(recruitmentResult.Data[2]) > 0)
                            resultModel.Qualification = SPResultHandler.GetList<RecruitmentQualificationModel>(recruitmentResult.Data[2]).Select(s => s.Title).ToList();
                        if (resultModel is not null && SPResultHandler.GetCount(recruitmentResult.Data[3]) > 0)
                            resultModel.Documents = SPResultHandler.GetList<RecruitmentDocumentModel>(recruitmentResult.Data[3]).Select(s => s.Path).ToList();
                        if (resultModel is not null && SPResultHandler.GetCount(recruitmentResult.Data[4]) > 0)
                            resultModel.HowToApply = SPResultHandler.GetList<RecruitmentFrontHowToApplyAndQuickLinkLookup>(recruitmentResult.Data[4]).ToList();
                        if (resultModel is not null && SPResultHandler.GetCount(recruitmentResult.Data[5]) > 0)
                            resultModel.OtherLinksList = SPResultHandler.GetList<RecruitmentFrontHowToApplyAndQuickLinkLookup>(recruitmentResult.Data[5]).ToList();
                        if (resultModel is not null && SPResultHandler.GetCount(recruitmentResult.Data[6]) > 0)
                            resultModel.RelatedCatSubCat = SPResultHandler.GetList<DashboardRecentAndPopularPostModel>(recruitmentResult.Data[6]).ToList();
                        if (resultModel is not null && SPResultHandler.GetCount(recruitmentResult.Data[7]) > 0)
                            resultModel.RelatedBlockContent = SPResultHandler.GetList<DashboardRecentAndPopularPostModel>(recruitmentResult.Data[7]).ToList();
                        return SetResultStatus<object>(resultModel, MessageStatus.Success, true);
                    }
                    else
                    {
                        return SetResultStatus<object>(resultModel, MessageStatus.NoRecord, true);
                    }
                }
                else
                {
                    BlockContentsFrontViewModel resultBlockModel = new BlockContentsFrontViewModel();
                    var blockContentsResult = QueryMultiple(@"Sp_FrontBlockContentsDetailsOfIdAndSlug @slugUrl,@Id,@UserId,@LanguageCode", new { SlugUrl = slugUrl, Id = id, UserId = user?.Id, LanguageCode = _jWTAuthManager.LanguageCode ?? "en" });

                    if (blockContentsResult.Data is not null && SPResultHandler.GetCount(blockContentsResult.Data[0]) > 0)
                    {
                        resultBlockModel = SPResultHandler.GetObject<BlockContentsFrontViewModel>(blockContentsResult.Data[0]) ?? null;
                        if (resultBlockModel is not null && SPResultHandler.GetCount(blockContentsResult.Data[1]) > 0)
                            resultBlockModel.Documents = SPResultHandler.GetList<BlockContentAttachmentModel>(blockContentsResult.Data[1]).Select(s => s.Path).ToList();
                        if (resultBlockModel is not null && SPResultHandler.GetCount(blockContentsResult.Data[2]) > 0)
                            resultBlockModel.HowToApply = SPResultHandler.GetList<BlockContentFrontHowToApplyAndQuickLinkLookup>(blockContentsResult.Data[2]).ToList();
                        if (resultBlockModel is not null && SPResultHandler.GetCount(blockContentsResult.Data[3]) > 0)
                            resultBlockModel.OtherLinksList = SPResultHandler.GetList<BlockContentFrontHowToApplyAndQuickLinkLookup>(blockContentsResult.Data[3]).ToList();
                        if (resultBlockModel is not null && SPResultHandler.GetCount(blockContentsResult.Data[4]) > 0)
                            resultBlockModel.RelatedRecruitment = SPResultHandler.GetList<DashboardRecentAndPopularPostModel>(blockContentsResult.Data[4]).ToList();
                        if (resultBlockModel is not null && SPResultHandler.GetCount(blockContentsResult.Data[5]) > 0)
                            resultBlockModel.RelatedBlockContent = SPResultHandler.GetList<DashboardRecentAndPopularPostModel>(blockContentsResult.Data[5]).ToList();
                        return SetResultStatus<object>(resultBlockModel, MessageStatus.Success, true);
                    }
                    else
                    {
                        return SetResultStatus<object>(resultBlockModel, MessageStatus.NoRecord, true);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("RecruitmentService.cs", "GetModuleWiseDataByIdAndSlug"));
                return SetResultStatus<object>(null, MessageStatus.Error, false, ex.Message);
            }
        }

        public async Task<ServiceResponse<object>> GetRecruitmentDetailsOfIdAndSlug(int? id, string? slugUrl, bool IsAdminView = false)
        {
            IsAdminView = _jWTAuthManager.RequestUrl == "admin.preptm.com";
            var user = _jWTAuthManager.FrontUser;
            string lang = _jWTAuthManager.LanguageCode ?? "en";

            // Cache anonymous (public) requests — skip cache for admin preview or logged-in users
            if (user == null && !IsAdminView)
            {
                string cacheKey = $"recruitment|{lang}|{slugUrl}|{id}";
                if (_cache.TryGetValue(cacheKey, out ServiceResponse<object> cached))
                    return cached;
                var fresh = await FetchRecruitmentDetails(id, slugUrl, IsAdminView, null, lang);
                if (fresh.Data != null) _cache.Set(cacheKey, fresh, _cacheOpts);
                return fresh;
            }

            return await FetchRecruitmentDetails(id, slugUrl, IsAdminView, user, lang);
        }

        private async Task<ServiceResponse<object>> FetchRecruitmentDetails(int? id, string? slugUrl, bool isAdminView, dynamic? user, string lang)
        {
            RecruitmentFrontViewModel resultModel = new RecruitmentFrontViewModel();
            try
            {
                var prams = new { SlugUrl = slugUrl, Id = id, UserId = (int?)(user?.Id), LanguageCode = lang, IsAdminView = isAdminView };

                var types = new (Type Type, string Alias)[]
                {
                    (typeof(RecruitmentFrontViewModel), "RecruitmentFrontViewModel"),
                    (typeof(RecruitmentJobDesignationModel), "RecruitmentJobDesignationModel"),
                    (typeof(RecruitmentQualificationModel), "RecruitmentQualificationModel"),
                    (typeof(RecruitmentDocumentModel), "RecruitmentDocumentModel"),
                    (typeof(RecruitmentFrontHowToApplyAndQuickLinkLookup), "HowToApply"),
                    (typeof(RecruitmentFrontHowToApplyAndQuickLinkLookup), "OtherLinksList"),
                    (typeof(DashboardRecentAndPopularPostModel), "RelatedCatSubCat"),
                    (typeof(DashboardRecentAndPopularPostModel), "RelatedBlockContent"),
                    (typeof(RecruitmentFrontFAQLookup), "RecruitmentFrontFAQLookup"),
                    (typeof(RecruitmentTagsModel), "RecruitmentTagsModel"),
                    (typeof(DashboardRecentAndPopularPostModel), "Articles")
                };

                var result = await GetMultipleDatasetAsync(@"Sp_FrontRecruitmentDetailsOfIdAndSlug @slugUrl,@Id,@UserId,@LanguageCode,@IsAdminView", _helperService.AddDynamicParameters(prams), types);

                resultModel = GetListingType<RecruitmentFrontViewModel>(result, "RecruitmentFrontViewModel").FirstOrDefault();
                if (resultModel == null)
                    return SetResultStatus<object>(null, MessageStatus.NoRecord, true);

                resultModel.Designation    = GetListingType<RecruitmentJobDesignationModel>(result, "RecruitmentJobDesignationModel").Select(s => s.Name).ToList();
                resultModel.Qualification  = GetListingType<RecruitmentQualificationModel>(result, "RecruitmentQualificationModel").Select(s => s.Title).ToList();
                resultModel.Documents      = GetListingType<RecruitmentDocumentModel>(result, "RecruitmentDocumentModel").Select(s => s.Path).ToList();
                resultModel.HowToApply     = GetListingType<RecruitmentFrontHowToApplyAndQuickLinkLookup>(result, "HowToApply").ToList();
                resultModel.OtherLinksList = GetListingType<RecruitmentFrontHowToApplyAndQuickLinkLookup>(result, "OtherLinksList").ToList();
                resultModel.RelatedCatSubCat    = GetListingType<DashboardRecentAndPopularPostModel>(result, "RelatedCatSubCat").ToList();
                resultModel.RelatedBlockContent = GetListingType<DashboardRecentAndPopularPostModel>(result, "RelatedBlockContent").ToList();
                resultModel.FAQs    = GetListingType<RecruitmentFrontFAQLookup>(result, "RecruitmentFrontFAQLookup").ToList();
                resultModel.Tags    = GetListingType<RecruitmentTagsModel>(result, "RecruitmentTagsModel").ToList();
                resultModel.Articles = GetListingType<DashboardRecentAndPopularPostModel>(result, "Articles").ToList();

                return SetResultStatus<object>(resultModel, MessageStatus.Success, true);
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("RecruitmentService.cs", "FetchRecruitmentDetails"));
                return SetResultStatus<object>(null, MessageStatus.Error, false, ex.Message);
            }
        }

        public ServiceResponse<DepartmentFrontViewModel> GetDepartmentDataByIdAndSlug(int? id, string? slugUrl)
        {
            try
            {
                DepartmentFrontViewModel resultModel = new DepartmentFrontViewModel();
                var user = _jWTAuthManager.FrontUser is null ? null : _jWTAuthManager.FrontUser;
                var deptresult = QueryMultiple(@"Sp_DepartmentDetailsofIdAndSlug @slugUrl,@Id,@LanguageCode", new { SlugUrl = slugUrl, Id = id, LanguageCode=_jWTAuthManager.LanguageCode ?? "en" });
                if (deptresult.Data is not null && SPResultHandler.GetCount(deptresult.Data[0]) > 0)
                {
                    resultModel = SPResultHandler.GetObject<DepartmentFrontViewModel>(deptresult.Data[0]) ?? null;
                    if (resultModel is not null && SPResultHandler.GetCount(deptresult.Data[1]) > 0)
                        resultModel.RelatedData = SPResultHandler.GetList<DashboardRecentAndPopularPostModel>(deptresult.Data[1]).ToList();
                    return SetResultStatus<DepartmentFrontViewModel>(resultModel, MessageStatus.Success, true);

                }
              
                else
                {
                    return SetResultStatus<DepartmentFrontViewModel>(null, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("RecruitmentService.cs", "GetDepartmentDataByIdAndSlug"));
                return SetResultStatus<DepartmentFrontViewModel>(null, MessageStatus.Error, false, ex.Message);
            }
        }
    }

}
