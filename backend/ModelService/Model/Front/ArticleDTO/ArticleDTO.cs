using ModelService.CommonModel;
using System.Text.Json.Serialization;

namespace ModelService.Model.Front.ArticleDTO
{
    public class ArticleFilterDTO : IndexModel
    {
        public string? Title { get; set; } = string.Empty;
        public string? ArticleTypeSlug { get; set; } = string.Empty;
        public string? TagTypeSlug { get; set; } = string.Empty;
        public string? SearchText { get; set; } = string.Empty;
        [JsonIgnore]
        public string? LanguageCode { get; set; } = string.Empty;
        [JsonIgnore]
        public int? StateId { get; set; }
    }
    public class ArticlesDTO
    {
        public List<ArticleListDTO> Articles { get; set; } = new List<ArticleListDTO>();
        public List<ArticleListDTO> LatestArticle { get; set; } = new List<ArticleListDTO>();
    }


    public class ArticleListDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Thumbnail { get; set; }
        public string? ModuleName { get; set; }
        [JsonIgnore]
        public string? TypeSlug { get; set; }
        public string? SlugUrl { get; set; }
        public string? ModifiedDate { get; set; }
        [JsonIgnore]
        public int TotalRows { get; set; } = 0;
        //public List<string> Tags { get; set; } = new List<string>();
    }

    public class FrontModuleModel
    {
        public string? ModuleText { get; set; }
    }
    public class ArticleDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Thumbnail { get; set; }
        public string? Description { get; set; }
        public string? ArticleType { get; set; }
        public string? Summary { get; set; }
        public string? Keywords { get; set; }
        public string? ThumbnailCredit { get; set; }
        public string? ModifiedDate { get; set; }
        public List<ArticleTagsDTO> Tags { get; set; } = new List<ArticleTagsDTO>();
        public List<ArticleFaqDTO> ArticleFaqs { get; set; } = new List<ArticleFaqDTO>();
        public List<TagsWiseRecruitmentDTO> Recruitments { get; set; } = new List<TagsWiseRecruitmentDTO>();
        public List<TagsWiseBlockContentsDTO> BlockContents { get; set; } = new List<TagsWiseBlockContentsDTO>();

    }

    public class ArticleTagsDTO
    {
        public string? Tag { get; set; }
        public string? SlugUrl { get; set; }
    }

    public class ArticleFaqDTO
    {
        public string? Que { get; set; }
        public string? Ans { get; set; }
    }

    public class TagsWiseRecruitmentDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Thumbnail { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? LastDate { get; set; }
        public int? TotalPost { get; set; }
        public string? SlugUrl { get; set; }
        public string? ModuleName { get; set; }
        public string? ModuleText { get; set; }
        public string? BlockTypeSlug { get; set; }
    }

    public class TagsWiseBlockContentsDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Thumbnail { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? LastDate { get; set; }
        public string? SlugUrl { get; set; }
        public string? ModuleName { get; set; }
        public string? ModuleText { get; set; }
        //[JsonIgnore]
        public string? BlockTypeSlug { get; set; }
    }
}
