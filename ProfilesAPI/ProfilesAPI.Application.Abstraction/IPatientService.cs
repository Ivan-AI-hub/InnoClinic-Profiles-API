using ProfilesAPI.Application.Abstraction.AggregatesModels;
using ProfilesAPI.Application.Abstraction.AggregatesModels.PatientAggregate;
using ProfilesAPI.Application.Abstraction.QueryableManipulation;

namespace ProfilesAPI.Application.Abstraction
{
    public interface IPatientService
    {
        Task<PatientDTO> CreatePatientAsync(CreatePatientModel model, CancellationToken cancellationToken = default);
        Task DeletePatientAsync(Guid id, CancellationToken cancellationToken = default);
        Task EditPatientAsync(Guid id, EditPatientModel model, CancellationToken cancellationToken = default);
        Task<PatientDTO> GetPatientAsync(Guid id, CancellationToken cancellationToken = default);
        IEnumerable<PatientDTO> GetPatients(Page page, PatientFiltrationModel filtrationModel);
        IEnumerable<PatientDTO> GetPatientsInfo(Page page, PatientFiltrationModel filtrationModel);
    }
}