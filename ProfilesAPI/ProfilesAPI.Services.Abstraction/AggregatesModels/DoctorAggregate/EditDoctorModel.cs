using Microsoft.AspNetCore.Http;

namespace ProfilesAPI.Services.Abstraction.AggregatesModels.DoctorAggregate
{
    public record EditDoctorModel(IFormFile? Photo,
                                     string FirstName,
                                     string LastName,
                                     string MiddleName,
                                     DateTime BirthDay,
                                     string Specialization,
                                     Guid OfficeId,
                                     int CareerStartYear,
                                     WorkStatusDTO Status);
}
