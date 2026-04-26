using AutoMapper;
using ModelService.MDL.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelService.Model.Translation.Article
{
    public class ArticleResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleHindi { get; set; }
        public int ArticleType { get; set; }
        public string Summary { get; set; }
        public string SummaryHindi { get; set; }
        public string Description { get; set; }
        public string DescriptionHindi { get; set; }
        public string DescriptionJson { get; set; }
        public string DescriptionJsonHindi { get; set; }
        public DateTime? PublisherDate { get; set; }
        public string Keywords { get; set; }
        public string KeywordHindi { get; set; }
        public string Thumbnail { get; set; }
        public string ThumbnailCredit { get; set; }
        public string SlugUrl { set; get; }
        public int? Status { get; set; }
        public List<int> ArticleTagsDTOs { get; set; } = new List<int>();
        public List<ArticleFaqResponseDTO> ArticleFaqsDTOs { get; set; } = new List<ArticleFaqResponseDTO>();

    }

    public class ArticleTagsResponseDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int TagsId { get; set; }
        [JsonIgnore]
        public int ArticleId { get; set; }
    }

    public class ArticleFaqResponseDTO 
    {
        public int Id { get; set; }
        [JsonIgnore]
        public int ArticleId { get; set; }
        public string Que { get; set; }
        public string Ans { get; set; }
        public string QueHindi { get; set; }
        public string AnsHindi { get; set; }

    }

    public class ArticleTitleCheckModel
    {
        public string? Title { get; set; }
        public string SlugUrl { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }

    }

    public class ArticleViewListModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleHindi { get; set; }
        public string SlugUrl { get; set; }
        public string ModifiedByName { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int Status { get; set; }
        public int VisitCount { get; set; }
        public string PublisherName { get; set; }
        public string PublisherDate { get; set; }
        public bool IsActive { get; set; }
        [JsonIgnore]
        public int TotalRows { get; set; }
    }
}
