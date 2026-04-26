using ModelService.CommonModel;
using ModelService.OtherModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ModelService.Model.Translation
{
    public class RecruitmentModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string? TitleHindi { get; set; }
        public int? DepartmentId { get; set; }
        public string? Salary { get; set; }
        public string? Description { get; set; }
        public string? DescriptionHindi { get; set; }
        public string? DescriptionJson { get; set; }
        public string? DescriptionHindiJson { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? LastDate { get; set; }
        public DateTime? ExtendedDate { get; set; }
        public DateTime? FeePaymentLastDate { get; set; }
        public DateTime? CorrectionLastDate { get; set; }
        public DateTime? AdmitCardDate { get; set; }
        public int ExamMode { get; set; }
        public string? ApplyLink { get; set; }
        public string? OfficialLink { get; set; }
        public string? NotificationLink { get; set; }
        public int? TotalPost { get; set; }
        [JsonIgnore]
        public string? OtherLinks { get; set; }
        [JsonIgnore]
        public string HowTo { get; set; }
        public string? ShortDesription { get; set; }
        public string? ShortDesriptionHindi { get; set; }
        [Required]
        public string SlugUrl { get; set; }
        public string? thumbnail { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
        [JsonIgnore]
        public int VisitCount { get; set; }
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public string? Keywords { get; set; }
        public string? KeywordsHindi { get; set; }
        public int? StateId { get; set; }
        public int BlockTypeCode { get; set; }
        public string? ThumbnailCaption { get; set; }
        public string? SocialMediaUrl { get; set; }
        public bool IsCompleted { get; set; }
        public int? UpcomingCalendarCode { get; set; }
        public DateTime? ShouldReminder { get; set; }
        public string? ReminderDescription { get; set; }
        public DateTime? PublishedDate { get; set; }
        public List<RecruitmentHowToApplyAndQuickLinkLookup>? HowToApplyAndQuickLinkLookup { get; set; } = new List<RecruitmentHowToApplyAndQuickLinkLookup>();
        [JsonIgnore]
        public List<RecruitmentDocumentModel> InternalAttachments { get; set; } = new List<RecruitmentDocumentModel>();
        public List<string> Attachments { get; set; }
        public List<int> jobDesignations { get; set; } = new List<int>();
        public List<int> Qualifications { get; set; } = new List<int>();
        [JsonIgnore]
        public string? DeleteDocumentLookupIds { get; set; }
        public List<RecruitmentFAQLookup> FAQLookup { get; set; } = new List<RecruitmentFAQLookup>();
    }

    public class RecruitmentViewModel
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? TitleHindi { get; set; }
        public string? DepartmentName { get; set; }
        public string? DepartmentNameHindi { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LastDate { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string? PublisherName { get; set; }
        //public bool IsApproved { get; set; }
        public int TotalPost { get; set; }
        public string? SlugUrl { get; set; }
        public int Status { get; set; }
        public string? ModifiedByName { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public int VisitCount { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryNameHindi { get; set; }
        public string? SubCategoryName { get; set; }
        public string? SubCategoryNameHindi { get; set; }
        public string? DepartmentLogo { get; set; }
        public string? ShortDesription { get; set; }
        public string? ModuleSlug { get; set; }
        public string? ModuleText { get; set; }
        public string? CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public int TotalRows { get; set; }
    }

    public class RecruitmentFrontViewModel
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? DepartmentName { get; set; }
        public string? DepartmentSlugUrl { get; set; }
        public string? Salary { get; set; }
        public string? Description { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? LastDate { get; set; }
        public DateTime? ExtendedDate { get; set; }
        public DateTime? FeePaymentLastDate { get; set; }
        public DateTime? CorrectionLastDate { get; set; }
        public DateTime? AdmitCardDate { get; set; }
        public DateTime? PublishedDate { get; set; }
        public int? ExamMode { get; set; }
        public string? ApplyLink { get; set; }
        public string? OfficialLink { get; set; }
        public string? NotificationLink { get; set; }
        public int? TotalPost { get; set; }
        public string? ShortDesription { get; set; }
        public string? SortLinks { get; set; }
        public List<RecruitmentFrontHowToApplyAndQuickLinkLookup> HowToApply { get; set; } = new List<RecruitmentFrontHowToApplyAndQuickLinkLookup>();
        public List<RecruitmentFrontHowToApplyAndQuickLinkLookup> OtherLinksList { get; set; } = new List<RecruitmentFrontHowToApplyAndQuickLinkLookup>();
        public string? SlugUrl { get; set; }
        public List<string> Documents { get; set; } = new List<string>();
        public List<string> Designation { get; set; } = new List<string>();
        public List<string> Qualification { get; set; } = new List<string>();
        public string? StateName { get; set; }
        public List<DashboardRecentAndPopularPostModel> RelatedCatSubCat { get; set; } = new List<DashboardRecentAndPopularPostModel>();
        public List<DashboardRecentAndPopularPostModel> RelatedBlockContent { get; set; } = new List<DashboardRecentAndPopularPostModel>();
        public List<RecruitmentFrontFAQLookup> FAQs { get; set; } = new List<RecruitmentFrontFAQLookup>();
        public List<RecruitmentTagsModel> Tags { get; set; } = new List<RecruitmentTagsModel>();
        public List<DashboardRecentAndPopularPostModel> Articles { get; set; } = new List<DashboardRecentAndPopularPostModel>();
        public string? ModuleName { get; set; }
        public string? ModuleText { get; set; }
        public string? ModuleSlug { get; set; }
        public string? CategoryName { get; set; }
        public string? SubCategoryName { get; set; }
        public string? Keywords { get; set; }
        public int? BookmarkId { get; set; }
        public int? BlockTypeId { get; set; }
        public string? Thumbnail { get; set; }
        public string? DepartmentLogo { get; set; }
        public string? SocialMediaUrl { get; set; }
        public string? ThumbnailCredit { get; set; }
        public bool? IsPrivate { get; set; }
    }

    public class RecruitmentFilterModel : IndexModel
    {
        public int RecruitmentId { get; set; }
        public string? Title { get; set; }
        public int DepartmentId { get; set; }
        public int PublisherId { get; set; }
        public int IsApproved { get; set; }
        public int IsActive { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int QualificationId { get; set; }
        public int JobDesignationId { get; set; }
        public int CategoryId { get; set; }
        public int BlockTypeCode { get; set; }
        public int SubCategoryId { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }

    }

    public class RecruitmentDocumentModel
    {
        public int Id { get; set; }
        public string? Path { get; set; }
        [JsonIgnore]
        public long RecruitmentId { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; }
    }

    public class RecruitmentJobDesignationModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameHindi { get; set; }
        [JsonIgnore]
        public int JobDesignationId { get; set; }
        [JsonIgnore]
        public long RecruitmentId { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; }
    }

    public class RecruitmentQualificationModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? TitleHindi { get; set; }
        [JsonIgnore]
        public int QualificationId { get; set; }
        [JsonIgnore]
        public long RecruitmentId { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; }
    }

    public class RecruitmentHowToApplyAndQuickLinkLookup
    {
        public int? Id { get; set; }
        [JsonIgnore]
        public int RecruitmentId { get; set; }
        public string? Title { get; set; }
        public string? TitleHindi { get; set; }
        public string? Description { get; set; }
        public string? DescriptionHindi { get; set; }
        public string? DescriptionJson { get; set; }
        public string? DescriptionHindiJson { get; set; }
        public bool IsQuickLink { get; set; }
        public string? LinkUrl { get; set; }
        public string? IconClass { get; set; }
        public bool IsUpdate { get; set; }
    }
    public class RecruitmentFrontHowToApplyAndQuickLinkLookup
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string? IconClass { get; set; }

    }

    public class RecruitmentFAQLookup
    {
        public int Id { get; set; }
        public string? Que { get; set; }
        public string? Ans { get; set; }
        public string? QueHindi { get; set; }
        public string? AnsHindi { get; set; }
        public bool isUpdate { get; set; }
    }

    public class RecruitmentFrontFAQLookup
    {
        public string? Que { get; set; }
        public string? Ans { get; set; }
    }

    public class RecruitmentTitleCheckModel
    {
        public string? Title { get; set; }
        public string SlugUrl { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }

    }
    
    public class RecruitmentTagsModel
    {
        public string? Tag { get; set; }
        public string? SlugUrl { get; set; }
    }

}
