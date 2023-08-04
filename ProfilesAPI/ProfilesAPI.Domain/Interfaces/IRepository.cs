using System.Linq.Expressions;

namespace ProfilesAPI.Domain.Interfaces
{
    public interface IRepository<T>
    {

        /// <param name="id">item's id</param>
        /// <returns>Item if it wes found or null if not</returns>
        Task<T?> GetItemAsync(Guid id, CancellationToken cancellationToken = default);

        /// <param name="predicate">Special predicate for element search</param>
        /// <returns>IQueryable collection</returns>
        public Task<IEnumerable<T>> GetItemsByConditionAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        /// <returns>queryable items from the database</returns>
        public Task<IEnumerable<T>> GetItemsAsync(int pageSize, int pageNumber, IFiltrator<T> filtrator, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create item in database
        /// </summary>
        public Task CreateAsync(T item, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update item in database
        /// </summary>
        public Task UpdateAsync(Guid id, T updatedItem, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete item from database
        /// </summary>
        public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        /// <returns>true if the element exists, and false if not</returns>
        public Task<bool> IsItemExistAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
