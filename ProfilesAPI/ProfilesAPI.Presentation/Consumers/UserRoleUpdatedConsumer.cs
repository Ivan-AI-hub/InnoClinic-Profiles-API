using MassTransit;
using ProfilesAPI.Application.Abstraction.AggregatesModels.ProfileAggregate;
using SharedEvents.Models;

namespace ProfilesAPI.Presentation.Consumers
{
    public class UserRoleUpdatedConsumer : IConsumer<UserRoleUpdated>
    {
        private IProfileService _profileService;

        public UserRoleUpdatedConsumer(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public async Task Consume(ConsumeContext<UserRoleUpdated> context)
        {
            await _profileService.UpdateRoleAsync(context.Message.Email, context.Message.Role, context.CancellationToken);
        }
    }
}
