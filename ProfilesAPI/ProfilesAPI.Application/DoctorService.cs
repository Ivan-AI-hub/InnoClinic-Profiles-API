using AutoMapper;
using FluentValidation;
using ProfilesAPI.Application.Abstraction;
using ProfilesAPI.Application.Abstraction.AggregatesModels.DoctorAggregate;
using ProfilesAPI.Domain;
using ProfilesAPI.Domain.Exceptions;
using ProfilesAPI.Domain.Interfaces;

namespace ProfilesAPI.Application
{
    public class DoctorService : BaseService, IDoctorService
    {
        private IMapper _mapper;
        private IValidator<CreateDoctorModel> _createDoctorValidator;
        private IValidator<EditDoctorModel> _editDoctorModel;

        public DoctorService(IRepositoryManager repositoryManager, IMapper mapper,
            IValidator<CreateDoctorModel> createDoctorValidator, IValidator<EditDoctorModel> editDoctorModel) : base(repositoryManager)
        {
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

            return _mapper.Map<DoctorDTO>(doctor);
        }

        public async Task EditDoctorAsync(Guid id, EditDoctorModel model, CancellationToken cancellationToken = default)
        {
            await ValidateModel(model, _editDoctorModel, cancellationToken);

            var doctor = _mapper.Map<Doctor>(model);
            await _repositoryManager.DoctorRepository.UpdateAsync(id, doctor);
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
