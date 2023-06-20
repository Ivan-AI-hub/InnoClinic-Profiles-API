using FluentValidation;
using MassTransit;
using ProfilesAPI.Application.Abstraction;
using ProfilesAPI.Application.Abstraction.AggregatesModels.DoctorAggregate;
using ProfilesAPI.Domain;
using ProfilesAPI.Domain.Interfaces;
using SharedEvents.Models;

namespace ProfilesAPI.Application
{
    public class DoctorService : BaseService, IDoctorService
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly AutoMapper.IMapper _mapper;
        private readonly IValidator<CreateDoctorModel> _createDoctorValidator;
        private readonly IValidator<EditDoctorModel> _editDoctorModel;

        public DoctorService(IPublishEndpoint publishEndpoint, IRepositoryManager repositoryManager, AutoMapper.IMapper mapper,
            IValidator<CreateDoctorModel> createDoctorValidator, IValidator<EditDoctorModel> editDoctorModel) : base(repositoryManager)
        {
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
            _createDoctorValidator = createDoctorValidator;
            _editDoctorModel = editDoctorModel;
        }
        public async Task<DoctorDTO> CreateDoctorAsync(CreateDoctorModel model, CancellationToken cancellationToken = default)
        {
            await ValidateModel(model, _createDoctorValidator, cancellationToken);
            await ValidateEmailAsync(model.Info.Email, cancellationToken);

            var doctor = _mapper.Map<Profile>(model);
            doctor.Role = Role.Doctor;

            await _repositoryManager.ProfileRepository.CreateAsync(doctor, cancellationToken);
            await _publishEndpoint.Publish(new DoctorCreated(doctor.Id, doctor.Info.Email, doctor.Info.FirstName, doctor.Info.MiddleName,
                                doctor.Info.LastName, doctor.Office.Id, model.Specialization, model.CareerStartYear, doctor.Info.BirthDay), cancellationToken);
            return _mapper.Map<DoctorDTO>(doctor);
        }

        public async Task EditDoctorAsync(Guid id, EditDoctorModel model, CancellationToken cancellationToken = default)
        {
            await ValidateModel(model, _editDoctorModel, cancellationToken);
            await ValidateRoleAndExistingAsync(id, Role.Doctor);

            var doctor = _mapper.Map<Profile>(model);
            await _repositoryManager.ProfileRepository.UpdateAsync(id, doctor, cancellationToken);
            await _publishEndpoint.Publish(new DoctorUpdated(id, doctor.Info.FirstName, doctor.Info.MiddleName,
                                doctor.Info.LastName, doctor.Office.Id, model.Specialization, model.CareerStartYear, doctor.Info.BirthDay), cancellationToken);
        }

        public async Task EditDoctorStatusAsync(Guid id, WorkStatusDTO workStatus, CancellationToken cancellationToken = default)
        {
            await ValidateRoleAndExistingAsync(id, Role.Doctor);
            await _repositoryManager.ProfileRepository.UpdateStatusAsync(id, _mapper.Map<WorkStatus>(workStatus), cancellationToken);
        }

        public async Task<DoctorDTO> GetDoctorAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await ValidateRoleAndExistingAsync(id, Role.Doctor);
            var doctor = await _repositoryManager.ProfileRepository.GetItemAsync(id, cancellationToken);

            return _mapper.Map<DoctorDTO>(doctor);
        }

        public async Task<DoctorDTO> GetDoctorAsync(string email, CancellationToken cancellationToken = default)
        {
            await ValidateRoleAndExistingAsync(email, Role.Doctor);
            var doctor = await _repositoryManager.ProfileRepository.GetByEmailAsync(email, cancellationToken);

            return _mapper.Map<DoctorDTO>(doctor);
        }

        public async Task<IEnumerable<DoctorDTO>> GetDoctorsAsync(Page page, DoctorFiltrationModel filtrationModel, CancellationToken cancellationToken = default)
        {
            var filtrator = _mapper.Map<IFiltrator<Profile>>(filtrationModel);
            var doctors = await _repositoryManager.ProfileRepository.GetItemsByRoleAsync(Role.Doctor,page.Size, page.Number, filtrator, cancellationToken);
            return _mapper.Map<IEnumerable<DoctorDTO>>(doctors);
        }
    }
}
