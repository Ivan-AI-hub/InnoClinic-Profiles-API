using Microsoft.AspNetCore.Http;

namespace ProfilesAPI.Application.Abstraction.AggregatesModels.ReceptionistAggregate
{
    public record EditReceptionistModel(IFormFile? Photo,
                                     string FirstName,
                                     string LastName,
                                     string MiddleName,
                                     DateTime BirthDay,
                                     Guid OfficeId);
}