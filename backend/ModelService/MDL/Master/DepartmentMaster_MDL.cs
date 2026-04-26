using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelService.MDL.Master
{
    public class DepartmentMaster_MDL :AuditableEntity
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
        public string? NameHindi { get; set; }
        public string? AddressHindi { get; set; }
        public string? DescriptionHindi { get; set; }
        public string? DescriptionHindiJson { get; set; }
        public string? WikipediaEnglishUrl { get; set; }
        public string? WikipediaHindiUrl { get; set; }
    }
}
