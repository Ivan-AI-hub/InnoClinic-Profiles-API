using ProfilesAPI.Application.Abstraction.AggregatesModels.HumanInfoAggregate;
using ProfilesAPI.Application.Abstraction.AggregatesModels.OfficeAggregate;

namespace ProfilesAPI.Application.Abstraction.AggregatesModels.ReceptionistAggregate
{
    public record CreateReceptionistModel(CreateHumanInfo Info, OfficeDTO Office);
}