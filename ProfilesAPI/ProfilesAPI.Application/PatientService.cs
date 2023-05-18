using AutoMapper;
using FluentValidation;
using ProfilesAPI.Application.Abstraction;
using ProfilesAPI.Application.Abstraction.AggregatesModels;
using ProfilesAPI.Application.Abstraction.AggregatesModels.PatientAggregate;
using ProfilesAPI.Application.Abstraction.QueryableManipulation;
using ProfilesAPI.Application.Filtrators;
using ProfilesAPI.Domain;
using ProfilesAPI.Domain.Exceptions;
using ProfilesAPI.Domain.Interfaces;

namespace ProfilesAPI.Application
{
    public class PatientService : BaseService, IPatientService
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
            await ValidateEmailAsync(model.Info.Email, cancellationToken);

            var patient = _mapper.Map<Patient>(model);

            await _repositoryManager.PatientRepository.CreateAsync(patient);
            await _repositoryManager.SaveChangesAsync(cancellationToken);

            if (model.Info.Photo != null)
            {
                await _blobService.UploadAsync(model.Info.Photo);
            }

            return await GetPatientDTOWithPhotoAsync(patient, cancellationToken);
        }

        public async Task DeletePatientAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var patient = await _repositoryManager.PatientRepository.GetItemAsync(id, false, cancellationToken);
            if (patient == null)
            {
                throw new PatientNotFoundException(id);
            }

            await _repositoryManager.PatientRepository.DeleteAsync(id);
            await _repositoryManager.SaveChangesAsync(cancellationToken);
            if (patient.Info.Photo != null)
            {
                await _blobService.DeleteAsync(patient.Info.Photo.Name);
            }
        }

        public async Task EditPatientAsync(Guid id, EditPatientModel model, CancellationToken cancellationToken = default)
        {
            await ValidateBlobFileName(model.Photo, cancellationToken);
            await ValidateModel(model, _editPatientValidator, cancellationToken);

            var oldPatient = await _repositoryManager.PatientRepository.GetItemAsync(id, false, cancellationToken);
            if (oldPatient == null)
            {
                throw new PatientNotFoundException(id);
            }

            var patient = _mapper.Map<Patient>(model);
            await _repositoryManager.PatientRepository.UpdateAsync(id, patient);
            await _repositoryManager.SaveChangesAsync(cancellationToken);

            if (oldPatient.Info.Photo != null)
            {
                await _blobService.DeleteAsync(oldPatient.Info.Photo.Name);
            }

            if (model.Photo != null)
            {
                await _blobService.UploadAsync(model.Photo);
            }
        }

        public async Task<PatientDTO> GetPatientAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var patient = await _repositoryManager.PatientRepository.GetItemAsync(id, false, cancellationToken);
            if (patient == null)
            {
                throw new PatientNotFoundException(id);
            }

            return await GetPatientDTOWithPhotoAsync(patient, cancellationToken);
        }

        public IEnumerable<PatientDTO> GetPatients(Page page, PatientFiltrationModel filtrationModel)
        {
            return GetFiltratedPatients(page, filtrationModel).ToList().Select(x => GetPatientDTOWithPhotoAsync(x).Result);
        }

        public IEnumerable<PatientDTO> GetPatientsInfo(Page page, PatientFiltrationModel filtrationModel)
        {
            var patients = GetFiltratedPatients(page, filtrationModel);

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

        private IQueryable<Patient> GetFiltratedPatients(Page page, PatientFiltrationModel filtrationModel)
        {
            var patients = _repositoryManager.PatientRepository.GetItems(false);
            var filtrator = _mapper.Map<IFiltrator<Patient>>(filtrationModel);
            patients = filtrator.Filtrate(patients);
            patients = PageSeparator.GetPage(patients, page);
            return patients;
        }
    }
}
