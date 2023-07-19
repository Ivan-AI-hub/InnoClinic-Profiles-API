using ProfilesAPI.Application.Abstraction.AggregatesModels.HumanInfoAggregate;

namespace ProfilesAPI.Application.Abstraction.AggregatesModels.PatientAggregate
{
    public record EditPatientModel(PictureDTO? Photo,
                                     string FirstName,
                                     string LastName,
                                     string MiddleName,
                                     DateTime BirthDay,
                                     string PhoneNumber);
}
