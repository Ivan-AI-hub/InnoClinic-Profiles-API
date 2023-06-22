using ProfilesAPI.Application.Abstraction.AggregatesModels.DoctorAggregate;
using ProfilesAPI.Application.Abstraction.AggregatesModels.HumanInfoAggregate;
using ProfilesAPI.Application.Abstraction.AggregatesModels.OfficeAggregate;
using ProfilesAPI.Application.Abstraction.AggregatesModels.PatientAggregate;
using ProfilesAPI.Application.Abstraction.AggregatesModels.ReceptionistAggregate;
using ProfilesAPI.Application.Filtrators;
using ProfilesAPI.Domain;
using ProfilesAPI.Domain.Interfaces;

namespace ProfilesAPI.Application.Mappings
{
    public class ServiceMappingProfile : AutoMapper.Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<Profile, PatientDTO>();
            CreateMap<PatientFiltrationModel, IFiltrator<Profile>>().As<PatientFiltrator>();
            CreateMap<PatientFiltrationModel, PatientFiltrator>();
            CreateMap<CreatePatientModel, Profile>();
            CreateMap<EditPatientModel, Profile>().ForMember(x => x.Info,
                    s => s.MapFrom(t => new HumanInfo("", t.FirstName, t.LastName, t.MiddleName, t.BirthDay)
                    { Photo = t.Photo != null ? new Picture(t.Photo.Name) : null }));

            CreateMap<Profile, DoctorDTO>();
            CreateMap<DoctorFiltrationModel, IFiltrator<Profile>>().As<DoctorFiltrator>();
            CreateMap<DoctorFiltrationModel, DoctorFiltrator>();
            CreateMap<CreateDoctorModel, Profile>().ForMember(x => x.Office, r => r.MapFrom(t => new Office(t.OfficeId)));
            CreateMap<EditDoctorModel, Profile>()
                .ForMember(x => x.Info,
                s => s.MapFrom(t => new HumanInfo("", t.FirstName, t.LastName, t.MiddleName, t.BirthDay)
                { Photo = t.Photo != null ? new Picture(t.Photo.Name) : null }))
                .ForMember(x => x.Office, s => s.MapFrom(t => new Office(t.OfficeId)));

            CreateMap<Profile, ReceptionistDTO>();
            CreateMap<ReceptionistFiltrationModel, IFiltrator<Profile>>().As<ReceptionistFiltrator>();
            CreateMap<ReceptionistFiltrationModel, ReceptionistFiltrator>();
            CreateMap<CreateReceptionistModel, Profile>();
            CreateMap<EditReceptionistModel, Profile>()
                .ForMember(x => x.Info,
                s => s.MapFrom(t => new HumanInfo("", t.FirstName, t.LastName, t.MiddleName, t.BirthDay)
                { Photo = t.Photo != null ? new Picture(t.Photo.Name) : null }))
                .ForMember(x => x.Office, s => s.MapFrom(t => new Office(t.OfficeId)));

            CreateMap<Office, OfficeDTO>().ReverseMap();

            CreateMap<Picture, PictureDTO>().ReverseMap();
            CreateMap<HumanInfo, HumanInfoDTO>().ForMember(s => s.Photo, r => r.Ignore());
            CreateMap<CreateHumanInfo, HumanInfo>().ReverseMap();
        }
    }
}
