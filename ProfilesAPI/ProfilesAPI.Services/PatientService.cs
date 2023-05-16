using AutoMapper;
using FluentValidation;
using ProfilesAPI.Domain;
using ProfilesAPI.Domain.Exceptions;
using ProfilesAPI.Domain.Interfaces;
using ProfilesAPI.Services.Abstraction;
using ProfilesAPI.Services.Abstraction.AggregatesModels;
using ProfilesAPI.Services.Abstraction.AggregatesModels.PatientAggregate;
using ProfilesAPI.Services.Abstraction.QueryableManipulation;

namespace ProfilesAPI.Services
{
    public class PatientService : BaseService, IPatientService<Patient>
    {
        private IMapper _mapper;
        private IValidator<CreatePatientModel> _createPatientValidator;
        private IValidator<EditPatientModel> _editPatientValidator;

        public PatientService(IRepositoryManager repositoryManager, IBlobService blobService, IMapper mapper,
            IValidator<CreatePatientModel> createPatientValidator, IValidator<EditPatientModel> editPatientValidator) : base(blobService, repositoryManager)
        {
            _mapper = mapper;
            _createPatientValidator = createPatientValidator;
            _editPatientValidator = editPatientValidator;
            _blobService = blobService;
        }

        public async Task<PatientDTO> CreatePatientAsync(CreatePatientModel model, CancellationToken cancellationToken = default)
        {
            await ValidateBlobFileName(model.Info.Photo, cancellationToken);
            await ValidateModel(model, _createPatientValidator, cancellationToken);
            await ValidateEmail(model.Info.Email);

            var patient = _mapper.Map<Patient>(model);

            await _repositoryManager.PatientRepository.CreateAsync(patient);
            await _repositoryManager.SaveChangesAsync(cancellationToken);

            if (model.Info.Photo != null)
                await _blobService.UploadAsync(model.Info.Photo);

            return await GetPatientDTOWithPhotoAsync(patient, cancellationToken);
        }

        public async Task DeletePatientAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var patient = await _repositoryManager.PatientRepository.GetItemAsync(id, false, cancellationToken);
            if (patient == null)
                throw new PatientNotFoundException(id);

            await _repositoryManager.PatientRepository.DeleteAsync(id);

            if (patient.Info.Photo != null)
                await _blobService.DeleteAsync(patient.Info.Photo.Name);
        }

        public async Task<PatientDTO> EditPatientAsync(Guid id, EditPatientModel model, CancellationToken cancellationToken = default)
        {
            await ValidateBlobFileName(model.Photo, cancellationToken);
            await ValidateModel(model, _editPatientValidator, cancellationToken);

            var oldPatient = await _repositoryManager.PatientRepository.GetItemAsync(id, false, cancellationToken);
            if (oldPatient == null)
                throw new PatientNotFoundException(id);

            var createModel = _mapper.Map<CreatePatientModel>(model);
            createModel.Info.Email = oldPatient.Info.Email;

            var patient = _mapper.Map<Patient>(createModel);
            await _repositoryManager.PatientRepository.CreateAsync(patient);
            await _repositoryManager.SaveChangesAsync(cancellationToken);

            if (oldPatient.Info.Photo != null)
                await _blobService.DeleteAsync(oldPatient.Info.Photo.Name);

            if (model.Photo != null)
                await _blobService.UploadAsync(model.Photo);

            return await GetPatientDTOWithPhotoAsync(patient);
        }

        public async Task<PatientDTO> GetPatientAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var patient = await _repositoryManager.PatientRepository.GetItemAsync(id, false, cancellationToken);
            if (patient == null)
                throw new PatientNotFoundException(id);

            return await GetPatientDTOWithPhotoAsync(patient, cancellationToken);
        }

        public IEnumerable<PatientDTO> GetPatients(Page page, IFiltrator<Patient> filtrator)
        {
            var patients = _repositoryManager.PatientRepository.GetItems(false);
            patients = filtrator.Filtrate(patients);
            patients = PageSeparator.GetPage(patients, page);

            return patients.ToList().Select(x => GetPatientDTOWithPhotoAsync(x).Result);
        }

        public IEnumerable<PatientDTO> GetPatientsInfo(Page page, IFiltrator<Patient> filtrator)
        {
            var patients = _repositoryManager.PatientRepository.GetItems(false);
            patients = filtrator.Filtrate(patients);
            patients = PageSeparator.GetPage(patients, page);

            return _mapper.Map<IEnumerable<PatientDTO>>(patients);
        }

        private async Task<PatientDTO> GetPatientDTOWithPhotoAsync(Patient patientData, CancellationToken cancellationToken = default)
        {
            var patient = _mapper.Map<PatientDTO>(patientData);

            if (patientData.Info.Photo != null)
            {
                patient.Info.Photo = await _blobService.DownloadAsync(patientData.Info.Photo.Name, cancellationToken);
            }

            return patient;
        }
    }
}
