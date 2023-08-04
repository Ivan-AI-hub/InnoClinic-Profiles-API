using FluentValidation;
using ProfilesAPI.Application.Abstraction;
using ProfilesAPI.Application.Abstraction.AggregatesModels.ReceptionistAggregate;
using ProfilesAPI.Domain;
using ProfilesAPI.Domain.Interfaces;

namespace ProfilesAPI.Application
{
    public class ReceptionistService : BaseService, IReceptionistService
    {
        private readonly AutoMapper.IMapper _mapper;
        private readonly IValidator<CreateReceptionistModel> _createReceptionistValidator;
        private readonly IValidator<EditReceptionistModel> _editReceptionistModel;

        public ReceptionistService(IRepositoryManager repositoryManager, AutoMapper.IMapper mapper,
            IValidator<CreateReceptionistModel> createReceptionistValidator,
            IValidator<EditReceptionistModel> editReceptionistModel) : base(repositoryManager)
        {
            _mapper = mapper;
            _createReceptionistValidator = createReceptionistValidator;
            _editReceptionistModel = editReceptionistModel;
        }

        public async Task<ReceptionistDTO> CreateReceptionistAsync(CreateReceptionistModel model, CancellationToken cancellationToken = default)
        {
            await ValidateModel(model, _createReceptionistValidator, cancellationToken);
            await ValidateEmailAsync(model.Info.Email, cancellationToken);

            var receptionist = _mapper.Map<Profile>(model);
            receptionist.Role = Role.Admin;

            await _repositoryManager.ProfileRepository.CreateAsync(receptionist, cancellationToken);

            return _mapper.Map<ReceptionistDTO>(receptionist);
        }

        public async Task DeleteReceptionistAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await ValidateRoleAndExistingAsync(id, Role.Admin);

            await _repositoryManager.ProfileRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task EditReceptionistAsync(Guid id, EditReceptionistModel model, CancellationToken cancellationToken = default)
        {
            await ValidateModel(model, _editReceptionistModel, cancellationToken);
            await ValidateRoleAndExistingAsync(id, Role.Admin);

            var receptionist = _mapper.Map<Profile>(model);

            await _repositoryManager.ProfileRepository.UpdateAsync(id, receptionist, cancellationToken);
        }

        public async Task<ReceptionistDTO> GetReceptionistAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await ValidateRoleAndExistingAsync(id, Role.Admin);
            var receptionist = await _repositoryManager.ProfileRepository.GetItemAsync(id, cancellationToken);

            return _mapper.Map<ReceptionistDTO>(receptionist);
        }

        public async Task<ReceptionistDTO> GetReceptionistAsync(string email, CancellationToken cancellationToken = default)
        {
            await ValidateRoleAndExistingAsync(email, Role.Admin);
            var receptionist = await _repositoryManager.ProfileRepository.GetByEmailAsync(email, cancellationToken);

            return _mapper.Map<ReceptionistDTO>(receptionist);
        }

        public async Task<IEnumerable<ReceptionistDTO>> GetReceptionistsAsync(Page page, ReceptionistFiltrationModel filtrationModel, CancellationToken cancellationToken = default)
        {
            var filtrator = _mapper.Map<IFiltrator<Profile>>(filtrationModel);
            var receptionists = await _repositoryManager.ProfileRepository.GetItemsByRoleAsync(Role.Admin, page.Size, page.Number, filtrator, cancellationToken);
            return _mapper.Map<IEnumerable<ReceptionistDTO>>(receptionists);
        }
    }
}
