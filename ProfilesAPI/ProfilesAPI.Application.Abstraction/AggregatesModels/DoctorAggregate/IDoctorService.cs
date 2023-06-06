namespace ProfilesAPI.Application.Abstraction.AggregatesModels.DoctorAggregate
{
    public interface IDoctorService
    {
        Task<DoctorDTO> CreateDoctorAsync(CreateDoctorModel model, CancellationToken cancellationToken = default);
        Task EditDoctorAsync(Guid id, EditDoctorModel model, CancellationToken cancellationToken = default);
        Task EditDoctorStatusAsync(Guid id, WorkStatusDTO workStatus, CancellationToken cancellationToken = default);
        Task<DoctorDTO> GetDoctorAsync(Guid id, CancellationToken cancellationToken = default);
        IEnumerable<DoctorDTO> GetDoctors(Page page, DoctorFiltrationModel filtrationModel);
    }
}
