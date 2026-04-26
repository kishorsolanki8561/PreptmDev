using Microsoft.AspNetCore.Http;
using ModelService.OtherModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelService.Model.Front
{
    public class SchemeFrontModel
    {
        public int Id { get; set; } 
        public string? Title { get; set; }
        public string? DepartmentName { get; set; }
        public string? State { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? ExtendedDate { get; set; }
        public DateTime? CorrectionLastDate { get; set; }
        public DateTime? PostponeDate { get; set; }
        public int? LevelType { get; set; }
        public int? Mode { get; set; }
        public string? OfficialLink { get; set; }
        public string? ApplyLink { get; set; }
        public string? ShortDescription { get; set; }
        public string? Keywords { get; set; }
        public string? Description { get; set; }
        //public string? OtherLinks { get; set; }
        public string? Fee { get; set; } 
        public List<string>? Documents { get; set; } = new List<string>();
        public List<string>? Eligibilitys { get; set; }=new List<string>();
        public List<SchemeAttachmentFrontLookupModel>? Attachments { get; set; } = new List<SchemeAttachmentFrontLookupModel>(); 
        public List<OtherSchemeFrontModel>? OtherSchemes { get; set; }= new List<OtherSchemeFrontModel>();
        public List<FAQLookupFrontModel>? FAQs { get; set; }= new List<FAQLookupFrontModel>();
        public List<SchemeContactDetailFrontModel>? ContactDetails { get; set; } = new List<SchemeContactDetailFrontModel>();
        public List<HowToApplyFrontModel>? HowToApplys { get; set; } = new List<HowToApplyFrontModel>();
        public List<QuickLinkFrontModel>? QuickLinks { get; set; }=new List<QuickLinkFrontModel>();
        public string? ModuleText { get ; set; }
        public string? ModuleName { get; set; }
        public string? ModuleSlug { get; set; }
        public string? DepartmentSlugUrl { get; set; }
        public int? BookmarkId { get; set; }
        public string? sortLinks { get; set; }
        public int? BlockTypeId { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string? Thumbnail { get; set; }
        public string? DepartmentLogo { get; set; }
        public string? SocialMediaUrl { get; set; }
        public string? ThumbnailCredit { get; set; }
    }

    public class SchemeAttachmentFrontLookupModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Path { get; set; }
        public int? Type { get; set; }
    }

    public class OtherSchemeFrontModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Thumbnail { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? LastDate { get; set; }
        public string? SlugUrl { get; set; }
        public string? ModuleName { get; set; }
        public string? BlockTypeSlug { get; set; }
    }
    public class SchemeDocumentFrontModel
    {
        public string? Document { get; set; }
    } 
    public class SchemeEligibilityFrontModel
    {
        public string? Eligibility { get; set; }
    }

    public class SchemeContactDetailFrontModel
    {
        public string? NodalOfficerName { get; set; }
        public string? DepartmentName { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
    }
    public class HowToApplyFrontModel
    {
        public string? Description { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }
    }

    public class QuickLinkFrontModel
    {
        public string? Description { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }
        public string? IconClass { get; set; }
    }

    public class FAQLookupFrontModel
    {
        public string? Que { get; set; }
        public string? Ans { get; set; }
    }

}
