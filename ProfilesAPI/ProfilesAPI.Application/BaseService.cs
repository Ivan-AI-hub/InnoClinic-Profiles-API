using FluentValidation;
using ProfilesAPI.Domain.Exceptions;
using ProfilesAPI.Domain.Interfaces;

namespace ProfilesAPI.Application
{
    public abstract class BaseService
    {
        protected IRepositoryManager _repositoryManager;

        public BaseService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        protected async Task ValidateEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var isEmailInvalid = await _repositoryManager.HumanInfoRepository.IsExistAsync(x => x.Email == email, cancellationToken);
            if (isEmailInvalid)
            {
                throw new ProfileWithSameEmailExistException(email);
            }
        }

        protected async Task ValidateModel<Tmodel>(Tmodel model, IValidator<Tmodel> validator, CancellationToken cancellationToken = default)
        {
            var validationResult = await validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }
    }
}
