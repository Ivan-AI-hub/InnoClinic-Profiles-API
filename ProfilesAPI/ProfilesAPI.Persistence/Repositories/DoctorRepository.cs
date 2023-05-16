using Microsoft.EntityFrameworkCore;
using ProfilesAPI.Domain;
using ProfilesAPI.Domain.Exceptions;
using ProfilesAPI.Domain.Interfaces;
using ProfilesAPI.Persistence.Abstract;

namespace ProfilesAPI.Persistence.Repositories
{
    public class DoctorRepository : RepositoryBase<Doctor>, IDoctorRepository
    {
        public DoctorRepository(ProfilesContext context) : base(context)
        {
        }

        public override async Task DeleteAsync(Guid id)
        {
            var doctor = await GetItemAsync(id, false);

            if (doctor == null)
            {
                throw new DoctorNotFoundException(id);
            }

            Context.Doctors.Remove(doctor);
        }

        public override Task<Doctor?> GetItemAsync(Guid id, bool trackChanges = true, CancellationToken cancellationToken = default)
        {
            var doctors = GetItems(trackChanges);
            return doctors.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public override IQueryable<Doctor> GetItems(bool trackChanges)
        {
            var doctors = Context.Doctors.Include(x => x.Info);
            return trackChanges ? doctors : doctors.AsNoTracking();
        }

        public override async Task UpdateAsync(Guid id, Doctor updatedItem)
        {
            var doctor = await GetItemAsync(id, true);

            if (doctor == null)
            {
                throw new DoctorNotFoundException(id);
            }

            doctor.Info.FirstName = updatedItem.Info.FirstName;
            doctor.Info.LastName = updatedItem.Info.LastName;
            doctor.Info.MiddleName = updatedItem.Info.MiddleName;
            doctor.Info.BirthDay = updatedItem.Info.BirthDay;
            doctor.Info.Photo = updatedItem.Info.Photo;
            doctor.Specialization = updatedItem.Specialization;
            doctor.Office.Id = updatedItem.Office.Id;
            doctor.CareerStartYear = updatedItem.CareerStartYear;
            doctor.Status = updatedItem.Status;

        }
    }
}
