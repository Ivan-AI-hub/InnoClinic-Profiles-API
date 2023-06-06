using AutoMapper;
using FluentValidation;
using MassTransit;
using ProfilesAPI.Application.Abstraction;
using ProfilesAPI.Application.Abstraction.AggregatesModels.DoctorAggregate;
using ProfilesAPI.Domain;
using ProfilesAPI.Domain.Exceptions;
using ProfilesAPI.Domain.Interfaces;
using SharedEvents.Models;

namespace ProfilesAPI.Application
{
    public class DoctorService : BaseService, IDoctorService
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateDoctorModel> _createDoctorValidator;
        private readonly IValidator<EditDoctorModel> _editDoctorModel;

        public DoctorService(IPublishEndpoint publishEndpoint, IRepositoryManager repositoryManager, IMapper mapper,
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

            var doctor = _mapper.Map<Doctor>(model);

            await _repositoryManager.DoctorRepository.CreateAsync(doctor);
            await _publishEndpoint.Publish(new DoctorCreated(doctor.Id, doctor.Info.Email, doctor.Info.FirstName, doctor.Info.MiddleName,
                                doctor.Info.LastName, doctor.Office.Id, doctor.Specialization, doctor.CareerStartYear, doctor.Info.BirthDay));
            return _mapper.Map<DoctorDTO>(doctor);
        }

        public async Task EditDoctorAsync(Guid id, EditDoctorModel model, CancellationToken cancellationToken = default)
        {
            await ValidateModel(model, _editDoctorModel, cancellationToken);

            var doctor = _mapper.Map<Doctor>(model);
            await _repositoryManager.DoctorRepository.UpdateAsync(id, doctor);
            await _publishEndpoint.Publish(new DoctorUpdated(id, doctor.Info.FirstName, doctor.Info.MiddleName,
                                doctor.Info.LastName, doctor.Office.Id, doctor.Specialization, doctor.CareerStartYear, doctor.Info.BirthDay));
        }

        public async Task EditDoctorStatusAsync(Guid id, WorkStatusDTO workStatus, CancellationToken cancellationToken = default)
        {
            await _repositoryManager.DoctorRepository.UpdateStatusAsync(id, _mapper.Map<WorkStatus>(workStatus));
        }

        public async Task<DoctorDTO> GetDoctorAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var doctor = await _repositoryManager.DoctorRepository.GetItemAsync(id, cancellationToken);
            if (doctor == null)
            {
                throw new DoctorNotFoundException(id);
            }

            return _mapper.Map<DoctorDTO>(doctor);
        }

        public IEnumerable<DoctorDTO> GetDoctors(Page page, DoctorFiltrationModel filtrationModel)
        {
            var filtrator = _mapper.Map<IFiltrator<Doctor>>(filtrationModel);
            var doctors = _repositoryManager.DoctorRepository.GetItems(page.Size, page.Number, filtrator);
            return _mapper.Map<IEnumerable<DoctorDTO>>(doctors);
        }
    }
}
