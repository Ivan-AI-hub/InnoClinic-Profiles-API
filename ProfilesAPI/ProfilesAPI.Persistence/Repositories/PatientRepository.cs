using Microsoft.EntityFrameworkCore;
using ProfilesAPI.Domain;
using ProfilesAPI.Domain.Exceptions;
using ProfilesAPI.Domain.Interfaces;
using ProfilesAPI.Persistence.Abstract;

namespace ProfilesAPI.Persistence.Repositories
{
    internal class PatientRepository : RepositoryBase<Patient>, IPatientRepository
    {
        public PatientRepository(ProfilesContext context) : base(context)
        {
        }

        public override async Task DeleteAsync(Guid id)
        {
            var patient = await GetItemAsync(id, false);

            if (patient == null)
            {
                throw new PatientNotFoundException(id);
            }

            Context.Patients.Remove(patient);
        }

        public override Task<Patient?> GetItemAsync(Guid id, bool trackChanges = true, CancellationToken cancellationToken = default)
        {
            var patients = GetItems(trackChanges);
            return patients.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public override IQueryable<Patient> GetItems(bool trackChanges)
        {
            var patients = Context.Patients.Include(x => x.Info);
            return trackChanges ? patients : patients.AsNoTracking();
        }

        public override async Task UpdateAsync(Guid id, Patient updatedItem)
        {
            var patient = await GetItemAsync(id, true);

            if (patient == null)
            {
                throw new PatientNotFoundException(id);
            }

            patient.Info.FirstName = updatedItem.Info.FirstName;
            patient.Info.LastName = updatedItem.Info.LastName;
            patient.Info.MiddleName = updatedItem.Info.MiddleName;
            patient.Info.BirthDay = updatedItem.Info.BirthDay;
            patient.Info.Photo = updatedItem.Info.Photo;
            patient.PhoneNumber = updatedItem.PhoneNumber;
        }
    }
}
