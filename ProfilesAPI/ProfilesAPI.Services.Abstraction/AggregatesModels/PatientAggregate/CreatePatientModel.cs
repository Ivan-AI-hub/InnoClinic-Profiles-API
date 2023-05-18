namespace ProfilesAPI.Services.Abstraction.AggregatesModels.PatientAggregate
{
    public record CreatePatientModel(CreateHumanInfo Info)
    {
        public string PhoneNumber { get; set; } = "";
    }
}
