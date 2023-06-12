using Microsoft.EntityFrameworkCore;
using ProfilesAPI.Domain;
using ProfilesAPI.Domain.Exceptions;
using ProfilesAPI.Domain.Interfaces;
using ProfilesAPI.Persistence.Abstract;

namespace ProfilesAPI.Persistence.Repositories
{
    public class ReceptionistRepository : RepositoryBase<Receptionist>, IReceptionistRepository
    {
        public ReceptionistRepository(ProfilesContext context) : base(context)
        {
        }

        public override async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var receptionist = await GetItemAsync(id, cancellationToken);

            if (receptionist == null)
            {
                throw new ReceptionistNotFoundException(id);
            }

            Context.HumansInfo.Remove(receptionist.Info);
            Context.Receptionists.Remove(receptionist);
            await Context.SaveChangesAsync(cancellationToken);
        }

        public override Task<Receptionist?> GetItemAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var receptionists = GetFullDataQueryable();
            return receptionists.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public override async Task UpdateAsync(Guid id, Receptionist updatedItem, CancellationToken cancellationToken = default)
        {
            var receptionist = await Context.Receptionists
                                            .Include(x => x.Info)
                                            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (receptionist == null)
            {
                throw new ReceptionistNotFoundException(id);
            }

            receptionist.Info.FirstName = updatedItem.Info.FirstName;
            receptionist.Info.LastName = updatedItem.Info.LastName;
            receptionist.Info.MiddleName = updatedItem.Info.MiddleName;
            receptionist.Info.BirthDay = updatedItem.Info.BirthDay;
            receptionist.Info.Photo = updatedItem.Info.Photo;
            receptionist.Office.Id = updatedItem.Office.Id;

            await Context.SaveChangesAsync(cancellationToken);
        }

        protected override IQueryable<Receptionist> GetFullDataQueryable()
        {
            return Context.Receptionists.Include(x => x.Info).AsNoTracking();
        }
    }
}
