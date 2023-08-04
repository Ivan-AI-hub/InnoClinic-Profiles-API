namespace ProfilesAPI.Domain.Exceptions
{
    public class ReceptionistNotFoundException : NotFoundException
    {
        public ReceptionistNotFoundException(Guid id)
            : base($"Receptionist with ID = {id} does not exist")
        {
        }
    }
}
