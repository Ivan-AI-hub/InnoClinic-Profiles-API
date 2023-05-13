using ProfilesAPI.Services.Abstraction.AggregatesModels.PatientAggregate;

namespace ProfilesAPI.Services.Abstraction
{
    public interface IPatientService
    {
        Task<PatientDTO> CreatePatientAsync(CreatePatientModel model, CancellationToken cancellationToken = default);
        Task<PatientDTO> GetPatientAsync(Guid id, CancellationToken cancellationToken = default);
    }
}