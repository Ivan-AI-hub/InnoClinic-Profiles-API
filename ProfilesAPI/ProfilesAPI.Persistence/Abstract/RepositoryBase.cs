using Microsoft.EntityFrameworkCore;
using ProfilesAPI.Domain.Interfaces;
using System.Linq.Expressions;

namespace ProfilesAPI.Persistence.Abstract
{
    public abstract class RepositoryBase<T> : IRepository<T>
        where T : class
    {
        protected readonly ProfilesContext Context;
        public RepositoryBase(ProfilesContext context)
        {
            Context = context;
        }

        public Task<bool> IsItemExistAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return Context.Set<T>().AnyAsync(predicate, cancellationToken);
        }

        public IQueryable<T> GetItemsByCondition(Expression<Func<T, bool>> predicate, bool trackChanges)
        {
            return GetItems(trackChanges).Where(predicate);
        }

        public async Task CreateAsync(T item)
        {
            await Context.Set<T>().AddAsync(item);
        }

        public abstract IQueryable<T> GetItems(bool trackChanges);

        public abstract Task<T?> GetItemAsync(Guid id, bool trackChanges = true, CancellationToken cancellationToken = default);

        public abstract Task UpdateAsync(Guid id, T updatedItem);

        public abstract Task DeleteAsync(Guid id);
    }
}
