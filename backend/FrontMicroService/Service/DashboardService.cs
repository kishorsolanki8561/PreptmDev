using CommonService.JWT;
using CommonService.Other;
using FrontMicroService.IService;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using ModelService.Model.Front;
using ModelService.Model.Translation;
using Newtonsoft.Json;
using Serilog;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace FrontMicroService.Service
{
    public class DashboardService : UtilityManager, IDashboardService
    {
        private readonly HelperService _helperService;
        private readonly JWTAuthManager _jWTAuthManager;
        private readonly IMemoryCache _cache;

        // Shared entry options: 5-minute absolute expiration, size = 1 per entry
        private static readonly MemoryCacheEntryOptions _cacheOpts =
            new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                .SetSize(1);

        public DashboardService(HelperService helperService, JWTAuthManager jWTAuthManager, IMemoryCache cache)
        {
            _helperService = helperService;
            _jWTAuthManager = jWTAuthManager;
            _cache = cache;
        }
        public ServiceResponse<DashboardModel> GetDashboardRecentAndPopularPostList(int pageSize)
        {
            try
            {
                var user = _jWTAuthManager.FrontUser;
                int? UserId = user is null ? null : user.Id;
                DashboardModel dashboard = new DashboardModel();
                var RecentPost = QueryList<DashboardRecentAndPopularPostModel>(@"Sp_DashboardRecentPostList @PageSize,@UserId", new { PageSize = pageSize, UserId = UserId }).Data;
                if (RecentPost != null && RecentPost.Count > 0)
                {
                    dashboard.RecentPosts = RecentPost;
                }
                var PopularPost = QueryList<DashboardRecentAndPopularPostModel>(@"Sp_DashboardPopularPostList @PageSize,@UserId", new { PageSize = pageSize, UserId = UserId }).Data;
                if (PopularPost != null && PopularPost.Count > 0)
                {
                    dashboard.PopularPosts = PopularPost;
                }

                if (dashboard != null)
                {
                    return SetResultStatus<DashboardModel>(dashboard, MessageStatus.Success, true, "", "");
                }
                else
                {
                    return SetResultStatus<DashboardModel>(new DashboardModel(), MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("DashboardService.cs", "GetDashboardRecentAndPopularPostList"));
                return SetResultStatus<DashboardModel>(new DashboardModel(), MessageStatus.Error, false);
            }
        }
        public async Task<ServiceResponse<List<DashboardRecentAndPopularPostModel>>> GetFrontDashboardList(DashboradFilterModel filterModel)
        {
            try
            {
                ServiceResponse<List<DashboardRecentAndPopularPostModel>> serviceResponse = new ServiceResponse<List<DashboardRecentAndPopularPostModel>>();
                filterModel.IsActive = 1;
                var user = _jWTAuthManager.FrontUser;
                filterModel.UserId = user is null ? null : user.Id;
                filterModel.LanguageCode = _jWTAuthManager.LanguageCode ?? "en";
                var total = 0;

                var Type = new Type[] { typeof(DashboardRecentAndPopularPostModel), typeof(FrontModuleTextModel) };

                var result = await GetMultipleDatasetAsync(@"Sp_FrontDashboardPagination @Page,@PageSize,@OrderBy,@OrderByAsc,@FromDate,@ToDate,@Title,@DepartmentId,@QualificationId,@JobDesignationId,@CategorySlug,@SubCategorySlug,@BlockTypeSlug,@EligibilityId,@CategoryId,@UserId,@SearchText,@LanguageCode,@StateId,@IsPrivate", _helperService.AddDynamicParameters(filterModel), Type);

                if (result is not null)
                {
                    serviceResponse.Data = GetListingType<DashboardRecentAndPopularPostModel>(result).ToList();

                    var moduleTexts = GetListingType<FrontModuleTextModel>(result).FirstOrDefault();

                    serviceResponse.OtherData = new Dictionary<string, object>()
                                {
                                    {"ModuleText",moduleTexts.ModuleText },
                                    {"Description",moduleTexts.Description },
                                };
                    if (serviceResponse.Data.Any())
                        total = (int)serviceResponse.Data[0].TotalRows;
                }

                return SetResultStatus<List<DashboardRecentAndPopularPostModel>>(serviceResponse.Data, MessageStatus.Success, true, "", "", total, 0, serviceResponse.OtherData);

            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("DashboardService.cs", "GetFrontDashboardList"));
                return SetResultStatus<List<DashboardRecentAndPopularPostModel>>(null, MessageStatus.Error, false);
            }
        }
        public ServiceResponse<List<DashboardRecentAndPopularPostModel>> GetDashboardSearchFilter(DashboradSearchFilterModel filterModel)
        {
            try
            {
                var user = _jWTAuthManager.FrontUser;
                filterModel.UserId = user is null ? null : user.Id;
                var result = QueryList<DashboardRecentAndPopularPostModel>(@"Sp_DashboardSearchFilter @SearchText,@StateId,@PageSize,@Page,@UserId", _helperService.AddDynamicParameters(filterModel));
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
                Log.Error(ex, CommonFunction.Errorstring("DashboardService.cs", "GetDashboardSearchFilter"));
                return SetResultStatus<List<DashboardRecentAndPopularPostModel>>(null, MessageStatus.Error, false);
            }
        }
        public async Task<ServiceResponse<DashboardModel>> GetDashboardData(int pageSize)
        {
            try
            {
                var user = _jWTAuthManager.FrontUser;
                int? UserId = user is null ? null : user.Id;

                // Cache anonymous home-page data — most expensive query on the site
                string lang = _jWTAuthManager.LanguageCode ?? "en";
                if (UserId == null)
                {
                    string cacheKey = $"dashboard|{lang}|{pageSize}";
                    if (_cache.TryGetValue(cacheKey, out ServiceResponse<DashboardModel> cached))
                        return cached;

                    var fresh = await FetchDashboardData(pageSize, UserId, lang);
                    _cache.Set(cacheKey, fresh, _cacheOpts);
                    return fresh;
                }

                return await FetchDashboardData(pageSize, UserId, lang);
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("DashboardService.cs", "GetDashboardData"));
                return SetResultStatus<DashboardModel>(null, MessageStatus.Error, false, ex.Message, ex.InnerException?.Message ?? "", 0, StatusCodes.Status500InternalServerError, null);
            }
        }

        private async Task<ServiceResponse<DashboardModel>> FetchDashboardData(int pageSize, int? UserId, string lang)
        {
            try
            {
                DashboardModel dashboard = new DashboardModel();


                //var data = QueryMultiple(@"Sp_GetDashboardData @PageSize,@UserId,@LanguageCode", new { PageSize = pageSize, UserId = UserId, LanguageCode = _jWTAuthManager.LanguageCode ?? "en" }).Data;


                var types = new (Type Type, string Alias)[]
                    {
                        (typeof(DashboardRecentAndPopularPostModel), "Popular"),
                        (typeof(DashboardRecentAndPopularPostModel), "Recent"),
                        (typeof(DashboardRecentAndPopularPostModel), "Recruitments"),
                        (typeof(DashboardRecentAndPopularPostModel), "Admission"),
                        (typeof(DashboardRecentAndPopularPostModel), "Exam"),
                        (typeof(DashboardRecentAndPopularPostModel), "Paper"),
                        (typeof(DashboardRecentAndPopularPostModel), "Result"),
                        (typeof(DashboardRecentAndPopularPostModel), "Syllabus"),
                        (typeof(DashboardRecentAndPopularPostModel), "AdmitCards"),
                        (typeof(DashboardRecentAndPopularPostModel), "Scheme"),
                        (typeof(DashboardRecentAndPopularPostModel), "UpComming"),
                        (typeof(DashboardRecentAndPopularPostModel), "Expired"),
                        (typeof(DashboardRecentAndPopularPostModel), "PrivateRecruitment"),

                    };

                var prams = new { PageSize = pageSize, UserId = UserId, LanguageCode = lang };

                var result = await GetMultipleDatasetAsync(@"Sp_GetDashboardData @PageSize,@UserId,@LanguageCode", _helperService.AddDynamicParameters(prams), types);

                if (result is not null)
                {
                    dashboard.PopularPosts = GetListingType<DashboardRecentAndPopularPostModel>(result, "Popular").ToList();
                    dashboard.RecentPosts = GetListingType<DashboardRecentAndPopularPostModel>(result, "Recent").ToList();
                    dashboard.Recruitments = GetListingType<DashboardRecentAndPopularPostModel>(result, "Recruitments").ToList();
                    dashboard.Admissions = GetListingType<DashboardRecentAndPopularPostModel>(result, "Admission").ToList();
                    dashboard.Exams = GetListingType<DashboardRecentAndPopularPostModel>(result, "Exam").ToList();
                    dashboard.Papers = GetListingType<DashboardRecentAndPopularPostModel>(result, "Paper").ToList();
                    dashboard.Results = GetListingType<DashboardRecentAndPopularPostModel>(result, "Result").ToList();
                    dashboard.Syllabus = GetListingType<DashboardRecentAndPopularPostModel>(result, "Syllabus").ToList();
                    dashboard.AdmitCards = GetListingType<DashboardRecentAndPopularPostModel>(result, "AdmitCards").ToList();
                    dashboard.Schemes = GetListingType<DashboardRecentAndPopularPostModel>(result, "Scheme").ToList();
                    dashboard.UpComingPosts = GetListingType<DashboardRecentAndPopularPostModel>(result, "UpComming").ToList();
                    dashboard.ExpiredSoonPosts = GetListingType<DashboardRecentAndPopularPostModel>(result, "Expired").ToList();
                    dashboard.PrivateRecruitments = GetListingType<DashboardRecentAndPopularPostModel>(result, "PrivateRecruitment").ToList();
                }

                if (dashboard is not null)
                {
                    return SetResultStatus<DashboardModel>(dashboard, MessageStatus.Success, true, "", "", 0, StatusCodes.Status200OK, null);
                }
                else
                {
                    return SetResultStatus<DashboardModel>(null, MessageStatus.NoRecord, true, "", "", 0, StatusCodes.Status404NotFound, null);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("DashboardService.cs", "FetchDashboardData"));
                return SetResultStatus<DashboardModel>(null, MessageStatus.Error, false, ex.Message, ex.InnerException?.Message ?? "", 0, StatusCodes.Status500InternalServerError, null);
            }
        }
        public ServiceResponse<List<string>> GetPopularBySearchText(int numberOfRecord, string SearchText)
        {
            try
            {
                var popularTextData = QueryList<string>(@"Sp_PopularBySearchText @PageSize,@SearchText", new { PageSize = numberOfRecord, SearchText = SearchText });
                if (popularTextData.Data is not null && popularTextData.Data.Count() > 0)
                {
                    return SetResultStatus<List<string>>(popularTextData.Data, MessageStatus.Success, true, "", "");
                }
                else
                {
                    return SetResultStatus<List<string>>(new List<string>(), MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("DashboardService.cs", "GetPopularBySearchText"));
                return SetResultStatus<List<string>>(new List<string>(), MessageStatus.Error, false);
            }
        }
        public ServiceResponse<List<BannerListModel>> GetBannersByPageSize(int numberOfRecord)
        {
            string cacheKey = $"banners|{numberOfRecord}";
            if (_cache.TryGetValue(cacheKey, out ServiceResponse<List<BannerListModel>> cached))
                return cached;
            try
            {
                var popularTextData = QueryList<BannerListModel>(@"Sp_BannerMasterByPageSizeOnFront @PageSize", new { PageSize = numberOfRecord });
                ServiceResponse<List<BannerListModel>> result;
                if (popularTextData.Data is not null && popularTextData.Data.Count() > 0)
                    result = SetResultStatus<List<BannerListModel>>(popularTextData.Data, MessageStatus.Success, true, "", "");
                else
                    result = SetResultStatus<List<BannerListModel>>(new List<BannerListModel>(), MessageStatus.NoRecord, true);

                _cache.Set(cacheKey, result, _cacheOpts);
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("DashboardService.cs", "GetBannersByPageSize"));
                return SetResultStatus<List<BannerListModel>>(new List<BannerListModel>(), MessageStatus.Error, false);
            }
        }

        public ServiceResponse<string> GetSiteMap(string langCode = "")
        {
            try
            {
                var siteMapData = QueryList<FrontSiteMapModel>(@"Sp_FrontGetSiteMap @langCode", new { langCode = langCode });
                if (siteMapData.Data is not null && siteMapData.Data.Count() > 0)
                {
                    string baseUrl = langCode == "hi"
                        ? "https://www.preptm.com/hi/"
                        : "https://www.preptm.com/";

                    StringBuilder sb = new StringBuilder();
                    sb.Append("<?xml version='1.0' encoding='UTF-8' ?><urlset xmlns='http://www.sitemaps.org/schemas/sitemap/0.9'>");
                    foreach (var page in siteMapData.Data)
                    {
                        var istdate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(page.ModifiedDate),
                        TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                        string mDate = istdate.ToString("yyyy-MM-ddTHH:mm:sszzz");
                        // Normalize slug to match URL-normalization middleware rules:
                        // lowercase + no trailing slash — prevents sitemap entries that 301-redirect.
                        string slug = (page.SlugUrl?.TrimStart('/') ?? string.Empty)
                            .ToLowerInvariant()
                            .TrimEnd('/');
                        string url = baseUrl + slug;
                        sb.Append("<url><loc>" + url + "</loc><lastmod>" + mDate + "</lastmod><changefreq>" + page.ChangeFreq + "</changefreq><priority>" + page.Priority + "</priority></url>");
                    }
                    sb.Append("</urlset>");
                    return SetResultStatus<string>(sb.ToString(), MessageStatus.Success, true, "", "");
                }
                else
                {
                    return SetResultStatus<string>(string.Empty, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("DashboardService.cs", "GetSiteMap"));
                return SetResultStatus<string>(string.Empty, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<string> GetSiteMapIndex()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<?xml version='1.0' encoding='UTF-8' ?>");
                sb.Append("<sitemapindex xmlns='http://www.sitemaps.org/schemas/sitemap/0.9'>");
                sb.Append("<sitemap><loc>https://www.preptm.com/sitemap.xml</loc></sitemap>");
                sb.Append("<sitemap><loc>https://www.preptm.com/hi/sitemap.xml</loc></sitemap>");
                sb.Append("</sitemapindex>");
                return SetResultStatus<string>(sb.ToString(), MessageStatus.Success, true, "", "");
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("DashboardService.cs", "GetSiteMapIndex"));
                return SetResultStatus<string>(string.Empty, MessageStatus.Error, false);
            }
        }
    }
}
