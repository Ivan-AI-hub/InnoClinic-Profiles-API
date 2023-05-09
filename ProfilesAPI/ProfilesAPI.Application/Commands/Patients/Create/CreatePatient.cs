using MediatR;
using ProfilesAPI.Application.Results;
using ProfilesAPI.Domain;

namespace ProfilesAPI.Application.Commands.Patients.Create
{
    public record CreatePatient(HumanInfo HumanInfo, string phoneNumber) 
        : IRequest<ApplicationValueResult<Patient>>;
}
