using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Translation
{
    public class Syllabus_MDL : AuditableEntity
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public int? DepartmentId { get; set; }
        public int? QualificationId { get; set; }
        public int? StateId { get; set; }
        public string Title { get; set; }
        public string TitleHindi { get; set; }
        public string SlugUrl { get; set; }
        public string Description { get; set; }
        public string DescriptionHindi { get; set; }
        public string Keywords { get; set; }
        public string KeywordsHindi { get; set; }
        public int? VisitCount { get; set; }
        public int? PublisherId { get; set; }
        public DateTime? PublisherDate { get; set; }
        public int? Status { get; set; }
        public string? DescriptionJson { get; set; }
        public string? DescriptionJsonHindi { get; set; }
        public ICollection<Syllabus_Subject_MDL> Syllabus_Subjects { get; set; }
        public ICollection<SyllabusTags_MDL> SyllabusTags { get; set; }
        public string? ShortDescription { get; set; }
        public string? ShortDescriptionHindi { get; set; }

    }
}
