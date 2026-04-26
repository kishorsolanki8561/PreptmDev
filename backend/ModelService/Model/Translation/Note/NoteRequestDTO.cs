using AutoMapper;
using ModelService.CommonModel;
using ModelService.MDL.Translation;
using ModelService.Model.Translation.Paper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelService.Model.Translation.Note
{
    public class NoteRequestDTO : IMapFrom<Note_MDL>
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
        public string? DescriptionJson { get; set; }
        public string? DescriptionJsonHindi { get; set; }
        public string Keywords { get; set; }
        public string KeywordsHindi { get; set; }
        public string? ShortDescription { get; set; }
        public string? ShortDescriptionHindi { get; set; }
        public ICollection<NoteSubjectRequestDTO> Note_Subjects { get; set; }
        public ICollection<int> Note_Tags { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<NoteRequestDTO, Note_MDL>().ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember, context) =>
                (srcMember != null && destMember != null &&
                srcMember.GetType() == destMember.GetType() &&
                srcMember.Equals(destMember) == false) || destMember == null
            ));

            profile.CreateMap<NoteRequestDTO, Note_MDL>()
           .ForMember(dest => dest.Note_Subjects, opt => opt.Ignore()) // Customize mapping for subjects if needed
           .ForMember(dest => dest.Note_Tags, opt => opt.Ignore()) // Customize mapping for tags if needed
           .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }
    }

    public class NoteSubjectRequestDTO : IMapFrom<Note_Subject_MDL>
    {
        public int Id { get; set; }
        [JsonIgnore]
        public int NoteId { get; set; }
        public string SubjectName { get; set; }
        public string SubjectNameHindi { get; set; }
        public int? YearId { get; set; }
        public string Path { get; set; }
        public void Mapping(Profile profile)
        {
            _ = profile.CreateMap<NoteSubjectRequestDTO, Note_Subject_MDL>().ReverseMap();
        }
    }

    public class NoteTagRequestDTO : IMapFrom<NoteTags_MDL>
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        [JsonIgnore]
        public int NoteId { get; set; }
        public void Mapping(Profile profile)
        {
            _ = profile.CreateMap<NoteTagRequestDTO, NoteTags_MDL>().ReverseMap();
        }
    }

    public class NoteFilterDTO : IndexModel
    {
        public string? Title { get; set; }
        public string? TitleHindi { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
    }
}
