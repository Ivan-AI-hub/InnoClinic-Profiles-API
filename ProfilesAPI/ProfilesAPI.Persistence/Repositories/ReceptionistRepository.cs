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

        public override async Task DeleteAsync(Guid id)
        {
            var receptionist = await GetItemAsync(id, false);

            if (receptionist == null)
            {
                throw new ReceptionistNotFoundException(id);
            }

            Context.HumansInfo.Remove(receptionist.Info);
            Context.Receptionists.Remove(receptionist);
        }

        public override Task<Receptionist?> GetItemAsync(Guid id, bool trackChanges = true, CancellationToken cancellationToken = default)
        {
            var receptionists = GetItems(trackChanges);
            return receptionists.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public override IQueryable<Receptionist> GetItems(bool trackChanges)
        {
            var receptionists = Context.Receptionists.Include(x => x.Info);
            return trackChanges ? receptionists : receptionists.AsNoTracking();
        }

        public override async Task UpdateAsync(Guid id, Receptionist updatedItem)
        {
            var receptionist = await GetItemAsync(id, true);

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
        }
    }
}
