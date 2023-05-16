using ProfilesAPI.Services.Abstraction.AggregatesModels;
using ProfilesAPI.Services.Abstraction.AggregatesModels.PatientAggregate;
using ProfilesAPI.Services.Abstraction.QueryableManipulation;

namespace ProfilesAPI.Services.Abstraction
{
    public interface IPatientService
    {
        Task<PatientDTO> CreatePatientAsync(CreatePatientModel model, CancellationToken cancellationToken = default);
        Task DeletePatientAsync(Guid id, CancellationToken cancellationToken = default);
        Task<PatientDTO> EditPatientAsync(Guid id, EditPatientModel model, CancellationToken cancellationToken = default);
        Task<PatientDTO> GetPatientAsync(Guid id, CancellationToken cancellationToken = default);
        IEnumerable<PatientDTO> GetPatients(Page page, PatientFiltrationModel filtrationModel);
        IEnumerable<PatientDTO> GetPatientsInfo(Page page, PatientFiltrationModel filtrationModel);
    }
}