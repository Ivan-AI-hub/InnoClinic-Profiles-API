using ProfilesAPI.Domain;
using ProfilesAPI.Services.Abstraction.QueryableManipulation;

namespace ProfilesAPI.Services.Filtrators
{
    public class PatientFiltrator : IFiltrator<Patient>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public IQueryable<Patient> Filtrate(IQueryable<Patient> query)
        {
            query = FirstName != null ? query.Where(x => x.Info.FirstName.ToLower() == FirstName.ToLower()) : query;
            query = LastName != null ? query.Where(x => x.Info.LastName.ToLower() == LastName.ToLower()) : query;
            query = MiddleName != null ? query.Where(x => x.Info.MiddleName.ToLower() == MiddleName.ToLower()) : query;
            return query;
        }
    }
}
