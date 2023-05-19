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

        public IQueryable<T> GetItemsByCondition(Expression<Func<T, bool>> predicate)
        {
            return GetFullDataQueryable().Where(predicate).AsNoTracking();
        }

        public async Task CreateAsync(T item)
        {
            await Context.Set<T>().AddAsync(item);
            await Context.SaveChangesAsync();
        }

        public virtual IQueryable<T> GetItems(int pageSize, int pageNumber, IFiltrator<T> filtrator)
        {
            var items = filtrator.Filtrate(GetFullDataQueryable());
            return GetPage(items, pageSize, pageNumber);
        }

        public abstract Task<T?> GetItemAsync(Guid id, CancellationToken cancellationToken = default);

        public abstract Task UpdateAsync(Guid id, T updatedItem);

        public abstract Task DeleteAsync(Guid id);

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
