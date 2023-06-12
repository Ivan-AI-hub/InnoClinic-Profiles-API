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

        public override async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var doctor = await GetItemAsync(id, cancellationToken);

            if (doctor == null)
            {
                throw new DoctorNotFoundException(id);
            }

            Context.HumansInfo.Remove(doctor.Info);
            Context.Doctors.Remove(doctor);
            await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateStatusAsync(Guid id, WorkStatus status, CancellationToken cancellationToken = default)
        {
            var doctor = await Context.Doctors.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (doctor == null)
            {
                throw new DoctorNotFoundException(id);
            }

            doctor.Status = status;
            await Context.SaveChangesAsync(cancellationToken);
        }

        public override Task<Doctor?> GetItemAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var doctors = GetFullDataQueryable();
            return doctors.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public override async Task UpdateAsync(Guid id, Doctor updatedItem, CancellationToken cancellationToken = default)
        {
            var doctor = await Context.Doctors.Include(x => x.Info)
                                        .FirstOrDefaultAsync(x => x.Id == id);

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

            await Context.SaveChangesAsync(cancellationToken);
        }

        protected override IQueryable<Doctor> GetFullDataQueryable()
        {
            return Context.Doctors.Include(x => x.Info).AsNoTracking();
        }
    }
}
