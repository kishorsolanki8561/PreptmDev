using CommonService.JWT;
using CommonService.Other;
using FrontMicroService.IService;
using Microsoft.Extensions.Caching.Memory;
using ModelService.Model;
using ModelService.Model.Translation;
using Serilog;

namespace FrontMicroService.Service
{
    public class BlockContentService : UtilityManager, IBlockContentService
    {
        private readonly HelperService _helperService;
        private readonly JWTAuthManager _jWTAuthManager;
        private readonly IMemoryCache _cache;

        private static readonly MemoryCacheEntryOptions _cacheOpts =
            new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                .SetSize(1);

        public BlockContentService(HelperService helperService, JWTAuthManager jWTAuthManager, IMemoryCache cache)
        {
            _helperService = helperService;
            _jWTAuthManager = jWTAuthManager;
            _cache = cache;
        }
        public ServiceResponse<List<DashboardRecentAndPopularPostModel>> GetFrontBlockContentList(FrontBlockContentFilterModel filterModel)
        {
            try
            {
                filterModel.IsActive = 1;
                var result = QueryList<DashboardRecentAndPopularPostModel>(@"Sp_FrontBlockContentPagination @Page,@PageSize,@Search,@OrderBy,@OrderByAsc,@FromDate,@ToDate,@Title,@DepartmentId,@CategoryId,@SubCategoryId,@GroupId,@RecruitmentId,@UserId", _helperService.AddDynamicParameters(filterModel));
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
                Log.Error(ex, CommonFunction.Errorstring("BlockContentService.cs", "GetFrontBlockContentList"));
                return SetResultStatus<List<DashboardRecentAndPopularPostModel>>(null, MessageStatus.Error, false);
            }
        }
        public ServiceResponse<object> GetBlockContentDetailsOfIdAndSlug(int? id, string? slugUrl, bool IsAdminView = false)
        {
            IsAdminView = _jWTAuthManager.RequestUrl == "admin.preptm.com";
            var user = _jWTAuthManager.FrontUser;
            string lang = _jWTAuthManager.LanguageCode ?? "en";

            if (user == null && !IsAdminView)
            {
                string cacheKey = $"blockcontent|{lang}|{slugUrl}|{id}";
                if (_cache.TryGetValue(cacheKey, out ServiceResponse<object> cached))
                    return cached;
                var fresh = FetchBlockContentDetails(id, slugUrl, IsAdminView, null, lang);
                if (fresh.Data != null) _cache.Set(cacheKey, fresh, _cacheOpts);
                return fresh;
            }

            return FetchBlockContentDetails(id, slugUrl, IsAdminView, user, lang);
        }

        private ServiceResponse<object> FetchBlockContentDetails(int? id, string? slugUrl, bool isAdminView, dynamic? user, string lang)
        {
            BlockContentsFrontViewModel resultBlockModel = new BlockContentsFrontViewModel();
            try
            {
                var blockContentsResult = QueryMultiple(@"Sp_FrontBlockContentsDetailsOfIdAndSlug @slugUrl,@Id,@UserId,@LanguageCode,@IsAdminView", new { SlugUrl = slugUrl, Id = id, UserId = (int?)(user?.Id), LanguageCode = lang, IsAdminView = isAdminView });

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

                    if (resultBlockModel is not null && SPResultHandler.GetCount(blockContentsResult.Data[6]) > 0)
                        resultBlockModel.Recruitment = SPResultHandler.GetObject<BlockContentMapRecruitmentModel>(blockContentsResult.Data[6]) ?? null;
                    if (resultBlockModel is not null && SPResultHandler.GetCount(blockContentsResult.Data[7]) > 0)
                        resultBlockModel.FAQs = SPResultHandler.GetList<BlockContentFrontFAQLookup>(blockContentsResult.Data[7]).ToList();
                    if (resultBlockModel is not null && SPResultHandler.GetCount(blockContentsResult.Data[8]) > 0)
                        resultBlockModel.Tags = SPResultHandler.GetList<BlockContentsTagsModel>(blockContentsResult.Data[8]).ToList();
                    if (resultBlockModel is not null && SPResultHandler.GetCount(blockContentsResult.Data[9]) > 0)
                        resultBlockModel.Articles = SPResultHandler.GetList<DashboardRecentAndPopularPostModel>(blockContentsResult.Data[9]).ToList();
                    return SetResultStatus<object>(resultBlockModel, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<object>(null, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("BlockContentService.cs", "FetchBlockContentDetails"));
                return SetResultStatus<object>(null, MessageStatus.Error, false, ex.Message);
            }
        }
    }
}
