namespace ProfilesAPI.Domain.Exceptions
{
    public class PatientNotFoundException : NotFoundException
    {
        public PatientNotFoundException(Guid id)
            : base($"Patient with ID = {id} does not exist")
        {
        }
    }
}
