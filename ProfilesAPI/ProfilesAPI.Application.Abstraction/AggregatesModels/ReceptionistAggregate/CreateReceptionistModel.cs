using ProfilesAPI.Application.Abstraction.AggregatesModels.DoctorAggregate;

namespace ProfilesAPI.Application.Abstraction.AggregatesModels.ReceptionistAggregate
{
    public record CreateReceptionistModel(CreateHumanInfo Info, OfficeDTO Office);
}