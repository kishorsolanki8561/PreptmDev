using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Translation
{
    public class BlockContents_MDL : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int BlockTypeId { get; set; }
        public int? RecruitmentId { get; set; }
        public int? DepartmentId { get; set; }
        public int? CategoryId { get; set; }
        public string? Url { get; set; }
        public string SlugUrl { set; get; }
        public string? Description { get; set; }
        public string? HowTo { get; set; }
        public DateTime? PublishedDate { get; set; }
        public int? PublisherId { get; set; }
        public int? VisitCount { get; set; }
        public int? Status { get; set; }
        public string? SortLinks { get; set; }
        public DateTime? Date { get; set; }
        public int? StateId { get; set; }
        public int? GroupId { get; set; }
        public int? SubCategoryId { get; set; }
        public string? Keywords { get; set; }
        public string? OtherLinks { get; set; }
        public string? NotificationLink { get; set; }
        public string? TitleHindi { get; set; }
        public string? DescriptionHindi { get; set; }
        public string? Summary { get; set; }
        public DateTime? LastDate { get; set; }
        public DateTime? ExtendedDate { get; set; }
        public DateTime? FeePaymentLastDate { get; set; }
        public DateTime? CorrectionLastDate { get; set; }
        public int? UrlLabelId { get; set; }
        public int? ExamMode { get; set; }
        public string? Thumbnail { get; set; }
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
        public string? SummaryHindi { get; set; }
    }

}
