using ProfilesAPI.Domain.Interfaces;

namespace ProfilesAPI.Persistence.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IPatientRepository> _patientRepository;
        private readonly Lazy<IReceptionistRepository> _receptionistRepository;
        private readonly Lazy<IDoctorRepository> _doctorRepository;
        private readonly Lazy<IHumanInfoRepository> _humanInfoRepository;

        public RepositoryManager(ProfilesContext context)
        {
            _patientRepository = new Lazy<IPatientRepository>(() => new PatientRepository(context));
            _receptionistRepository = new Lazy<IReceptionistRepository>(() => new ReceptionistRepository(context));
            _doctorRepository = new Lazy<IDoctorRepository>(() => new DoctorRepository(context));
            _humanInfoRepository = new Lazy<IHumanInfoRepository>(() => new HumanInfoRepository(context));
        }

        public IPatientRepository PatientRepository => _patientRepository.Value;
        public IReceptionistRepository ReceptionistRepository => _receptionistRepository.Value;
        public IDoctorRepository DoctorRepository => _doctorRepository.Value;
        public IHumanInfoRepository HumanInfoRepository => _humanInfoRepository.Value;
    }
}
