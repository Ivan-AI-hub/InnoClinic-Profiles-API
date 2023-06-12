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
            return GetFullDataQueryable().AnyAsync(predicate, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetItemsByConditionAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await GetFullDataQueryable().Where(predicate).AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task CreateAsync(T item, CancellationToken cancellationToken = default)
        {
            await Context.Set<T>().AddAsync(item, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<T>> GetItemsAsync(int pageSize, int pageNumber, IFiltrator<T> filtrator, CancellationToken cancellationToken = default)
        {
            var items = filtrator.Filtrate(GetFullDataQueryable());
            return await GetPage(items, pageSize, pageNumber).ToListAsync(cancellationToken);
        }

        public abstract Task<T?> GetItemAsync(Guid id, CancellationToken cancellationToken = default);

        public abstract Task UpdateAsync(Guid id, T updatedItem, CancellationToken cancellationToken = default);

        public abstract Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        protected virtual IQueryable<T> GetFullDataQueryable()
        {
            return Context.Set<T>().AsNoTracking();
        }

        protected IQueryable<T> GetPage(IQueryable<T> query, int pageSize, int pageNumber)
        {
            return query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
        }
    }
}
