namespace ProfilesAPI.Domain.Exceptions
{
    public class ProfileWithSameEmailExistException : BadRequestException
    {
        public ProfileWithSameEmailExistException(string email)
            : base($"Profile with email={email} already exist")
        {
        }
    }
}
