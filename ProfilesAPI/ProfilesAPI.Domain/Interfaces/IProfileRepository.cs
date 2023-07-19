namespace ProfilesAPI.Domain.Interfaces
{
    public interface IProfileRepository : IRepository<Profile>
    {
        public Task UpdateStatusAsync(Guid id, WorkStatus status, CancellationToken cancellationToken = default);

        public Task UpdateRoleAsync(string email, Role role, CancellationToken cancellationToken = default);

        /// <returns>queryable items from the database</returns>
        public Task<IEnumerable<Profile>> GetItemsByRoleAsync(Role role, int pageSize, int pageNumber, IFiltrator<Profile> filtrator, CancellationToken cancellationToken = default);
        public Task<Profile?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}
