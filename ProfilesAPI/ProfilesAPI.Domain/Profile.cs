namespace ProfilesAPI.Domain
{
    public class Profile
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public HumanInfo Info { get; private set; }
        public Role Role { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Specialization { get; set; }
        public Office? Office { get; set; }
        public int? CareerStartYear { get; set; }
        public WorkStatus? Status { get; set; }
        private Profile() { }
        public Profile(HumanInfo info, Role role, string? phoneNumber, string? specialization, Office? office, int? careerStartYear, WorkStatus? status)
        {
            Info = info;
            Role = role;
            PhoneNumber = phoneNumber;
            Specialization = specialization;
            Office = office;
            CareerStartYear = careerStartYear;
            Status = status;
        }
    }
}
