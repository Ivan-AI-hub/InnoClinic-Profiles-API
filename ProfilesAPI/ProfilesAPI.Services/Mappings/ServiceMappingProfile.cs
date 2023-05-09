using AutoMapper;
using ProfilesAPI.Application.Commands.Patients.Create;
using ProfilesAPI.Domain;
using ProfilesAPI.Services.Models;

namespace ProfilesAPI.Services.Mappings
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<CreatePatientModel, CreatePatient>()
                 .ForMember(s => s.Info.Photo.Name, r => r.MapFrom(d => d.Photo.Name));
            
            CreateMap<Patient, PatientDTO>().ReverseMap();

            CreateMap<HumanInfo, HumanInfoDTO>()
                .ForMember(s => s.Photo, r => r.Ignore())
                .ReverseMap();
        }
    }
}
