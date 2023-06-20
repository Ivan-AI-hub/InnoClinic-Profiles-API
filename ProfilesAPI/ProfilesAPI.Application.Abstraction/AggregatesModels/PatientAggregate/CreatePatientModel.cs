using ProfilesAPI.Application.Abstraction.AggregatesModels.HumanInfoAggregate;

namespace ProfilesAPI.Application.Abstraction.AggregatesModels.PatientAggregate
{
    public record CreatePatientModel(CreateHumanInfo Info)
    {
        public string PhoneNumber { get; set; } = "";
    }
}
