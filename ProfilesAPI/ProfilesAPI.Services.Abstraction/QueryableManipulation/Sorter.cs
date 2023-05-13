﻿namespace ProfilesAPI.Services.Abstraction.QueryableManipulation
{
    public abstract class Sorter<T>
    {
        public bool IsDescendingSort { get; set; } = false;
        /// <summary>
        /// Performs IQueryable sorting according to the specified rules
        /// </summary>
        /// <returns> Sorted IQueryable </returns>
        public IQueryable<T> Sort(IQueryable<T> query)
        {
            if (IsDescendingSort)
            {
                return DescendingOrder(query);
            }

            return AscendingOrder(query);
        }
        protected abstract IQueryable<T> DescendingOrder(IQueryable<T> query);
        protected abstract IQueryable<T> AscendingOrder(IQueryable<T> query);
    }
}
