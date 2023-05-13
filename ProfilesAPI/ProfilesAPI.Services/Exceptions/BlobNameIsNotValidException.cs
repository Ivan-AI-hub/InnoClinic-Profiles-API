using ProfilesAPI.Domain.Exceptions;
namespace ProfilesAPI.Services.Exceptions
{
    public class BlobNameIsNotValidException : BadRequestException
    {
        public BlobNameIsNotValidException(string name)
            : base($"File with the {name} name already exist in database")
        {
        }
    }
}
