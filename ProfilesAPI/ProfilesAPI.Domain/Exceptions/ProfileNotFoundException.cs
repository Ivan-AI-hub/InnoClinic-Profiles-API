namespace ProfilesAPI.Domain.Exceptions
{
    public class ProfileNotFoundException : NotFoundException
    {
        public ProfileNotFoundException(Guid id)
            : base($"Profile with ID = {id} does not exist")
        {
        }

        public ProfileNotFoundException(string email)
            : base($"Profile with Email = {email} does not exist")
        {
        }
    }
}
