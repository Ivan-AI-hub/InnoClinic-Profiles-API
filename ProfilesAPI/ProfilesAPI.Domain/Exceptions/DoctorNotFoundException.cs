namespace ProfilesAPI.Domain.Exceptions
{
    public class DoctorNotFoundException : NotFoundException
    {
        public DoctorNotFoundException(Guid id)
            : base($"Doctor with ID = {id} does not exist")
        {
        }
    }
}
