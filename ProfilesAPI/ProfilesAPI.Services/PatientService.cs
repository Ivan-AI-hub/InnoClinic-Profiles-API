using AutoMapper;
using FluentValidation;
using ProfilesAPI.Domain;
using ProfilesAPI.Domain.Exceptions;
using ProfilesAPI.Domain.Interfaces;
using ProfilesAPI.Services.Abstraction;
using ProfilesAPI.Services.Abstraction.AggregatesModels.PatientAggregate;

namespace ProfilesAPI.Services
{
    public class PatientService : BaseService, IPatientService
    {
        private IMapper _mapper;
        private IValidator<CreatePatientModel> _createPatientValidator;

        public PatientService(IRepositoryManager repositoryManager, IBlobService blobService, IMapper mapper,
            IValidator<CreatePatientModel> createPatientValidator) : base(blobService, repositoryManager)
        {
            _mapper = mapper;
            _createPatientValidator = createPatientValidator;
            _blobService = blobService;
        }

        public async Task<PatientDTO> CreatePatientAsync(CreatePatientModel model, CancellationToken cancellationToken = default)
        {
            await ValidateBlobFileName(model.Photo, cancellationToken);
            await ValidateModel(model, _createPatientValidator, cancellationToken);
            await ValidateEmail(model.Email);

            var patient = _mapper.Map<Patient>(model);

            await _repositoryManager.PatientRepository.CreateAsync(patient);
            await _repositoryManager.SaveChangesAsync(cancellationToken);

            if (model.Photo != null)
                await _blobService.UploadAsync(model.Photo);

            return await GetPatientDTOWithPhotoAsync(patient, cancellationToken);
        }

        public async Task<PatientDTO> GetPatientAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var patient = await _repositoryManager.PatientRepository.GetItemAsync(id, cancellationToken);
            if (patient == null)
                throw new PatientNotFoundException(id);

            return await GetPatientDTOWithPhotoAsync(patient, cancellationToken);
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
