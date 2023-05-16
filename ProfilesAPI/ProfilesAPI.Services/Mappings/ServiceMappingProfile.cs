using AutoMapper;
using ProfilesAPI.Domain;
using ProfilesAPI.Services.Abstraction.AggregatesModels;
using ProfilesAPI.Services.Abstraction.AggregatesModels.PatientAggregate;
using ProfilesAPI.Services.Abstraction.QueryableManipulation;
using ProfilesAPI.Services.Filtrators;

namespace ProfilesAPI.Services.Mappings
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<CreatePatientModel, Patient>();
            CreateMap<EditPatientModel, CreatePatientModel>();

            CreateMap<Patient, PatientDTO>().ReverseMap();

            CreateMap<HumanInfo, HumanInfoDTO>()
                .ForMember(s => s.Photo, r => r.Ignore())
                .ReverseMap();

            CreateMap<CreateHumanInfo, HumanInfo>()
                .ForMember(s => s.Photo, r => r.MapFrom(t => new Picture(t.Photo.FileName)))
                .ReverseMap();

            CreateMap<PatientFiltrationModel, PatientFiltrator>();
            CreateMap<DoctorFiltrationModel, DoctorFiltrator>();
        }
    }
}
