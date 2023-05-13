using ProfilesAPI.Services.Abstraction.AggregatesModels;

namespace ProfilesAPI.Services.Abstraction.QueryableManipulation
{
    public static class PageSeparator
    {
        public static IQueryable<T> GetPage<T>(IQueryable<T> query, Page page)
        {
            return query.Skip(page.Size * (page.Number - 1)).Take(page.Size);
        }
    }
}
