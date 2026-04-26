using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelService.Model.MastersModel
{
    public class LookupTypeModel
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? TitleHindi { get; set; }
        public string? Description { get; set; }
        public string? DescriptionHindi { get; set; }
        [Required]
        public string? Slug { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }
    public class LookupTypeViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? TitleHindi { get; set; }
        public string? Description { get; set; }
        public string? DescriptionHindi { get; set; }
        public string? Slug { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
    public class LookupTypeViewListModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? TitleHindi { get; set; }
        public string? Description { get; set; }
        public string? DescriptionHindi { get; set; }
        public string? Slug { get; set; }
        public string? ModifiedByName { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        [JsonIgnore]
        public int TotalRows { get; set; }
    }
    public class LookupTypeFilterModel : IndexModel
    {
        public string? Title { get; set; }
        public string? TitleHindi { get; set; }
        public int IsActive { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
    }
}
