namespace ProfilesAPI.Domain.Interfaces
{
    public interface IFiltrator<T>
    {
        /// <summary>
        /// Performs IQueryable filtering according to the specified rules
        /// </summary>
        /// <returns> Filtered IQueryable </returns>
        public IQueryable<T> Filtrate(IQueryable<T> query);
    }
}
