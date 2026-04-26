using ModelService.CommonModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ModelService.Model.MastersModel
{
    public class CategoryMasterModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? NameHindi { get; set; }
        [Required]
        public string SlugUrl { get; set; }
        public string Icon { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }

    public class CategoryMasterViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameHindi { get; set; }
        public string? SlugUrl { get; set; }
        public string? Icon { get; set; }

        public string? ModifiedByName { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public int TotalRows { get; set; }
    }
    public class CategoryMasterFilterModel : IndexModel
    {

        public string Name { get; set; }
        public string? NameHindi { get; set; }
        public string SlugUrl { get; set; }
        public string Icon { get; set; }
        public int IsActive { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
    }
}
