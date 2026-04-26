using AutoMapper;
using ModelService.CommonModel;
using ModelService.MDL.Translation;
using ModelService.Model.Translation.Note;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelService.Model.Translation.Syllabus
{
    public class SyllabusRequestDTO : IMapFrom<Syllabus_MDL>
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
        public ICollection<SyllabusSubjectRequestDTO> Syllabus_Subjects { get; set; }
        public ICollection<int> SyllabusTags { get; set; }
        public string? ShortDescription { get; set; }
        public string? ShortDescriptionHindi { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SyllabusRequestDTO, Syllabus_MDL>().ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember, context) =>
                (srcMember != null && destMember != null &&
                srcMember.GetType() == destMember.GetType() &&
                srcMember.Equals(destMember) == false) || destMember == null
            ));

            profile.CreateMap<SyllabusRequestDTO, Syllabus_MDL>()
           .ForMember(dest => dest.Syllabus_Subjects, opt => opt.Ignore()) // Customize mapping for subjects if needed
           .ForMember(dest => dest.SyllabusTags, opt => opt.Ignore()) // Customize mapping for tags if needed
           .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }
    }

    public class SyllabusSubjectRequestDTO : IMapFrom<Syllabus_Subject_MDL>
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
            _ = profile.CreateMap<SyllabusSubjectRequestDTO, Syllabus_Subject_MDL>().ReverseMap();
        }
    }

    public class SyllabusTagRequestDTO : IMapFrom<SyllabusTags_MDL>
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        [JsonIgnore]
        public int SyllabusId { get; set; }
        public void Mapping(Profile profile)
        {
            _ = profile.CreateMap<SyllabusTagRequestDTO, SyllabusTags_MDL>().ReverseMap();
        }
    }

    public class SyllabusFilterDTO : IndexModel
    {
        public string? Title { get; set; }
        public string? TitleHindi { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
    }
}
