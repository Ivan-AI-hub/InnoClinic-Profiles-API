using ProfilesAPI.Domain;

namespace ProfilesAPI.Application.Filtrators
{
    public class ReceptionistFiltrator : IFiltrator<Receptionist>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public IQueryable<Receptionist> Filtrate(IQueryable<Receptionist> query)
        {
            query = FirstName != null ? query.Where(x => x.Info.FirstName.ToLower() == FirstName.ToLower()) : query;
            query = LastName != null ? query.Where(x => x.Info.LastName.ToLower() == LastName.ToLower()) : query;
            query = MiddleName != null ? query.Where(x => x.Info.MiddleName.ToLower() == MiddleName.ToLower()) : query;
            return query;
        }
    }
}
