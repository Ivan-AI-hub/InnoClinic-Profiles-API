using AutoMapper;
using FluentValidation;
using ProfilesAPI.Application.Abstraction;
using ProfilesAPI.Application.Abstraction.AggregatesModels.BlobAggregate;
using ProfilesAPI.Application.Abstraction.AggregatesModels.PatientAggregate;
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

            if (model.Info.Photo != null)
            {
                await _blobService.UploadAsync(model.Info.Photo);
            }

            return await GetPatientDTOWithPhotoAsync(patient, cancellationToken);
        }

        public async Task DeletePatientAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var patient = await _repositoryManager.PatientRepository.GetItemAsync(id, cancellationToken);
            if (patient == null)
            {
                throw new PatientNotFoundException(id);
            }

            await _repositoryManager.PatientRepository.DeleteAsync(id);
            if (patient.Info.Photo != null)
            {
                await _blobService.DeleteAsync(patient.Info.Photo.Name);
            }
        }

        public async Task EditPatientAsync(Guid id, EditPatientModel model, CancellationToken cancellationToken = default)
        {
            await ValidateBlobFileName(model.Photo, cancellationToken);
            await ValidateModel(model, _editPatientValidator, cancellationToken);

            var oldPatient = await _repositoryManager.PatientRepository.GetItemAsync(id, cancellationToken);
            if (oldPatient == null)
            {
                throw new PatientNotFoundException(id);
            }

            var patient = _mapper.Map<Patient>(model);
            await _repositoryManager.PatientRepository.UpdateAsync(id, patient);

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
            var patient = await _repositoryManager.PatientRepository.GetItemAsync(id, cancellationToken);
            if (patient == null)
            {
                throw new PatientNotFoundException(id);
            }

            return await GetPatientDTOWithPhotoAsync(patient, cancellationToken);
        }

        public IEnumerable<PatientDTO> GetPatients(Page page, PatientFiltrationModel filtrationModel)
        {
            var filtrator = _mapper.Map<IFiltrator<Patient>>(filtrationModel);
            var patients = _repositoryManager.PatientRepository.GetItems(page.Size, page.Number, filtrator);
            return patients.ToList().Select(x => GetPatientDTOWithPhotoAsync(x).Result);
        }

        public IEnumerable<PatientDTO> GetPatientsInfo(Page page, PatientFiltrationModel filtrationModel)
        {
            var filtrator = _mapper.Map<IFiltrator<Patient>>(filtrationModel);
            var patients = _repositoryManager.PatientRepository.GetItems(page.Size, page.Number, filtrator);

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
