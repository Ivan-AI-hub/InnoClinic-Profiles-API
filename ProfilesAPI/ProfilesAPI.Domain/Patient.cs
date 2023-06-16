namespace ProfilesAPI.Domain
{
    public class Patient
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public HumanInfo Info { get; private set; }
        public string PhoneNumber { get; set; }

        private Patient() { }
        public Patient(HumanInfo info, string phoneNumber)
        {
            Info = info;
            PhoneNumber = phoneNumber;
        }
    }
}
