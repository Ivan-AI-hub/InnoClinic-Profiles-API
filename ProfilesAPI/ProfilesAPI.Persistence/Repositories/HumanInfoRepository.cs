using Microsoft.EntityFrameworkCore;
using ProfilesAPI.Domain;
using ProfilesAPI.Domain.Interfaces;
using System.Linq.Expressions;

namespace ProfilesAPI.Persistence.Repositories
{
    public class HumanInfoRepository : IHumanInfoRepository
    {
        private ProfilesContext _context;
        public HumanInfoRepository(ProfilesContext context)
        {
            _context = context;
        }
        public Task<bool> IsExistAsync(Expression<Func<HumanInfo, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return _context.HumansInfo.AnyAsync(predicate, cancellationToken);
        }
    }
}
