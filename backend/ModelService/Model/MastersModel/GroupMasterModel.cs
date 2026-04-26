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
    public class GroupMasterModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? NameHindi { get; set; }
        [Required]
        public string SlugUrl { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }
    public class GroupMasterViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameHindi { get; set; }
        public string? SlugUrl { get; set; }
        public string? ModifiedByName { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public int TotalRows { get; set; }
    }

    public class GroupMasterFilterModel : IndexModel
    {

        public string Name { get; set; }
        public string SlugUrl { get; set; }
        public int IsActive { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
    }
}
