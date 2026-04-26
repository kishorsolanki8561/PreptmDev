using AutoMapper;
using ModelService.CommonModel;
using ModelService.MDL.Translation;
using System.Text.Json.Serialization;

namespace ModelService.Model.Translation.Paper
{
    public class PaperRequestDTO : IMapFrom<Paper_MDL>
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
        public int? Status { get; set; }
        public List<int> PaperTags { get; set; } = new List<int>();
        public string? ShortDescription { get; set; }
        public string? ShortDescriptionHindi { get; set; }
        public List<PapperSubjectRequestDTO> PapperSubjects { get; set; }/// = new List<PapperSubjectRequestDTO>();
        public void Mapping(Profile profile)
        {
            profile.CreateMap<PaperRequestDTO, Paper_MDL>().ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember, context) =>
                (srcMember != null && destMember != null &&
                srcMember.GetType() == destMember.GetType() &&
                srcMember.Equals(destMember) == false) || destMember == null
            ));

            profile.CreateMap<PaperRequestDTO, Paper_MDL>()
           .ForMember(dest => dest.Paper_Subjects, opt => opt.Ignore()) // Customize mapping for subjects if needed
           .ForMember(dest => dest.PaperTags, opt => opt.Ignore()) // Customize mapping for tags if needed
           .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
         
        }
    }

    public class PapperSubjectRequestDTO : IMapFrom<PaperSubject_MDL>
    {
        public int Id { get; set; }
        [JsonIgnore]
        public int PaperId { get; set; }
        public string SubjectName { get; set; }
        public string SubjectNameHindi { get; set; }
        public int? YearId { get; set; }
        public string Path { get; set; }
        public void Mapping(Profile profile)
        {
            _ = profile.CreateMap<PapperSubjectRequestDTO, PaperSubject_MDL>().ReverseMap();
        }
    }

    public class PapperTagRequestDTO : IMapFrom<PaperTags_MDL>
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        [JsonIgnore]
        public int PaperId { get; set; }
        public void Mapping(Profile profile)
        {
            _ = profile.CreateMap<PapperTagRequestDTO, PaperTags_MDL>();
        }
    }

    public class PaperFilterDTO : IndexModel
    {
        public string? Title { get; set; }
        public string? TitleHindi { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
    }
}
