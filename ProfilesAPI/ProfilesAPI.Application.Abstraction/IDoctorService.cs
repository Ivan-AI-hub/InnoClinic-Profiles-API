using ProfilesAPI.Application.Abstraction.AggregatesModels;
using ProfilesAPI.Application.Abstraction.AggregatesModels.DoctorAggregate;
using ProfilesAPI.Application.Abstraction.QueryableManipulation;

namespace ProfilesAPI.Application.Abstraction
{
    public interface IDoctorService
    {
        Task<DoctorDTO> CreateDoctorAsync(CreateDoctorModel model, CancellationToken cancellationToken = default);
        Task EditDoctorAsync(Guid id, EditDoctorModel model, CancellationToken cancellationToken = default);
        Task EditDoctorStatusAsync(Guid id, WorkStatusDTO workStatus, CancellationToken cancellationToken = default);
        Task<DoctorDTO> GetDoctorAsync(Guid id, CancellationToken cancellationToken = default);
        IEnumerable<DoctorDTO> GetDoctorsInfo(Page page, DoctorFiltrationModel filtrationModel);
        IEnumerable<DoctorDTO> GetDoctors(Page page, DoctorFiltrationModel filtrationModel);
    }
}
