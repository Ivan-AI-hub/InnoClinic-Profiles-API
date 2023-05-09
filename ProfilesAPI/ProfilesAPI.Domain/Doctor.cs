namespace ProfilesAPI.Domain
{
    public class Doctor
    {
        public Guid Id { get; private set; }
        public HumanInfo Info { get; private set; }
        public string Specialization { get; private set; }
        public Office Office { get; private set; }
        public int CareerStartYear { get; private set; }
        public WorkStatus Status { get; private set; }

        public Doctor(HumanInfo info, string specialization,
            Office office, int careerStartYear, WorkStatus status)
        {
            Id = Guid.NewGuid();
            Info = info;
            Specialization = specialization;
            Office = office;
            CareerStartYear = careerStartYear;
            Status = status;
        }
    }
}
