using MassTransit;
using ProfilesAPI.Application.Abstraction.AggregatesModels.ProfileAggregate;
using ProfilesAPI.Domain;
using ProfilesAPI.Domain.Exceptions;
using ProfilesAPI.Domain.Interfaces;
using SharedEvents.Models;

namespace ProfilesAPI.Application
{
    public class ProfileService : IProfileService
    {
        private IRepositoryManager _repositoryManager;
        private IPublishEndpoint _publishEndpoint;

        public ProfileService(IRepositoryManager repositoryManager, IPublishEndpoint publishEndpoint)
        {
            _repositoryManager = repositoryManager;
            _publishEndpoint = publishEndpoint;
        }

        public async Task UpdateRoleAsync(string email, string role, CancellationToken cancellationToken = default)
        {
            var user = await _repositoryManager.ProfileRepository.GetByEmailAsync(email, cancellationToken);
            if (user == null)
            {
                throw new ProfileNotFoundException(email);
            }

            var newRole = Enum.Parse<Role>(role);
            await _repositoryManager.ProfileRepository.UpdateRoleAsync(email, Enum.Parse<Role>(role), cancellationToken);

            switch(user.Role)
            {
                case Role.Patient: 
                    await _publishEndpoint.Publish(new PatientDeleted(user.Id));
                    break;
                case Role.Doctor:
                    await _publishEndpoint.Publish(new DoctorDeleted(user.Id));
                    break;
            }
            switch (newRole)
            {
                case Role.Patient:
                    await _publishEndpoint.Publish(new PatientCreated(user.Id, user.Info.Email, user.Info.FirstName, user.Info.MiddleName, user.Info.LastName, user.PhoneNumber ?? default, user.Info.BirthDay));
                    break;
                case Role.Doctor:
                    await _publishEndpoint.Publish(new DoctorCreated(user.Id, user.Info.Email, user.Info.FirstName, user.Info.MiddleName, user.Info.LastName, user.Office?.Id ?? default, user.Specialization ?? default, user.CareerStartYear ?? 0, user.Info.BirthDay));
                    break;
            }
        }
    }
}
