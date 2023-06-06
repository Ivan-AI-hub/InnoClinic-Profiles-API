namespace ProfilesAPI.Domain
{
    public class Doctor
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public HumanInfo Info { get; private set; }
        public string Specialization { get; set; }
        public Office Office { get; private set; }
        public int CareerStartYear { get; set; }
        public WorkStatus Status { get; set; }
        private Doctor() { }
        public Doctor(HumanInfo info, string specialization,
            Office office, int careerStartYear, WorkStatus status)
        {
            Info = info;
            Specialization = specialization;
            Office = office;
            CareerStartYear = careerStartYear;
            Status = status;
        }

        public Doctor(Guid id, HumanInfo info, string specialization,
            Office office, int careerStartYear, WorkStatus status) : this(info, specialization, office, careerStartYear, status)
        {
            Id = id;
        }
    }
}
