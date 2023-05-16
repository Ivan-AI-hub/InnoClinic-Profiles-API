namespace ProfilesAPI.Services.Abstraction.AggregatesModels.DoctorAggregate
{
    public record CreateDoctorModel
    {
        public CreateHumanInfo Info { get; private set; }
        public string Specialization { get; set; }
        public Guid OfficeId { get; set; }
        public int CareerStartYear { get; set; }

        public CreateDoctorModel(CreateHumanInfo info, string specialization, Guid officeId, int careerStartYear)
        {
            Info = info;
            Specialization = specialization;
            OfficeId = officeId;
            CareerStartYear = careerStartYear;
        }
    }
}
