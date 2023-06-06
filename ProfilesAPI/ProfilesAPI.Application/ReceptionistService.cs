using AutoMapper;
using FluentValidation;
using ProfilesAPI.Application.Abstraction;
using ProfilesAPI.Application.Abstraction.AggregatesModels.ReceptionistAggregate;
using ProfilesAPI.Domain;
using ProfilesAPI.Domain.Exceptions;
using ProfilesAPI.Domain.Interfaces;

namespace ProfilesAPI.Application
{
    public class ReceptionistService : BaseService, IReceptionistService
    {
        private readonly IMapper _mapper;
        private readonly IValidator<CreateReceptionistModel> _createReceptionistValidator;
        private readonly IValidator<EditReceptionistModel> _editReceptionistModel;

        public ReceptionistService(IRepositoryManager repositoryManager, IMapper mapper,
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

            var receptionist = _mapper.Map<Receptionist>(model);

            await _repositoryManager.ReceptionistRepository.CreateAsync(receptionist);

            return _mapper.Map<ReceptionistDTO>(receptionist);
        }

        public async Task DeleteReceptionistAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var receptionist = await _repositoryManager.ReceptionistRepository.GetItemAsync(id, cancellationToken);
            if (receptionist == null)
            {
                throw new ReceptionistNotFoundException(id);
            }

            await _repositoryManager.ReceptionistRepository.DeleteAsync(id);
        }

        public async Task EditReceptionistAsync(Guid id, EditReceptionistModel model, CancellationToken cancellationToken = default)
        {
            await ValidateModel(model, _editReceptionistModel, cancellationToken);

            var receptionist = _mapper.Map<Receptionist>(model);

            await _repositoryManager.ReceptionistRepository.UpdateAsync(id, receptionist);
        }

        public async Task<ReceptionistDTO> GetReceptionistAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var receptionist = await _repositoryManager.ReceptionistRepository.GetItemAsync(id, cancellationToken);
            if (receptionist == null)
            {
                throw new ReceptionistNotFoundException(id);
            }

            return _mapper.Map<ReceptionistDTO>(receptionist);
        }

        public IEnumerable<ReceptionistDTO> GetReceptionists(Page page, ReceptionistFiltrationModel filtrationModel)
        {
            var filtrator = _mapper.Map<IFiltrator<Receptionist>>(filtrationModel);
            var receptionists = _repositoryManager.ReceptionistRepository.GetItems(page.Size, page.Number, filtrator);
            return _mapper.Map<IEnumerable<ReceptionistDTO>>(receptionists);
        }
    }
}
