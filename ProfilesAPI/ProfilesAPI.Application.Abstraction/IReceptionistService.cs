using ProfilesAPI.Application.Abstraction.AggregatesModels.ReceptionistAggregate;
using ProfilesAPI.Application.Abstraction.AggregatesModels;
using ProfilesAPI.Application.Abstraction.QueryableManipulation;

namespace ProfilesAPI.Application.Abstraction
{
    public interface IReceptionistService
    {
        Task<ReceptionistDTO> CreateReceptionistAsync(CreateReceptionistModel model, CancellationToken cancellationToken = default);
        Task DeleteReceptionistAsync(Guid id, CancellationToken cancellationToken = default);
        Task EditReceptionistAsync(Guid id, EditReceptionistModel model, CancellationToken cancellationToken = default);
        Task<ReceptionistDTO> GetReceptionistAsync(Guid id, CancellationToken cancellationToken = default);
        IEnumerable<ReceptionistDTO> GetReceptionists(Page page, ReceptionistFiltrationModel filtrationModel);
        IEnumerable<ReceptionistDTO> GetReceptionistsInfo(Page page, ReceptionistFiltrationModel filtrationModel);
    }
}
