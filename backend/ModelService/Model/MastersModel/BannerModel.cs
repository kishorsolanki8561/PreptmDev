using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelService.Model.MastersModel
{
    public class BannerModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleHindi { get; set; }
        public string Url { get; set; }
        public string AttachmentUrl { get; set; }
        public bool IsAdvt { get; set; } = false;
        public int DisplayOrder { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }
    public class BannerViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? TitleHindi { get; set; }
        public string? Url { get; set; }
        public string? AttachmentUrl { get; set; }
        public bool? IsAdvt { get; set; } = false;
        public int DisplayOrder { get; set; }

        [JsonIgnore]
        public int TotalRows { get; set; }
    }

    public class BannerViewListModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? TitleHindi { get; set; }
        public string? Url { get; set; }
        public string? AttachmentUrl { get; set; }
        public bool IsAdvt { get; set; }
        public int DisplayOrder { get; set; }

        public string? ModifiedByName { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        [JsonIgnore]
        public int TotalRows { get; set; }
    }
    public class BannerFilterModel : IndexModel
    {
        public string? Title { get; set; } = string.Empty;
        public int IsAdvt { get; set; }
        public int IsActive { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
    }
}
