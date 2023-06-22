using ProfilesAPI.Application.Abstraction.AggregatesModels.ProfileAggregate;
using ProfilesAPI.Domain;
using ProfilesAPI.Domain.Interfaces;

namespace ProfilesAPI.Application
{
    public class ProfileService : IProfileService
    {
        private IRepositoryManager _repositoryManager;

        public ProfileService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task UpdateRoleAsync(string email, string role, CancellationToken cancellationToken = default)
        {
            await _repositoryManager.ProfileRepository.UpdateRoleAsync(email, Enum.Parse<Role>(role), cancellationToken);
        }
    }
}
