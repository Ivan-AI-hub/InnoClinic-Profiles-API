using Microsoft.EntityFrameworkCore;
using ProfilesAPI.Domain;
using ProfilesAPI.Domain.Exceptions;
using ProfilesAPI.Domain.Interfaces;
using ProfilesAPI.Persistence.Abstract;

namespace ProfilesAPI.Persistence.Repositories
{
    public class ProfileRepository : RepositoryBase<Profile>, IProfileRepository
    {
        public ProfileRepository(ProfilesContext context) : base(context)
        {
        }

        public override async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var profile = await GetItemAsync(id, cancellationToken);

            if (profile == null)
            {
                throw new ProfileNotFoundException(id);
            }

            Context.HumansInfo.Remove(profile.Info);
            Context.Profiles.Remove(profile);
            await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateStatusAsync(Guid id, WorkStatus status, CancellationToken cancellationToken = default)
        {
            var profile = await Context.Profiles.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (profile == null)
            {
                throw new ProfileNotFoundException(id);
            }

            profile.Status = status;
            await Context.SaveChangesAsync(cancellationToken);
        }
        public async Task UpdateRoleAsync(string email, Role role, CancellationToken cancellationToken = default)
        {
            var profile = await Context.Profiles.Include(x => x.Info).FirstOrDefaultAsync(x => x.Info.Email == email, cancellationToken);

            if (profile == null)
            {
                throw new ProfileNotFoundException(email);
            }

            profile.Role = role;
            await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Profile>> GetItemsByRoleAsync(int pageSize, int pageNumber, Role role, CancellationToken cancellationToken = default)
        {
            var doctors = GetFullDataQueryable().Where(x => x.Role == role);
            return await GetPage(doctors, pageSize, pageNumber).ToListAsync(cancellationToken);
        }

        public override Task<Profile?> GetItemAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var doctors = GetFullDataQueryable();
            return doctors.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Profile?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var doctors = GetFullDataQueryable();
            return await doctors.FirstOrDefaultAsync(x => x.Info.Email == email, cancellationToken);
        }

        public override async Task UpdateAsync(Guid id, Profile updatedItem, CancellationToken cancellationToken = default)
        {
            var profile = await Context.Profiles.Include(x => x.Info)
                                        .FirstOrDefaultAsync(x => x.Id == id);

            if (profile == null)
            {
                throw new ProfileNotFoundException(id);
            }

            profile.Info.FirstName = updatedItem.Info.FirstName;
            profile.Info.LastName = updatedItem.Info.LastName;
            profile.Info.MiddleName = updatedItem.Info.MiddleName;
            profile.Info.BirthDay = updatedItem.Info.BirthDay;
            profile.Info.Photo = updatedItem.Info.Photo;
            profile.Specialization = updatedItem.Specialization;
            profile.Office = updatedItem.Office;
            profile.CareerStartYear = updatedItem.CareerStartYear;
            profile.Status = updatedItem.Status;
            profile.PhoneNumber = updatedItem.PhoneNumber;

            await Context.SaveChangesAsync(cancellationToken);
        }

        protected override IQueryable<Profile> GetFullDataQueryable()
        {
            return Context.Profiles.Include(x => x.Info).AsNoTracking();
        }

        public async Task<IEnumerable<Profile>> GetItemsByRoleAsync(Role role, int pageSize, int pageNumber, IFiltrator<Profile> filtrator, CancellationToken cancellationToken = default)
        {
            var doctors = GetFullDataQueryable().Where(x => x.Role == role);
            doctors = filtrator.Filtrate(doctors);
            return await GetPage(doctors, pageSize, pageNumber).ToListAsync(cancellationToken);
        }
    }
}
