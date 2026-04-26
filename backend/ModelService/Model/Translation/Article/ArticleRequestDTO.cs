using AutoMapper;
using ModelService.CommonModel;
using ModelService.MDL.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelService.Model.Translation.Article
{
    public class ArticleRequestDTO : IMapFrom<Article_MDL>
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
        public string Keywords { get; set; }
        public string KeywordHindi { get; set; }
        public string Thumbnail { get; set; }
        public string ThumbnailCredit { get; set; }
        public string SlugUrl { set; get; }
        public int? Status { get; set; }
        public DateTime? PublisherDate { get; set; }
        public List<int> ArticleTagsDTOs { get; set; } = new List<int>();
        public List<ArticleFaqDTO> ArticleFaqsDTOs { get; set; } =new List<ArticleFaqDTO>();
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ArticleRequestDTO, Article_MDL>().ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember, context) =>
                (srcMember != null && destMember != null &&
                srcMember.GetType() == destMember.GetType() &&
                srcMember.Equals(destMember) == false) || destMember == null
            ));
        }
    }
    public class ArticleTagsDTO : IMapFrom<ArticleTags_MDL>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int TagsId { get; set; }
        [JsonIgnore]
        public int ArticleId { get; set; }
        public void Mapping(Profile profile)
        {
            _ = profile.CreateMap<ArticleTagsDTO, Article_MDL>();
        }
    }

    public class ArticleFaqDTO : IMapFrom<ArticleFaq_MDL>
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public string Que { get; set; }
        public string Ans { get; set; }
        public string QueHindi { get; set; }
        public string AnsHindi { get; set; }

        public void Mapping(Profile profile)
        {
            _ = profile.CreateMap<ArticleFaqDTO, ArticleFaq_MDL>();
        }
    }

    public class ArticleFilterModel : IndexModel
    {
        public string? Title { get; set; }
        public string? TitleHindi { get; set; }
        public int IsActive { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
    }
}
