using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Translation
{
    public class Scheme_MDL : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? TitleHindi { get; set; }
        public int? DepartmentId { get; set; }
        public int? StateId { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? ExtendedDate { get; set; }
        public DateTime CorrectionLastDate { get; set; }
        public DateTime? PostponeDate { get; set; }
        public int? LevelType { get; set; }
        public int? Mode { get; set; }
        public string? OfficelLink { get; set; }
        public string? ApplyLink { get; set; }
        public string? ShortDescription { get; set; }
        public string? ShortDescriptionHindi { get; set; }
        public string? Keywords { get; set; }
        public string? Description { get; set; }
        public string? DescriptionHindi { get; set; }
        public string? HowtoApply { get; set; }
        public string? OtherLinks { get; set; }
        public string? Thumbnail { get; set; }
        public string Slug { get; set; }
        public string? ContactDetail { get; set; }
        public string? Fee { get; set; }
        public int? Status { get; set; }
        public int? VisitCount { get; set; }
        public string? SortLinks { get; set; }
        public int? PublisherId { get; set; }
        public DateTime? PublishedDate { get; set; }
        public int? BlockTypeCode { get; set; }
        public string? SocialMediaUrl { get; set; }
        public string? ThumbnailCredit { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsExpired { get; set; }
        public DateTime? ShouldReminder { get; set; }
        public string? ReminderDescription { get; set; }
        public int? UpcomingCalendarCode { get; set; }
        public string? DescriptionJson { get; set; }
        public string? DescriptionHindiJson { get; set; }
        public string? KeywordsHindi { get; set; }
    }

}
