namespace ProfilesAPI.Domain
{
    public class HumanInfo
    {
        public Guid Id { get; private set; }
        public Picture? Photo { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateOnly BirthDay { get; set; }
        private HumanInfo() { }
        public HumanInfo(Picture photo, string email, string firstName, string lastName, string middleName, DateOnly birthDay)
        {
            Photo = photo;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            BirthDay = birthDay;
        }
    }
}
