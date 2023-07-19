namespace ProfilesAPI.Application.Abstraction.AggregatesModels.DoctorAggregate
{
    public interface IDoctorService
    {
        Task<DoctorDTO> CreateDoctorAsync(CreateDoctorModel model, CancellationToken cancellationToken = default);
        Task EditDoctorAsync(Guid id, EditDoctorModel model, CancellationToken cancellationToken = default);
        Task EditDoctorStatusAsync(Guid id, WorkStatusDTO workStatus, CancellationToken cancellationToken = default);
        Task<DoctorDTO> GetDoctorAsync(Guid id, CancellationToken cancellationToken = default);
        Task<DoctorDTO> GetDoctorAsync(string email, CancellationToken cancellationToken = default);
        Task<IEnumerable<DoctorDTO>> GetDoctorsAsync(Page page, DoctorFiltrationModel filtrationModel, CancellationToken cancellationToken = default);
    }
}
