
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
        private readonly AutoMapper.IMapper _mapper;
        private readonly IValidator<CreatePatientModel> _createPatientValidator;
        private readonly IValidator<EditPatientModel> _editPatientValidator;

        public PatientService(IRepositoryManager repositoryManager, AutoMapper.IMapper mapper,
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

            var patient = _mapper.Map<Profile>(model);
            patient.Role = Role.Patient;

            await _repositoryManager.ProfileRepository.CreateAsync(patient, cancellationToken);
            await _publishEndpoint.Publish(new PatientCreated(patient.Id, patient.Info.Email, patient.Info.FirstName,
                                            patient.Info.MiddleName, patient.Info.LastName, patient.PhoneNumber, patient.Info.BirthDay), cancellationToken);

            return _mapper.Map<PatientDTO>(patient);
        }

        public async Task DeletePatientAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await ValidateRoleAndExistingAsync(id, Role.Patient);

            await _repositoryManager.ProfileRepository.DeleteAsync(id, cancellationToken);
            await _publishEndpoint.Publish(new PatientDeleted(id));
        }

        public async Task EditPatientAsync(Guid id, EditPatientModel model, CancellationToken cancellationToken = default)
        {
            await ValidateRoleAndExistingAsync(id, Role.Patient);
            await ValidateModel(model, _editPatientValidator, cancellationToken);
            await ValidatePhoneNumber(model.PhoneNumber, id, cancellationToken);

            var patient = _mapper.Map<Profile>(model);
            await _repositoryManager.ProfileRepository.UpdateAsync(id, patient, cancellationToken);
            await _publishEndpoint.Publish(new PatientUpdated(id, patient.Info.FirstName, patient.Info.MiddleName,
                                                            patient.Info.LastName, patient.PhoneNumber, patient.Info.BirthDay), cancellationToken);
        }

        public async Task<PatientDTO> GetPatientAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await ValidateRoleAndExistingAsync(id, Role.Patient);
            var patient = await _repositoryManager.ProfileRepository.GetItemAsync(id, cancellationToken);

            return _mapper.Map<PatientDTO>(patient);
        }

        public async Task<PatientDTO> GetPatientAsync(string email, CancellationToken cancellationToken = default)
        {
            await ValidateRoleAndExistingAsync(email, Role.Patient);
            var patient = await _repositoryManager.ProfileRepository.GetByEmailAsync(email, cancellationToken);

            return _mapper.Map<PatientDTO>(patient);
        }

        public async Task<IEnumerable<PatientDTO>> GetPatientsAsync(Page page, PatientFiltrationModel filtrationModel, CancellationToken cancellationToken = default)
        {
            var filtrator = _mapper.Map<IFiltrator<Profile>>(filtrationModel);
            var patients = await _repositoryManager.ProfileRepository.GetItemsByRoleAsync(Role.Patient, page.Size, page.Number, filtrator, cancellationToken);
            return _mapper.Map<IEnumerable<PatientDTO>>(patients);
        }

        private async Task ValidatePhoneNumber(string phoneNumber, Guid excludeId = default, CancellationToken cancellationToken = default)
        {
            var isPhoneNumberInvalid = await _repositoryManager.ProfileRepository.IsItemExistAsync(x => x.PhoneNumber == phoneNumber && x.Id != excludeId, cancellationToken);
            if (isPhoneNumberInvalid)
            {
                throw new ProfileWithSameEmailExistException(phoneNumber);
            }
        }
    }
}
