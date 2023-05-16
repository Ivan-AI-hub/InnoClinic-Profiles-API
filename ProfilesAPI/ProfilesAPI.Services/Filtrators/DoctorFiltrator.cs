using ProfilesAPI.Domain;

namespace ProfilesAPI.Services.Filtrators
{
    public class DoctorFiltrator : IFiltrator<Doctor>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? Specialization { get; set; }
        public Guid? OfficeId { get; set; }
        public IQueryable<Doctor> Filtrate(IQueryable<Doctor> query)
        {
            query = FirstName != null ? query.Where(x => x.Info.FirstName.ToLower() == FirstName.ToLower()) : query;
            query = LastName != null ? query.Where(x => x.Info.LastName.ToLower() == LastName.ToLower()) : query;
            query = MiddleName != null ? query.Where(x => x.Info.MiddleName.ToLower() == MiddleName.ToLower()) : query;
            query = Specialization != null ? query.Where(x => x.Specialization.ToLower() == Specialization.ToLower()) : query;
            query = OfficeId != null ? query.Where(x => x.Office.Id == OfficeId) : query;

            return query;
        }
    }
}
