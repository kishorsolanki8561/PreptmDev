using AutoMapper;
using ModelService.Model.MastersModel;

namespace MasterService
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserMasterViewModel, UserMasterViewModel>().ReverseMap();
            CreateMap<UserViewModel, UserViewModel>().ReverseMap();
            CreateMap<UserMasterModel, UserMasterModel>().ReverseMap();
            CreateMap<LoginModel, LoginModel>().ReverseMap();
            CreateMap<DepartmentMasterModel, DepartmentMasterModel>().ReverseMap();
            CreateMap<DepartmentMasterModel, DepartmentMasterModel>().ReverseMap();
        }
    }
}
