namespace ProfilesAPI.Services.Abstraction.AggregatesModels.PatientAggregate
{
    public record CreatePatientModel
    {
        public CreateHumanInfo Info { get; private set; }
        public string PhoneNumber { get; set; }

        public CreatePatientModel(CreateHumanInfo info, string phoneNumber)
        {
            Info = info;
            PhoneNumber = phoneNumber;
        }
    }
}
