using FluentValidation;
using Microsoft.AspNetCore.Http;
using ProfilesAPI.Domain.Exceptions;
using ProfilesAPI.Domain.Interfaces;
using ProfilesAPI.Services.Abstraction;
using ProfilesAPI.Services.Exceptions;

namespace ProfilesAPI.Services
{
    public class BaseService
    {
        protected IBlobService _blobService;
        protected IRepositoryManager _repositoryManager;

        public BaseService(IBlobService blobService, IRepositoryManager repositoryManager)
        {
            _blobService = blobService;
            _repositoryManager = repositoryManager;
        }

        protected async Task ValidateBlobFileName(IFormFile? file, CancellationToken cancellationToken = default)
        {
            if (file != null && await _blobService.IsBlobExist(file.FileName, cancellationToken))
                throw new BlobNameIsNotValidException(file.FileName);
        }

        protected async Task ValidateEmail(string email)
        {
            var isEmailInvalid = await _repositoryManager.HumanInfoRepository.IsItemExistAsync(x => x.Email == email);
            if (isEmailInvalid)
                throw new ProfileWithSameEmailExistException(email);
        }

        protected async Task ValidateModel<Tmodel>(Tmodel model, IValidator<Tmodel> validator, CancellationToken cancellationToken = default)
        {
            var validationResult = await validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }
    }
}
