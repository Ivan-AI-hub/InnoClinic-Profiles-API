namespace ProfilesAPI.Domain.Interfaces
{
    public interface IRepositoryManager
    {
        public IPatientRepository PatientRepository { get; }
        public IDoctorRepository DoctorRepository { get; }
        public IHumanInfoRepository HumanInfoRepository { get; }
        public IReceptionistRepository ReceptionistRepository { get; }
    }
}
