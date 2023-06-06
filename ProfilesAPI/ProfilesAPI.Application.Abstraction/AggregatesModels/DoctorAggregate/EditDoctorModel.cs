using ProfilesAPI.Application.Abstraction.AggregatesModels.HumanInfoAggregate;

namespace ProfilesAPI.Application.Abstraction.AggregatesModels.DoctorAggregate
{
    public record EditDoctorModel(PictureDTO? Photo,
                                     string FirstName,
                                     string LastName,
                                     string MiddleName,
                                     DateTime BirthDay,
                                     string Specialization,
                                     Guid OfficeId,
                                     int CareerStartYear,
                                     WorkStatusDTO Status);
}
