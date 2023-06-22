namespace ProfilesAPI.Application.Abstraction.AggregatesModels.ProfileAggregate
{
    public interface IProfileService
    {
        public Task UpdateRoleAsync(string email, string role, CancellationToken cancellationToken = default);
    }
}
