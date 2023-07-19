namespace ProfilesAPI.Application.Abstraction.AggregatesModels.DoctorAggregate
{
    public class DoctorFiltrationModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? Specialization { get; set; }
        public Guid? OfficeId { get; set; }
    }
}
