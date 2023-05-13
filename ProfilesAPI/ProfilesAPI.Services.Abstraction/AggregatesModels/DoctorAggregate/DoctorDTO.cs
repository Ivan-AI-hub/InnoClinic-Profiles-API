namespace ProfilesAPI.Services.Abstraction.AggregatesModels.DoctorAggregate
{
    public class DoctorDTO
    {
        public Guid Id { get; private set; }
        public HumanInfoDTO Info { get; private set; }
        public string Specialization { get; private set; }
        public OfficeDTO Office { get; private set; }
        public int CareerStartYear { get; private set; }
        public WorkStatusDTO Status { get; private set; }

        public DoctorDTO(Guid id, HumanInfoDTO info, string specialization, OfficeDTO office, int careerStartYear, WorkStatusDTO status)
        {
            Id = id;
            Info = info;
            Specialization = specialization;
            Office = office;
            CareerStartYear = careerStartYear;
            Status = status;
        }

    }
}
