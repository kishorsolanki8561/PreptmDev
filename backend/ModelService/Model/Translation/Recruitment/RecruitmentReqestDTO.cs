using AutoMapper;
using ModelService.MDL.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.Model.Translation.Recruitment
{
    public class RecruitmentReqestDTO : IMapFrom<Recruitment_MDL>
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
        public DateTime? FeePaymentLastDate { get; set; }
        public DateTime? CorrectionLastDate { get; set; }
        public DateTime? AdmitCardDate { get; set; }
        public int ExamMode { get; set; }
        public string? ApplyLink { get; set; }
        public string? OfficialLink { get; set; }
        public string? NotificationLink { get; set; }
        public long? TotalPost { get; set; }
        public int? SubCategoryId { get; set; }
        public string? HowTo { get; set; }
        public string? ShortDesription { get; set; }
        public int? Status { get; set; }
        public string? SlugUrl { get; set; }
        public string? Thumbnail { get; set; }
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
        public List<string> Attachments { get; set; } = new List<string>();
        public List<int> JobDesignations { get; set; } = new List<int>();
        public List<int> Qualifications { get; set; } = new List<int>();
        public List<RecruitmentHowToApplyAndQuickLinkRequestDTO> HowToApplyAndQuickLinkLookup { get; set; } = new List<RecruitmentHowToApplyAndQuickLinkRequestDTO>();
        public List<BlockContentFAQRequestDTO> FAQLookup { get; set; } = new List<BlockContentFAQRequestDTO>();
        public List<int> Tags { get; set; } = new List<int>();
        public void Mapping(Profile profile)
        {
            profile.CreateMap<RecruitmentReqestDTO, Recruitment_MDL>().ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember, context) =>
                (srcMember != null && destMember != null &&
                srcMember.GetType() == destMember.GetType() &&
                srcMember.Equals(destMember) == false) || destMember == null
            ));
        }
    }
    public class RecruitmentHowToApplyAndQuickLinkRequestDTO : IMapFrom<RecruitmentHowToApplyAndQuickLinkLookup_MDL>
    {
        public int Id { get; set; }
        public int RecruitmentId { get; set; }
        public string? Title { get; set; }
        public string? TitleHindi { get; set; }
        public string? LinkUrl { get; set; }
        public bool IsQuickLink { get; set; }
        public string? Description { get; set; }
        public string? DescriptionHindi { get; set; }
        public string? IconClass { get; set; }
        public string? DescriptionJson { get; set; }
        public string? DescriptionHindiJson { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<RecruitmentHowToApplyAndQuickLinkRequestDTO, RecruitmentHowToApplyAndQuickLinkLookup_MDL>();
        }
    }

    public class FAQRequestDTO : IMapFrom<FAQ_MDL>
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public int BlockTypeId { get; set; }
        public string Que { get; set; }
        public string Ans { get; set; }
        public string QueHindi { get; set; }
        public string AnsHindi { get; set; }
        public bool IsUpdate { get; set; }
        public void Mapping(Profile profile)
        {
            _ = profile.CreateMap<FAQRequestDTO, FAQ_MDL>();
        }
    }
}
