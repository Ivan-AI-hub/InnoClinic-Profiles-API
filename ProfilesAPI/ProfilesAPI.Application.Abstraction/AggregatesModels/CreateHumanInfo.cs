using Microsoft.AspNetCore.Http;

namespace ProfilesAPI.Application.Abstraction.AggregatesModels
{
    public class CreateHumanInfo
    {
        public IFormFile? Photo { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDay { get; set; }
        public CreateHumanInfo() { }
        public CreateHumanInfo(IFormFile? photo, string email, string firstName, string lastName, string middleName, DateTime birthDay)
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
