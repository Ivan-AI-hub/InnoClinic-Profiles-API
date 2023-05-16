namespace ProfilesAPI.Domain
{
    public class Patient
    {
        public Guid Id { get; private set; }
        public HumanInfo Info { get; private set; }
        public string PhoneNumber { get; set; }

        private Patient() { }
        public Patient(HumanInfo info, string phoneNumber)
        {
            Id = Guid.NewGuid();
            Info = info;
            PhoneNumber = phoneNumber;
        }
    }
}
