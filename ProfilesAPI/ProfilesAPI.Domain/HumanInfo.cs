namespace ProfilesAPI.Domain
{
    public class HumanInfo
    {
        public Picture Photo { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string MiddleName { get; private set; }
        public DateOnly BirthDay { get; private set; }
        private HumanInfo() { }
        public HumanInfo(Picture photo, string firstName, string lastName, string middleName, DateOnly birthDay)
        {
            Photo = photo;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            BirthDay = birthDay;
        }
    }
}
