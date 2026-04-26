using ModelService.CommonModel;
using System.Text.Json.Serialization;

namespace ModelService.MDL.Translation
{
    public class Paper_MDL : AuditableEntity
    {
        public int Id { get;set; }
        public int? CategoryId { get; set; }
        public int? DepartmentId { get; set; }
        public int? QualificationId { get; set; }
        public int? StateId { get; set; }
        public string Title { get;set; }
        public string TitleHindi { get; set; }
        public string SlugUrl { get; set; }
        public string Description { get; set; }
        public string DescriptionHindi { get; set; }
        public string? DescriptionJson { get; set; }
        public string? DescriptionJsonHindi { get; set; }
        public string Keywords { get; set; }
        public string KeywordsHindi { get; set; }
        public int? VisitCount { get; set; }
        public int? PublisherId { get; set; }
        public DateTime? PublisherDate { get; set; }
        public int? Status { get; set; }
        [JsonIgnore]
        public ICollection<PaperSubject_MDL> Paper_Subjects { get; set; }
        [JsonIgnore]
        public ICollection<PaperTags_MDL> PaperTags { get; set; }
        public string? ShortDescription { get; set; }
        public string? ShortDescriptionHindi { get; set; }

    }
}
