using AutoMapper;
using ModelService.CommonModel;
using ModelService.MDL.Translation;
using ModelService.Model.Translation;
using ModelService.Model.Translation.Article;
using ModelService.OtherModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ModelService.Model
{
    public class BlockContentsRequestDTO : IMapFrom<BlockContents_MDL>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? BlockTypeId { get; set; }
        public int? RecruitmentId { get; set; }
        public int? DepartmentId { get; set; }
        public int? CategoryId { get; set; }
        public string SlugUrl { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public string SortLinks { get; set; }
        public DateTime? Date { get; set; }
        public int? StateId { get; set; }
        public int? GroupId { get; set; }
        public int? SubCategoryId { get; set; }
        public string Keywords { get; set; }
        public string OtherLinks { get; set; }
        public string NotificationLink { get; set; }
        public string TitleHindi { get; set; }
        public string DescriptionHindi { get; set; }
        public string Summary { get; set; }
        public DateTime? LastDate { get; set; }
        public DateTime? ExtendedDate { get; set; }
        public DateTime? FeePaymentLastDate { get; set; }
        public DateTime? CorrectionLastDate { get; set; }
        public int? UrlLabelId { get; set; }
        public int? ExamMode { get; set; }
        public string Thumbnail { get; set; }
        public string SocialMediaUrl { get; set; }
        public string ThumbnailCredit { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsExpired { get; set; }
        public DateTime? ShouldReminder { get; set; }
        public string ReminderDescription { get; set; }
        public int? UpcomingCalendarCode { get; set; }
        public string DescriptionJson { get; set; }
        public string DescriptionHindiJson { get; set; }
        public string KeywordsHindi { get; set; }
        public string SummaryHindi { get; set; }
        public List<HowToApplyAndQuickLinkRequestDTO> HowToApplyAndQuickLinkLookup { get; set; } = new List<HowToApplyAndQuickLinkRequestDTO>();
        public List<string> Documents { get; set; } = new List<string>();
        public List<int> BlockContentTags { get; set; } = new List<int>();
        public List<BlockContentFAQRequestDTO> FAQLookup { get; set; } = new List<BlockContentFAQRequestDTO>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BlockContentsRequestDTO, BlockContents_MDL>().ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember, context) =>
                (srcMember != null && destMember != null &&
                srcMember.GetType() == destMember.GetType() &&
                srcMember.Equals(destMember) == false) || destMember == null
            ));
        }

    }

    public class HowToApplyAndQuickLinkRequestDTO : IMapFrom<BlockContentsHowToApplyAndQuickLinkLookup_MDL>
    {
        public int Id { get; set; }
        [JsonIgnore]
        public int? BlockContentId { get; set; }
        public string Title { get; set; }
        public string TitleHindi { get; set; }
        public string Description { get; set; }
        public string DescriptionHindi { get; set; }
        public string DescriptionJson { get; set; }
        public string DescriptionHindiJson { get; set; }
        public bool IsQuickLink { get; set; }
        public string Url { get; set; }
        public string LinkUrl { get; set; }
        public string IconClass { get; set; }
        public bool IsUpdate { get; set; }
        public void Mapping(Profile profile)
        {
            _ = profile.CreateMap<HowToApplyAndQuickLinkRequestDTO, BlockContentsHowToApplyAndQuickLinkLookup_MDL>();
        }
    }

    public class BlockContentAttachmentRequestDTO : IMapFrom<BlockContentAttachmentLookup_MDL>
    {
        public int? Id { get; set; }
        public string? Path { get; set; }
        [JsonIgnore]
        public long BlockContentId { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; }
        public void Mapping(Profile profile)
        {
            _ = profile.CreateMap<BlockContentAttachmentRequestDTO, BlockContentAttachmentLookup_MDL>();
        }
    }

    public class BlockContentFAQRequestDTO : IMapFrom<FAQ_MDL>
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
            _ = profile.CreateMap<BlockContentFAQRequestDTO, FAQ_MDL>();
        }
    }

    public class BlockContentResponseDTO
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
        public List<HowToApplyAndQuickLinkResponseDTO> HowToApplyAndQuickLinkLookup { get; set; } = new List<HowToApplyAndQuickLinkResponseDTO>();
        public List<string> Documents { get; set; } = new List<string>();
        public List<int> BlockContentTags { get; set; } = new List<int>();
        public List<BlockContentFAQResponseDTO> FAQLookup { get; set; } = new List<BlockContentFAQResponseDTO>();

    }

    public class HowToApplyAndQuickLinkResponseDTO
    {
        public int Id { get; set; }
        [JsonIgnore]
        public int? BlockContentId { get; set; }
        public string Title { get; set; }
        public string TitleHindi { get; set; }
        public string Description { get; set; }
        public string DescriptionHindi { get; set; }
        public string DescriptionJson { get; set; }
        public string DescriptionHindiJson { get; set; }
        public bool IsQuickLink { get; set; }
        public string Url { get; set; }
        public string LinkUrl { get; set; }
        public string IconClass { get; set; }
        public bool IsUpdate { get; set; }
    }

    public class BlockContentAttachmentResponseDTO
    {
        public int? Id { get; set; }
        public string? Path { get; set; }
        [JsonIgnore]
        public long BlockContentId { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; }
    }

    public class BlockContentFAQResponseDTO
    {
        public int Id { get; set; }
        public string Que { get; set; }
        public string Ans { get; set; }
        public string QueHindi { get; set; }
        public string AnsHindi { get; set; }
        public bool IsUpdate { get; set; }

    }
    public class BlockContentsTagsResponseDTO
    {
        public int Id { get; set; }
        public int TagsId { get; set; }
        public int BlockContentId { get; set; }
    }

    public class BlockContentsViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? RecruitmentTitle { get; set; }
        public string? DepartmentName { get; set; }
        public string? CategoryName { get; set; }
        public string? ModifiedByName { get; set; }
        public string? SlugUrl { get; set; }
        public string? PublisherName { get; set; }
        public int Status { get; set; }
        public int VisitCount { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime? Date { get; set; }
        public string? GroupName { get; set; }
        public string? SubCategoryName { get; set; }
        public string? Keywords { get; set; }
        public string? Designation { get; set; }
        public string? Summary { get; set; }
        public string? blockTypeName { get; set; }
        public string? Logo { get; set; }
        [JsonIgnore]
        public string? ModuleSlug { get; set; }
        [JsonIgnore]
        public string? ModuleText { get; set; }
        [JsonIgnore]
        public int TotalRows { get; set; }
    }

    #region Front
    public class BlockContentsFrontViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string? DepartmentName { get; set; }
        public string? DepartmentSlugUrl { get; set; }
        public string? SortLinks { get; set; }
        public string? StateName { get; set; }
        public string? NotificationLink { get; set; }
        public DateTime? StartDate { get; set; }
        public string? ModuleName { get; set; }
        public string? ModuleText { get; set; }
        public string? ModuleSlug { get; set; }
        public int? BookmarkId { get; set; }
        public DateTime? LastDate { get; set; }
        public DateTime? ExtendedDate { get; set; }
        public DateTime? FeePaymentLastDate { get; set; }
        public DateTime? CorrectionLastDate { get; set; }
        public string? UrlLabel { get; set; }
        public string? Summary { get; set; }
        public int? ExamMode { get; set; }
        public BlockContentMapRecruitmentModel? Recruitment { get; set; }
        public List<string> Documents { get; set; } = new List<string>();
        public List<BlockContentFrontHowToApplyAndQuickLinkLookup> HowToApply { get; set; } = new List<BlockContentFrontHowToApplyAndQuickLinkLookup>();
        public List<BlockContentFrontHowToApplyAndQuickLinkLookup> OtherLinksList { get; set; } = new List<BlockContentFrontHowToApplyAndQuickLinkLookup>();
        public List<DashboardRecentAndPopularPostModel> RelatedRecruitment { get; set; } = new List<DashboardRecentAndPopularPostModel>();
        public List<DashboardRecentAndPopularPostModel> RelatedBlockContent { get; set; } = new List<DashboardRecentAndPopularPostModel>();
        public List<BlockContentFrontFAQLookup> FAQs { get; set; } = new List<BlockContentFrontFAQLookup>();
        public List<BlockContentsTagsModel> Tags { get; set; } = new List<BlockContentsTagsModel>();
        public List<DashboardRecentAndPopularPostModel> Articles { get; set; } = new List<DashboardRecentAndPopularPostModel>();

        public string? Keywords { get; set; }
        public int? BlockTypeId { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string? Thumbnail { get; set; }
        public string? DepartmentLogo { get; set; }
        public string? SocialMediaUrl { get; set; }
        public string? ThumbnailCredit { get; set; }
    }

    public class BlockContentMapRecruitmentModel
    {
        public string? Tittle { get; set; }
        public string? SlugUrl { get; set; }
        public string? ShortDesription { get; set; }
        public string? ModuleSlug { get; set; }
        public string? JobDesignation { get; set; }
        public string? Qualification { get; set; }
    }
    public class BlockContentsFilterModel : IndexModel
    {
        public string Title { get; set; }
        public string SlugUrl { get; set; }
        public int BlockTypeId { get; set; }
        public int RecruitmentId { get; set; }
        public int DepartmentId { get; set; }
        public int CategoryId { get; set; }
        public int IsActive { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int Status { get; set; }
        public int GroupId { get; set; }
        public int SubCategoryId { get; set; }
        public int? BlockTypeCode { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }

    public class FrontBlockContentFilterModel : IndexModel
    {
        public string? Title { get; set; }
        public int DepartmentId { get; set; }
        public int IsActive { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int GroupId { get; set; }
        public int RecruitmentId { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }

    public class BlockContentAttachmentModel
    {
        public int Id { get; set; }
        public string Path { get; set; }
        [JsonIgnore]
        public long BlockContentId { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; }
    }

    public class BlockContentFrontHowToApplyAndQuickLinkLookup
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string? IconClass { get; set; }

    }

    public class BlockContentFrontFAQLookup
    {
        public string? Que { get; set; }
        public string? Ans { get; set; }
    }

    public class BlockContentsTitleCheckModel
    {
        public string? Title { get; set; }
        public string SlugUrl { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }

    }

    public class BlockContentsTagsModel
    {
        public string? Tag { get; set; }
        public string? SlugUrl { get; set; }
    }
    #endregion
}
