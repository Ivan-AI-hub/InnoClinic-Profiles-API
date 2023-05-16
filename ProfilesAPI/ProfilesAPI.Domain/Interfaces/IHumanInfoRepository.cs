using System.Linq.Expressions;

namespace ProfilesAPI.Domain.Interfaces
{
    public interface IHumanInfoRepository
    {
        /// <returns>true if the element exists, and false if not</returns>
        public Task<bool> IsExistAsync(Expression<Func<HumanInfo, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
