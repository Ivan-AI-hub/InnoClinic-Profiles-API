using ProfilesAPI.Application.Abstraction.AggregatesModels.DoctorAggregate;

namespace ProfilesAPI.Presentation.Models.RequestModels
{
    public record EditStatusRequestModel(WorkStatusDTO WorkStatus);
}
