namespace ProfilesAPI.Services.Abstraction.AggregatesModels.DoctorAggregate
{
    public class OfficeDTO
    {
        public Guid Id { get; private set; }
        public OfficeDTO(Guid id)
        {
            Id = id;
        }

    }
}
