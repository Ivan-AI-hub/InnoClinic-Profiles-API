using ProfilesAPI.Domain.Interfaces;

namespace ProfilesAPI.Persistence.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IProfileRepository> _profileRepository;
        private readonly Lazy<IHumanInfoRepository> _humanInfoRepository;

        public RepositoryManager(ProfilesContext context)
        {
            _profileRepository = new Lazy<IProfileRepository>(() => new ProfileRepository(context));
            _humanInfoRepository = new Lazy<IHumanInfoRepository>(() => new HumanInfoRepository(context));
        }

        public IProfileRepository ProfileRepository => _profileRepository.Value;
        public IHumanInfoRepository HumanInfoRepository => _humanInfoRepository.Value;
    }
}
