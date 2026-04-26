using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelService.Model.Front
{
    public class BookmarkModel
    {
        public int Id { get; set; } = 0;
        public int PostId { get; set; } = 0;
        [JsonIgnore]
        public int UserId { get; set; }
        public int ModuleEnum { get; set; } = 0;

    }

    public class BookmarkResponseModel
    {
        public string? Massage { get; set; }
        public int? BookmarkId { get; set; }

    }

    public class BookmarkSearchFilterModel
    {
        public string? SearchText { get; set; }
        public int? StateId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? UserId { get; set; }
        [JsonIgnore]
        public string? LanguageCode { get; set; }

    }

    public class BookmarkPostListModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Thumbnail { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? LastDate { get; set; }
        public int TotalPost { get; set; }
        public string? SlugUrl { get; set; }
        public string? ModuleName { get; set; }
        public string? BlockTypeSlug { get; set; }
        public int? BookmarkId { get; set; }
        public int TotalRows { get; set; }
    }
}
