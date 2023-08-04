namespace ProfilesAPI.Domain.Interfaces
{
    public interface IRepositoryManager
    {
        public IProfileRepository ProfileRepository { get; }
        public IHumanInfoRepository HumanInfoRepository { get; }
    }
}
