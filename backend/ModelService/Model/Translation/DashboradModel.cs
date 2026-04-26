using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelService.Model.Translation
{
    public class DashboardModel
    {
        public List<DashboardRecentAndPopularPostModel> RecentPosts { get; set; } = new List<DashboardRecentAndPopularPostModel>();
        public List<DashboardRecentAndPopularPostModel> PopularPosts { get; set; } = new List<DashboardRecentAndPopularPostModel>();
        public List<DashboardRecentAndPopularPostModel> Recruitments { get; set; } = new List<DashboardRecentAndPopularPostModel>();
        public List<DashboardRecentAndPopularPostModel> Admissions { get; set; } = new List<DashboardRecentAndPopularPostModel>();
        public List<DashboardRecentAndPopularPostModel> Exams { get; set; } = new List<DashboardRecentAndPopularPostModel>();
        public List<DashboardRecentAndPopularPostModel> Papers { get; set; } = new List<DashboardRecentAndPopularPostModel>();
        public List<DashboardRecentAndPopularPostModel> Results { get; set; } = new List<DashboardRecentAndPopularPostModel>();
        public List<DashboardRecentAndPopularPostModel> Syllabus { get; set; } = new List<DashboardRecentAndPopularPostModel>();
        public List<DashboardRecentAndPopularPostModel> AdmitCards { get; set; } = new List<DashboardRecentAndPopularPostModel>();
        public List<DashboardRecentAndPopularPostModel> Schemes { get; set; } = new List<DashboardRecentAndPopularPostModel>();
        public List<DashboardRecentAndPopularPostModel> UpComingPosts { get; set; } = new List<DashboardRecentAndPopularPostModel>();
        public List<DashboardRecentAndPopularPostModel> ExpiredSoonPosts { get; set; } = new List<DashboardRecentAndPopularPostModel>();
        public List<DashboardRecentAndPopularPostModel> PrivateRecruitments { get; set; } = new List<DashboardRecentAndPopularPostModel>();
        [JsonIgnore]
        public List<string> PopularSearchText { get; set; } = new List<string>();

    }
    public class DashboardRecentAndPopularPostModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Thumbnail { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? PublishedDate { get; set; }
        public DateTime? LastDate { get; set; }
        public int? TotalPost { get; set; }
        public string? SlugUrl { get; set; }
        public string? ModuleName { get; set; }
        public string? ModuleText { get; set; }
        //[JsonIgnore]
        public string? BlockTypeSlug { get; set; }
        [JsonIgnore]
        public bool? ForRecruitment { get; set; }
        public int? ReminingFewDays { get; set; }
        public int TotalRows { get; set; } = 0;
        public string? OrderBy { get; set; }
        public string? Years { get; set; }

    }

    public class DashboradFilterModel : IndexModel
    {
        public string? Title { get; set; }
        public int DepartmentId { get; set; }
        public int IsActive { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int QualificationId { get; set; }
        public int JobDesignationId { get; set; }
        public string? CategorySlug { get; set; }
        public string? SubCategorySlug { get; set; }
        public string? BlockTypeSlug { get; set; }
        public int? UserId { get; set; }
        public int? EligibilityId { get; set; }
        public int? CategoryId { get; set; }
        public string? SearchText { get; set; }
        public string? LanguageCode { get; set; }
        public int? StateId { get; set; }
        public bool? IsPrivate { get; set; }
    }

    public class DashboradSearchFilterModel
    {
        public string? SearchText { get; set; }
        public int? StateId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? UserId { get; set; }

    }
    public class FrontModuleTextModel
    {
        public string? ModuleText { get; set; }
        public string? Description { get; set; }

    }

    public class FrontSiteMapModel
    {
        public string? SlugUrl { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ChangeFreq { get; set; }
        public decimal? Priority { get; set; }

    }

}
