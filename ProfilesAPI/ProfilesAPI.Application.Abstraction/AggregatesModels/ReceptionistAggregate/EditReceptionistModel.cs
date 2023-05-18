using Microsoft.AspNetCore.Http;
using ProfilesAPI.Application.Abstraction.AggregatesModels.DoctorAggregate;

namespace ProfilesAPI.Application.Abstraction.AggregatesModels.ReceptionistAggregate
{
    public record EditReceptionistModel(IFormFile? Photo,
                                     string FirstName,
                                     string LastName,
                                     string MiddleName,
                                     DateTime BirthDay,
                                     Guid OfficeId);
}