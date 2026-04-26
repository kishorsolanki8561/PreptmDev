using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Translation
{
    public class Recruitment_MDL : AuditableEntity
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public int? DepartmentId { get; set; }
        public string? Salary { get; set; }
        public string? Description { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? LastDate { get; set; }
        public DateTime? ExtendedDate { get; set; }
        public DateTime? PublishedDate { get; set; }
        public DateTime? FeePaymentLastDate { get; set; }
        public DateTime? CorrectionLastDate { get; set; }
        public DateTime? AdmitCardDate { get; set; }
        public int ExamMode { get; set; }
        public string? ApplyLink { get; set; }
        public string? OfficialLink { get; set; }
        public string? NotificationLink { get; set; }
        public int? PublisherId { get; set; }
        public bool? IsApproved { get; set; }
        public long? TotalPost { get; set; }
        public int? SubCategoryId { get; set; }
        public string? HowTo { get; set; }
        public string? ShortDesription { get; set; }
        public int? Status { get; set; }
        public string? SlugUrl { get; set; }
        public string? Thumbnail { get; set; }
        public int? VisitCount { get; set; }
        public string? SortLinks { get; set; }
        public string? Keywords { get; set; }
        public int? StateId { get; set; }
        public int? CategoryId { get; set; }
        public string? OtherLinks { get; set; }
        public string? TitleHindi { get; set; }
        public string? DescriptionHindi { get; set; }
        public string? ShortDesriptionHindi { get; set; }
        public int? BlockTypeCode { get; set; }
        public string? ThumbnailCaption { get; set; }
        public string? SocialMediaUrl { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsExpired { get; set; }
        public DateTime? ShouldReminder { get; set; }
        public string? ReminderDescription { get; set; }
        public int? UpcomingCalendarCode { get; set; }
        public string? DescriptionJson { get; set; }
        public string? DescriptionHindiJson { get; set; }
        public string? KeywordsHindi { get; set; }
        public bool? IsPrivate { get; set; }
    }

}
