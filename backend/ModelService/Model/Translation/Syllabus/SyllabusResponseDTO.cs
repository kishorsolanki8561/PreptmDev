using AutoMapper;
using ModelService.MDL.Translation;
using ModelService.Model.Translation.Paper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelService.Model.Translation.Syllabus
{
    public class SyllabusResponseDTO : IMapFrom<Syllabus_MDL>
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
        public string? DescriptionJson { get; set; }
        public string? DescriptionJsonHindi { get; set; }
        public int? PublisherId { get; set; }
        public DateTime? PublisherDate { get; set; }
        public string? ShortDescription { get; set; }
        public string? ShortDescriptionHindi { get; set; }
        public ICollection<SyllabusSubjecResponseDTO> Syllabus_Subjects { get; set; }
        public ICollection<int> SyllabusTags { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Syllabus_MDL, SyllabusResponseDTO>()
           .ForMember(dest => dest.Syllabus_Subjects, opt => opt.Ignore()) // Customize mapping for subjects if needed
           .ForMember(dest => dest.SyllabusTags, opt => opt.Ignore()) // Customize mapping for tags if needed
           .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }

    public class SyllabusSubjecResponseDTO : IMapFrom<Syllabus_Subject_MDL>
    {
        public int Id { get; set; }
        [JsonIgnore]
        public int SyllabusId { get; set; }
        public string SubjectName { get; set; }
        public string SubjectNameHindi { get; set; }
        public int? YearId { get; set; }
        public string Path { get; set; }
        public void Mapping(Profile profile)
        {
            _ = profile.CreateMap<Syllabus_Subject_MDL, SyllabusSubjecResponseDTO>();
        }
    }

    public class SyllabusTagResponseDTO : IMapFrom<SyllabusTags_MDL>
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        [JsonIgnore]
        public int SyllabusId { get; set; }
        public void Mapping(Profile profile)
        {
            _ = profile.CreateMap<SyllabusTags_MDL, SyllabusTagResponseDTO>();
        }
    }

    public class SyllabusTitleCheckDTO
    {
        public string? Title { get; set; }
        public string SlugUrl { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }

    }

    public class SyllabusViewListDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleHindi { get; set; }
        public string SlugUrl { get; set; }
        public string ModifiedByName { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int Status { get; set; }
        public int VisitCount { get; set; }
        public string PublisherName { get; set; }
        public string PublisherDate { get; set; }
        public bool IsActive { get; set; }
        [JsonIgnore]
        public int TotalRows { get; set; }
    }
}
