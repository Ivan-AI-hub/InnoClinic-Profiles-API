using AutoMapper;
using ProfilesAPI.Application.Commands.Patients.Create;
using ProfilesAPI.Domain;

namespace ProfilesAPI.Application.Mappings
{
    internal class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile() 
        {
            CreateMap<CreatePatient, Patient>();
        }
    }
}
