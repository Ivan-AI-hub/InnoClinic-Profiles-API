using ProfilesAPI.Services.Abstraction.AggregatesModels;
using ProfilesAPI.Services.Abstraction.AggregatesModels.DoctorAggregate;
using ProfilesAPI.Services.Abstraction.QueryableManipulation;

namespace ProfilesAPI.Services.Abstraction
{
    public interface IDoctorService
    {
        Task<DoctorDTO> CreateDoctorAsync(CreateDoctorModel model, CancellationToken cancellationToken = default);
        Task<DoctorDTO> EditDoctorAsync(Guid id, EditDoctorModel model, CancellationToken cancellationToken = default);
        Task EditDoctorStatusAsync(Guid id, WorkStatusDTO workStatus, CancellationToken cancellationToken = default);
        Task<DoctorDTO> GetDoctorAsync(Guid id, CancellationToken cancellationToken = default);
        IEnumerable<DoctorDTO> GetDoctors(Page page, DoctorFiltrationModel filtrationModel);
    }
}
