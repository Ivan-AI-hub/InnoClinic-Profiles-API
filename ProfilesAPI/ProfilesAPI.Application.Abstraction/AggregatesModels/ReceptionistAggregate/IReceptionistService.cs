﻿namespace ProfilesAPI.Application.Abstraction.AggregatesModels.ReceptionistAggregate
{
    public interface IReceptionistService
    {
        Task<ReceptionistDTO> CreateReceptionistAsync(CreateReceptionistModel model, CancellationToken cancellationToken = default);
        Task DeleteReceptionistAsync(Guid id, CancellationToken cancellationToken = default);
        Task EditReceptionistAsync(Guid id, EditReceptionistModel model, CancellationToken cancellationToken = default);
        Task<ReceptionistDTO> GetReceptionistAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ReceptionistDTO> GetReceptionistAsync(string email, CancellationToken cancellationToken = default);
        Task<IEnumerable<ReceptionistDTO>> GetReceptionistsAsync(Page page, ReceptionistFiltrationModel filtrationModel, CancellationToken cancellationToken = default);
    }
}
