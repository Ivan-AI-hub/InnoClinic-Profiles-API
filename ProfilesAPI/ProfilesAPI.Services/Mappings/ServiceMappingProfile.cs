using AutoMapper;
using ProfilesAPI.Domain;
using ProfilesAPI.Services.Abstraction.AggregatesModels;
using ProfilesAPI.Services.Abstraction.AggregatesModels.PatientAggregate;

namespace ProfilesAPI.Services.Mappings
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<CreatePatientModel, Patient>()
                 .ForMember(s => s.Info.Photo.Name, r => r.MapFrom(d => d.Photo.Name));

            CreateMap<Patient, PatientDTO>().ReverseMap();

            CreateMap<HumanInfo, HumanInfoDTO>()
                .ForMember(s => s.Photo, r => r.Ignore())
                .ReverseMap();
        }
    }
}
