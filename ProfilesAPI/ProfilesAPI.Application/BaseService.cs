using FluentValidation;
using ProfilesAPI.Domain;
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

        protected async Task ValidateRoleAndExistingAsync(Guid id, Role needRole, CancellationToken cancellationToken = default)
        {
            var user = await _repositoryManager.ProfileRepository.GetItemAsync(id, cancellationToken);
            if (user == null)
            {
                throw new ProfileNotFoundException(id);
            }
            if (user.Role != needRole)
            {
                throw new RoleNotMatchException(user.Role, needRole);
            }
        }

        protected async Task ValidateRoleAndExistingAsync(string email, Role needRole, CancellationToken cancellationToken = default)
        {
            var user = await _repositoryManager.ProfileRepository.GetByEmailAsync(email, cancellationToken);
            if (user == null)
            {
                throw new ProfileNotFoundException(email);
            }
            if (user.Role != needRole)
            {
                throw new RoleNotMatchException(user.Role, needRole);
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
