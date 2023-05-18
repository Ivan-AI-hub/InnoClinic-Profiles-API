using AutoMapper;
using FluentValidation;
using ProfilesAPI.Application.Abstraction;
using ProfilesAPI.Application.Abstraction.AggregatesModels;
using ProfilesAPI.Application.Abstraction.AggregatesModels.ReceptionistAggregate;
using ProfilesAPI.Application.Abstraction.QueryableManipulation;
using ProfilesAPI.Application.Filtrators;
using ProfilesAPI.Domain;
using ProfilesAPI.Domain.Exceptions;
using ProfilesAPI.Domain.Interfaces;

namespace ProfilesAPI.Application
{
    public class ReceptionistService : BaseService, IReceptionistService
    {
        private IMapper _mapper;
        private IValidator<CreateReceptionistModel> _createReceptionistValidator;
        private IValidator<EditReceptionistModel> _editReceptionistModel;

        public ReceptionistService(IRepositoryManager repositoryManager, IBlobService blobService, IMapper mapper,
            IValidator<CreateReceptionistModel> createReceptionistValidator,
            IValidator<EditReceptionistModel> editReceptionistModel) : base(blobService, repositoryManager)
        {
            _mapper = mapper;
            _createReceptionistValidator = createReceptionistValidator;
            _editReceptionistModel = editReceptionistModel;
        }

        public async Task<ReceptionistDTO> CreateReceptionistAsync(CreateReceptionistModel model, CancellationToken cancellationToken = default)
        {
            await ValidateBlobFileName(model.Info.Photo, cancellationToken);
            await ValidateModel(model, _createReceptionistValidator, cancellationToken);
            await ValidateEmailAsync(model.Info.Email, cancellationToken);

            var receptionist = _mapper.Map<Receptionist>(model);

            await _repositoryManager.ReceptionistRepository.CreateAsync(receptionist);

            if (model.Info.Photo != null)
            {
                await _blobService.UploadAsync(model.Info.Photo);
            }

            return await GetReceptionistDTOWithPhotoAsync(receptionist, cancellationToken);
        }

        public async Task DeleteReceptionistAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var receptionist = await _repositoryManager.ReceptionistRepository.GetItemAsync(id, cancellationToken);
            if (receptionist == null)
            {
                throw new ReceptionistNotFoundException(id);
            }

            await _repositoryManager.ReceptionistRepository.DeleteAsync(id);
            if (receptionist.Info.Photo != null)
            {
                await _blobService.DeleteAsync(receptionist.Info.Photo.Name);
            }
        }

        public async Task EditReceptionistAsync(Guid id, EditReceptionistModel model, CancellationToken cancellationToken = default)
        {
            await ValidateBlobFileName(model.Photo, cancellationToken);
            await ValidateModel(model, _editReceptionistModel, cancellationToken);

            var oldReceptionist = await _repositoryManager.ReceptionistRepository.GetItemAsync(id, cancellationToken);
            if (oldReceptionist == null)
            {
                throw new ReceptionistNotFoundException(id);
            }

            var receptionist = _mapper.Map<Receptionist>(model);

            await _repositoryManager.ReceptionistRepository.UpdateAsync(id, receptionist);

            if (oldReceptionist.Info.Photo != null)
            {
                await _blobService.DeleteAsync(oldReceptionist.Info.Photo.Name);
            }

            if (model.Photo != null)
            {
                await _blobService.UploadAsync(model.Photo);
            }
        }

        public async Task<ReceptionistDTO> GetReceptionistAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var receptionist = await _repositoryManager.ReceptionistRepository.GetItemAsync(id, cancellationToken);
            if (receptionist == null)
            {
                throw new ReceptionistNotFoundException(id);
            }

            return await GetReceptionistDTOWithPhotoAsync(receptionist, cancellationToken);
        }

        public IEnumerable<ReceptionistDTO> GetReceptionists(Page page, ReceptionistFiltrationModel filtrationModel)
        {
            return GetFiltratedReceptionists(page, filtrationModel).ToList().Select(x => GetReceptionistDTOWithPhotoAsync(x).Result);
        }

        public IEnumerable<ReceptionistDTO> GetReceptionistsInfo(Page page, ReceptionistFiltrationModel filtrationModel)
        {
            var receptionists = GetFiltratedReceptionists(page, filtrationModel);

            return _mapper.Map<IEnumerable<ReceptionistDTO>>(receptionists);
        }

        private async Task<ReceptionistDTO> GetReceptionistDTOWithPhotoAsync(Receptionist receptionistData, CancellationToken cancellationToken = default)
        {
            var receptionist = _mapper.Map<ReceptionistDTO>(receptionistData);

            if (receptionistData.Info.Photo != null)
            {
                receptionist.Info.Photo = await _blobService.DownloadAsync(receptionistData.Info.Photo.Name, cancellationToken);
            }

            return receptionist;
        }

        private IQueryable<Receptionist> GetFiltratedReceptionists(Page page, ReceptionistFiltrationModel filtrationModel)
        {
            var receptionists = _repositoryManager.ReceptionistRepository.GetItems();
            var filtrator = _mapper.Map<IFiltrator<Receptionist>>(filtrationModel);
            receptionists = filtrator.Filtrate(receptionists);
            receptionists = PageSeparator.GetPage(receptionists, page);
            return receptionists;
        }
    }
}
