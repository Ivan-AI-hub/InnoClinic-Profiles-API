using ProfilesAPI.Domain;
using ProfilesAPI.Services.Abstraction.QueryableManipulation;

namespace ProfilesAPI.Services.Filtrators
{
    internal class DoctorFiltrator : IFiltrator<Doctor>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? Specialization { get; set; }
        public Office? Office { get; set; }
        public IQueryable<Doctor> Filtrate(IQueryable<Doctor> query)
        {
            query = FirstName != null ? query.Where(x => x.Info.FirstName.ToLower() == FirstName.ToLower()) : query;
            query = LastName != null ? query.Where(x => x.Info.LastName.ToLower() == LastName.ToLower()) : query;
            query = MiddleName != null ? query.Where(x => x.Info.MiddleName.ToLower() == MiddleName.ToLower()) : query;
            query = Specialization != null ? query.Where(x => x.Specialization.ToLower() == Specialization.ToLower()) : query;
            query = Office != null ? query.Where(x => x.Office.Id == Office.Id) : query;

            return query;
        }
    }
}
