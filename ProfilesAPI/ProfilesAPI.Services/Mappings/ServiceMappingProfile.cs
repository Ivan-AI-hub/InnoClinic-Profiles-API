using AutoMapper;
using ProfilesAPI.Domain;
using ProfilesAPI.Services.Abstraction.AggregatesModels;
using ProfilesAPI.Services.Abstraction.AggregatesModels.DoctorAggregate;
using ProfilesAPI.Services.Abstraction.AggregatesModels.PatientAggregate;
using ProfilesAPI.Services.Abstraction.QueryableManipulation;
using ProfilesAPI.Services.Filtrators;

namespace ProfilesAPI.Services.Mappings
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<Patient, PatientDTO>();
            CreateMap<PatientFiltrationModel, IFiltrator<Patient>>().As<PatientFiltrator>();
            CreateMap<PatientFiltrationModel, PatientFiltrator>();
            CreateMap<CreatePatientModel, Patient>();
            CreateMap<EditPatientModel, Patient>().ForMember(x => x.Info,
                    s => s.MapFrom(t => new HumanInfo("", t.FirstName, t.LastName, t.MiddleName, t.BirthDay)
                    { Photo = t.Photo != null ? new Picture(t.Photo.FileName) : null }));

            CreateMap<Doctor, DoctorDTO>();
            CreateMap<Office, OfficeDTO>();
            CreateMap<DoctorFiltrationModel, IFiltrator<Doctor>>().As<DoctorFiltrator>();
            CreateMap<DoctorFiltrationModel, DoctorFiltrator>();
            CreateMap<CreateDoctorModel, Doctor>().ForMember(x => x.Office, r => r.MapFrom(t => new Office(t.OfficeId)));
            CreateMap<EditDoctorModel, Doctor>()
                .ForMember(x => x.Info,
                s => s.MapFrom(t => new HumanInfo("", t.FirstName, t.LastName, t.MiddleName, t.BirthDay)
                { Photo = t.Photo != null ? new Picture(t.Photo.FileName) : null }))
                .ForMember(x => x.Office, s => s.MapFrom(t => new Office(t.OfficeId)));

            CreateMap<HumanInfo, HumanInfoDTO>().ForMember(s => s.Photo, r => r.Ignore());
            CreateMap<CreateHumanInfo, HumanInfo>()
                .ForMember(s => s.Photo, r => r.MapFrom(t => new Picture(t.Photo.FileName)))
                .ReverseMap();
        }
    }
}
