using ProfilesAPI.Application.Abstraction.AggregatesModels;

namespace ProfilesAPI.Application.Abstraction.QueryableManipulation
{
    public static class PageSeparator
    {
        public static IQueryable<T> GetPage<T>(IQueryable<T> query, Page page)
        {
            return query.Skip(page.Size * (page.Number - 1)).Take(page.Size);
        }
    }
}
