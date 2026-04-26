using ModelService.CommonModel;
using ModelService.Model.Translation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelService.Model.MastersModel
{
    public class DepartmentMasterModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Address { get; set; }
        public string? MapUrl { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Logo { get; set; }
        public string? Url { get; set; }

        [Required]
        public string SlugUrl { get; set; }
        public string? Description { get; set; }
        public string? DescriptionJson { get; set; }
        public string? FaceBookLink { get; set; }
        public string? TwitterLink { get; set; }
        public int? StateId { get; set; }
        [JsonIgnore]
        public int? UserId { get; set; }
        public string? NameHindi { get; set; }
        public string? AddressHindi { get; set; }
        public string? DescriptionHindi { get; set; }
        public string? DescriptionHindiJson { get; set; }
        public string? WikipediaEnglishUrl { get; set; }
        public string? WikipediaHindiUrl { get; set; }
        public string? ShortDescription { get; set; }
        public string? ShortDescriptionHindi { get; set; }
    }
    public class DepartmentMasterViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string? ShortName { get; set; }
        public string? Logo { get; set; }
        public string StateName { get; set; }
        public string? PhoneNumber { get; set; }

        public string ModifiedByName { get; set; }
        public string ModifiedDate { get; set; }
        public string Description { get; set; }
        public string FaceBookLink { get; set; }
        public string TwitterLink { get; set; }

        public bool IsActive { get; set; }

        [JsonIgnore]
        public int TotalRows { get; set; }

    }

    public class DepartmentMasterFilterModel : IndexModel
    {
        public string? Name { get; set; }
        public int StateId { get; set; }
        public string? Url { get; set; }
        public string? ShortName { get; set; }
        public string? NameHindi { get; set; }
        public int IsActive { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
    }
    public class CommonModel
    {
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }

    public class DepartmentFrontViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public string? ShortName { get; set; }
        public string? Logo { get; set; }
        public string? StateName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
        public string? FaceBookLink { get; set; }
        public string? TwitterLink { get; set; }
        public string? MapUrl { get; set; }
        public string? Address { get; set; }
        public string? SlugUrl { get; set; }
        public string? Email { get; set; }
        public string? WikipediaUrl { get; set; }
        public string? ShortDescription { get; set; }
        public string? ShortDescriptionHindi { get; set;}
        public List<DashboardRecentAndPopularPostModel> RelatedData { get; set; } = new List<DashboardRecentAndPopularPostModel>();
    }


}
