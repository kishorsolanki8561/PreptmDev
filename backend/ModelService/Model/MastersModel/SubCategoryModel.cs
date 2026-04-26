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
    public class SubCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? NameHindi { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public string SlugUrl { get; set; }
        public string Icon { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }
    public class SubCategoryViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameHindi { get; set; }

        public string? CategoryName { get; set; }
        public string? CategoryNameHindi { get; set; }
        public string? SlugUrl { get; set; }
        public string? Icon { get; set; }
        public string? ModifiedByName { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public int TotalRows { get; set; }
    }
    public class SubCategoryFilterModel : IndexModel
    {
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public string? SlugUrl { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int IsActive { get; set; }

    }
    public class DDLSubCategoryModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameHindi { get; set; }
        public string? Icon { get; set; }
        public string? SlugUrl { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryNameHindi { get; set; }
        public string? CategorySlugUrl { get; set; }

    }
}
