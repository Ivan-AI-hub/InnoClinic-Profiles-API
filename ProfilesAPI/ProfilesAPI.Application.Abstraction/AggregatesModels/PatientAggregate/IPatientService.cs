﻿namespace ProfilesAPI.Application.Abstraction.AggregatesModels.PatientAggregate
{
    public interface IPatientService
    {
        Task<PatientDTO> CreatePatientAsync(CreatePatientModel model, CancellationToken cancellationToken = default);
        Task DeletePatientAsync(Guid id, CancellationToken cancellationToken = default);
        Task EditPatientAsync(Guid id, EditPatientModel model, CancellationToken cancellationToken = default);
        Task<PatientDTO> GetPatientAsync(Guid id, CancellationToken cancellationToken = default);
        Task<PatientDTO> GetPatientAsync(string email, CancellationToken cancellationToken = default);
        Task<IEnumerable<PatientDTO>> GetPatientsAsync(Page page, PatientFiltrationModel filtrationModel, CancellationToken cancellationToken = default);
    }
}