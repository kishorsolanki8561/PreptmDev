using Microsoft.AspNetCore.Http;
using ModelService.CommonModel;
using ModelService.OtherModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ModelService.Model.Translation
{
    public class SchemeRequestModel
    {
        public int? Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? TitleHindi { get; set; }
        public int? DepartmentId { get; set; }
        public int? StateId { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? ExtendedDate { get; set; }
        public DateTime? CorrectionLastDate { get; set; }
        public DateTime? PostponeDate { get; set; }
        public int LevelType { get; set; }
        [Required]
        public int Mode { get; set; }
        public string? OfficelLink { get; set; }
        public string? ApplyLink { get; set; }
        public string? ShortDescription { get; set; }
        public string? ShortDescriptionHindi { get; set; }
        public string? keywords { get; set; }
        public string? keywordsHindi { get; set; }
        public string? Description { get; set; }
        public string? DescriptionHindi { get; set; }
        public string? DescriptionJson { get; set; }
        public string? DescriptionHindiJson { get; set; }
        public string? Thumbnail { get; set; }
        [Required]
        public string? Slug { get; set; }
        public string? Fee { get; set; }
        public string? DocumentIds { get; set; }
        public string? EligibilityIds { get; set; }
        [JsonIgnore]
        public int? UserId { get; set; }
        public string? SocialMediaUrl { get; set; }
        public string? ThumbnailCredit { get; set; }
        public bool IsCompleted { get; set; }
        public int? UpcomingCalendarCode { get; set; }
        public DateTime? ShouldReminder { get; set; }
        public string? ReminderDescription { get; set; }
        [JsonIgnore]
        public XDocument? DBHowToApplyAndQuickLinkLookups { get; set; }
        [JsonIgnore]
        public XDocument? DBSchemeContactDetailsLookups { get; set; }
        [JsonIgnore]
        public XDocument? DBSchemeAttachmentLookups { get; set; }
        [JsonIgnore]
        public string? DBDeleteDocumentIds { get; set; }
        [JsonIgnore]
        public string? DBDeleteEligibilityIds { get; set; }
        [JsonIgnore]
        public string? DBDeleteSchemeContactDetailsLookupIds { get; set; }
        [JsonIgnore]
        public string? DBDeleteHowToApplyAndQuickLinkLookupIds { get; set; }
        [JsonIgnore]
        public string? DBDeleteSchemeAttachmentLookupIds { get; set; }

        public List<SchemeContactDetailsLookup>? ContactDetail { get; set; } = new List<SchemeContactDetailsLookup>();
        public List<SchemeHowToApplyAndQuickLinkLookup>? HowToApplyAndQuickLinkLookup { get; set; } = new List<SchemeHowToApplyAndQuickLinkLookup>();
        public List<SchemeAttachmentLookupModel>? SchemeAttachmentLookups { get; set; } = new List<SchemeAttachmentLookupModel>();
        public List<SchemeFAQLookup> FAQLookups { get; set; } = new List<SchemeFAQLookup>();

    }

    public class SchemeViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? TitleHindi { get; set; }
        public int? DepartmentId { get; set; }
        public int? StateId { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? ExtendedDate { get; set; }
        public DateTime? CorrectionLastDate { get; set; }
        public DateTime? PostponeDate { get; set; }
        public int LevelType { get; set; }
        public int Mode { get; set; }
        public string? OfficelLink { get; set; }
        public string? ApplyLink { get; set; }
        public string? ShortDescription { get; set; }
        public string? ShortDescriptionHindi { get; set; }
        public string? keywords { get; set; }
        public string? keywordsHindi { get; set; }
        public string? DescriptionHindi { get; set; }
        public string? Description { get; set; }
        public string? DescriptionHindiJson { get; set; }
        public string? DescriptionJson { get; set; }
        public string? Thumbnail { get; set; }
        public string? Slug { get; set; }
        public string? Fee { get; set; }
        [JsonIgnore]
        public string? ModuleSlug { get; set; }
        [JsonIgnore]
        public string? ModuleName { get; set; }
        public List<int>? DocumentIds { get; set; }
        public string? SocialMediaUrl { get; set; }
        public string? ThumbnailCredit { get; set; }
        public bool IsCompleted { get; set; }
        public int? UpcomingCalendarCode { get; set; }
        public DateTime? ShouldReminder { get; set; }
        public string? ReminderDescription { get; set; }
        public DateTime? PublishedDate { get; set; }
        [JsonIgnore]// only use for internal
        public List<SchemeDocumentLookupModel> SchemeDocuments { get; set; } = new List<SchemeDocumentLookupModel>();
        public List<int>? EligibilityIds { get; set; } //= new List<SchemeEligibilityLookupModel>();
        [JsonIgnore]// only use for internal
        public List<SchemeEligibilityLookupModel> SchemeEligibilitys { get; set; } = new List<SchemeEligibilityLookupModel>();
        public List<SchemeAttachmentLookupModel>? SchemeAttachmentLookups { get; set; } = new List<SchemeAttachmentLookupModel>();
        public List<SchemeContactDetailsLookup> ContactDetail { get; set; } = new List<SchemeContactDetailsLookup>();
        public List<SchemeHowToApplyAndQuickLinkLookup> HowToApplyAndQuickLinkLookup { get; set; } = new List<SchemeHowToApplyAndQuickLinkLookup>();
        public List<SchemeFAQLookup> FAQLookups { get; set; } = new List<SchemeFAQLookup>();
        public int? Status { get; set; }

    }
    public class SchemeViewListModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? TitleHindi { get; set; }
        public string? Description { get; set; }
        public string? DescriptionHindi { get; set; }
        public string? Slug { get; set; }
        public string? ModifiedByName { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int Status { get; set; }
        public int VisitCount { get; set; }
        public string? PublisherName { get; set; }
        public string? PublishedDate { get; set; }
        public bool IsActive { get; set; }
        [JsonIgnore]
        public int TotalRows { get; set; }
    }
    public class SchemeFilterModel : IndexModel
    {
        public string? Title { get; set; }
        public string? TitleHindi { get; set; }
        public int IsActive { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
    }
    public class SchemeDocumentLookupModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int SchemeId { get; set; }
        public int LookupId { get; set; }
        public string? Description { get; set; }
        public string? DescriptionHindi { get; set; }
    }
    public class SchemeEligibilityLookupModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int SchemeId { get; set; }
        public int EligibilityId { get; set; }
    }
    public class SchemeAttachmentLookupModel
    {
        public int? Id { get; set; }
        [JsonIgnore]
        public int SchemeId { get; set; }
        public string? Title { get; set; }
        public string? TitleHindi { get; set; }
        public string? Description { get; set; }
        public string? DescriptionHindi { get; set; }
        public int? Type { get; set; }
        public string? Path { get; set; }
        public bool IsUpdate { get; set; }
    }
    public class SchemeHowToApplyAndQuickLinkLookup
    {
        public int? Id { get; set; }
        [JsonIgnore]
        public int SchemeId { get; set; }
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
    public class SchemeContactDetailsLookup
    {
        public int? Id { get; set; }
        public int? DepartmentId { get; set; }
        public int? SchemeId { get; set; }
        public string? NodalOfficerName { get; set; }
        public string? NodalOfficerNameHindi { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
        public bool IsUpdate { get; set; }
    }
    public class GetSchemeDocumentAndAttachmentLookupDataModel
    {
        public List<SchemeDocumentLookupModel> SchemeDocumentLookups { get; set; } = new List<SchemeDocumentLookupModel>();
        public List<SchemeEligibilityLookupModel> SchemeEligibilityLookups { get; set; } = new List<SchemeEligibilityLookupModel>();
        public List<SchemeAttachmentLookupModel> SchemeAttachmentLookups { get; set; } = new List<SchemeAttachmentLookupModel>();
        public List<SchemeContactDetailsLookup> ContactDetail { get; set; } = new List<SchemeContactDetailsLookup>();
        public List<SchemeHowToApplyAndQuickLinkLookup> HowToApplyAndQuickLinkLookup { get; set; } = new List<SchemeHowToApplyAndQuickLinkLookup>();
        public List<SchemeFAQLookup> FAQLookups { get; set; } = new List<SchemeFAQLookup>();
    }

    public class SchemeFAQLookup
    {
        public int Id { get; set; }
        public string? Que { get; set; }
        public string? Ans { get; set; }
        public string? QueHindi { get; set; }
        public string? AnsHindi { get; set; }
        public bool IsUpdate { get; set; }

    }

    public class SchemeTitleCheckModel
    {
        public string? Title { get; set; }
        public string SlugUrl { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }

    }

}
