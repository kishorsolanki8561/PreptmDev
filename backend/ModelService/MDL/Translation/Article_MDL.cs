using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Translation
{
    public class Article_MDL : AuditableEntity
    {
        public Article_MDL() { }
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleHindi { get; set; }
        public int ArticleType { get; set; }
        public string? Summary { get; set; }
        public string? SummaryHindi { get; set; }
        public string? Description { get; set; }
        public string? DescriptionHindi { get; set; }
        public string? DescriptionJson { get; set; }
        public string? DescriptionJsonHindi { get; set; }
        public string? Keywords { get; set; }
        public string? KeywordHindi { get; set; }
        public string? Thumbnail { get; set; }
        public string? ThumbnailCredit { get; set; }
        public int? TagId { get; set; }
        public int? VisitCount { get; set; }
        public int? PublisherId { get; set; }
        public DateTime? PublisherDate { get; set; }
        public int? Status { get; set; }
        public string? SlugUrl { set; get; }
    }
}
