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
            var patient = await GetItemAsync(id);

            if (patient == null)
            {
                throw new PatientNotFoundException(id);
            }

            Context.HumansInfo.Remove(patient.Info);
            Context.Patients.Remove(patient);
            await Context.SaveChangesAsync();
        }

        public override Task<Patient?> GetItemAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var patients = GetItems();
            return patients.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public override IQueryable<Patient> GetItems()
        {
            var patients = Context.Patients.Include(x => x.Info);
            return patients.AsNoTracking();
        }

        public override async Task UpdateAsync(Guid id, Patient updatedItem)
        {
            var patient = await Context.Patients.FirstOrDefaultAsync(x => x.Id == id);

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

            await Context.SaveChangesAsync();
        }
    }
}
