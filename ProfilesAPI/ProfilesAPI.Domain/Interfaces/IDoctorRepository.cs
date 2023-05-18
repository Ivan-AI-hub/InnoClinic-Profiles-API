namespace ProfilesAPI.Domain.Interfaces
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        public Task UpdateStatusAsync(Guid id, WorkStatus status, CancellationToken cancellationToken = default);
    }
}
