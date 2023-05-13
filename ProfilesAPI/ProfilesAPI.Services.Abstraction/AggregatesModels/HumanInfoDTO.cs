namespace ProfilesAPI.Services.Abstraction.AggregatesModels
{
    public class HumanInfoDTO
    {
        public Blob? Photo { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateOnly BirthDay { get; set; }

        public HumanInfoDTO(Blob? photo, string email, string firstName, string lastName, string middleName, DateOnly birthDay)
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
