namespace ProfilesAPI.Domain.Interfaces
{
    public interface IRepositoryManager
    {
        public IPatientRepository PatientRepository { get; }
        public IDoctorRepository DoctorRepository { get; }
        public IHumanInfoRepository HumanInfoRepository { get; }
        /// <summary>
        /// Saves all changes made in repositories.
        /// </summary>
        public Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
