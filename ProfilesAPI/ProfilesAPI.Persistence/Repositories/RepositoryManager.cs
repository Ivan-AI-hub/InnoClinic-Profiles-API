using ProfilesAPI.Domain.Interfaces;

namespace ProfilesAPI.Persistence.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ProfilesContext _context;
        private Lazy<IPatientRepository> _patientRepository;
        private Lazy<IReceptionistRepository> _receptionistRepository;
        private Lazy<IDoctorRepository> _doctorRepository;
        private Lazy<IHumanInfoRepository> _humanInfoRepository;

        public RepositoryManager(ProfilesContext context)
        {
            _context = context;
            _patientRepository = new Lazy<IPatientRepository>(() => new PatientRepository(context));
            _receptionistRepository = new Lazy<IReceptionistRepository>(() => new ReceptionistRepository(context));
            _doctorRepository = new Lazy<IDoctorRepository>(() => new DoctorRepository(context));
            _humanInfoRepository = new Lazy<IHumanInfoRepository>(() => new HumanInfoRepository(context));
        }

        public IPatientRepository PatientRepository => _patientRepository.Value;
        public IReceptionistRepository ReceptionistRepository => _receptionistRepository.Value;

        public IDoctorRepository DoctorRepository => _doctorRepository.Value;

        public IHumanInfoRepository HumanInfoRepository => _humanInfoRepository.Value;

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
