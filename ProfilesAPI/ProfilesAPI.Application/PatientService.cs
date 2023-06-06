using AutoMapper;
using FluentValidation;
using MassTransit;
using ProfilesAPI.Application.Abstraction;
using ProfilesAPI.Application.Abstraction.AggregatesModels.PatientAggregate;
using ProfilesAPI.Domain;
using ProfilesAPI.Domain.Exceptions;
using ProfilesAPI.Domain.Interfaces;
using SharedEvents.Models;

namespace ProfilesAPI.Application
{
    public class PatientService : BaseService, IPatientService
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;
        private readonly IValidator<CreatePatientModel> _createPatientValidator;
        private readonly IValidator<EditPatientModel> _editPatientValidator;

        public PatientService(IRepositoryManager repositoryManager, IMapper mapper,
            IValidator<CreatePatientModel> createPatientValidator, IValidator<EditPatientModel> editPatientValidator,
            IPublishEndpoint publishEndpoint) : base(repositoryManager)
        {
            _mapper = mapper;
            _createPatientValidator = createPatientValidator;
            _editPatientValidator = editPatientValidator;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<PatientDTO> CreatePatientAsync(CreatePatientModel model, CancellationToken cancellationToken = default)
        {
            await ValidateModel(model, _createPatientValidator, cancellationToken);
            await ValidateEmailAsync(model.Info.Email, cancellationToken);
            await ValidatePhoneNumber(model.PhoneNumber, default, cancellationToken);

            var patient = _mapper.Map<Patient>(model);

            await _repositoryManager.PatientRepository.CreateAsync(patient);
            await _publishEndpoint.Publish(new PatientCreated(patient.Id, patient.Info.Email));

            return _mapper.Map<PatientDTO>(patient);
        }

        public async Task DeletePatientAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var patient = await _repositoryManager.PatientRepository.GetItemAsync(id, cancellationToken);
            if (patient == null)
            {
                throw new PatientNotFoundException(id);
            }

            await _repositoryManager.PatientRepository.DeleteAsync(id);
        }

        public async Task EditPatientAsync(Guid id, EditPatientModel model, CancellationToken cancellationToken = default)
        {
            await ValidateModel(model, _editPatientValidator, cancellationToken);
            await ValidatePhoneNumber(model.PhoneNumber, id, cancellationToken);

            var patient = _mapper.Map<Patient>(model);
            await _repositoryManager.PatientRepository.UpdateAsync(id, patient);
        }

        public async Task<PatientDTO> GetPatientAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var patient = await _repositoryManager.PatientRepository.GetItemAsync(id, cancellationToken);
            if (patient == null)
            {
                throw new PatientNotFoundException(id);
            }

            return _mapper.Map<PatientDTO>(patient);
        }

        public IEnumerable<PatientDTO> GetPatients(Page page, PatientFiltrationModel filtrationModel)
        {
            var filtrator = _mapper.Map<IFiltrator<Patient>>(filtrationModel);
            var patients = _repositoryManager.PatientRepository.GetItems(page.Size, page.Number, filtrator);
            return _mapper.Map<IEnumerable<PatientDTO>>(patients);
        }

        private async Task ValidatePhoneNumber(string phoneNumber, Guid excludeId = default, CancellationToken cancellationToken = default)
        {
            var isPhoneNumberInvalid = await _repositoryManager.PatientRepository.IsItemExistAsync(x => x.PhoneNumber == phoneNumber && x.Id != excludeId, cancellationToken);
            if (isPhoneNumberInvalid)
            {
                throw new ProfileWithSameEmailExistException(phoneNumber);
            }
        }
    }
}
