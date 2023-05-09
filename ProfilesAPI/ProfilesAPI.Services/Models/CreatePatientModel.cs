using Microsoft.AspNetCore.Http;

namespace ProfilesAPI.Services.Models
{
    public record CreatePatientModel(IFormFile? Photo, string Email, string FirstName,
        string LastName, string MiddleName, DateOnly BirthDay, string PhoneNumber);
}
