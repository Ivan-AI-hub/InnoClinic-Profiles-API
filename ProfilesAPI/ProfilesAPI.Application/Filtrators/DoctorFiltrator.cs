using ProfilesAPI.Application.Abstraction.AggregatesModels.DoctorAggregate;
using ProfilesAPI.Domain;
using ProfilesAPI.Domain.Interfaces;

namespace ProfilesAPI.Application.Filtrators
{
    public class DoctorFiltrator : DoctorFiltrationModel, IFiltrator<Doctor>
    {
        public IQueryable<Doctor> Filtrate(IQueryable<Doctor> query)
        {
            query = FirstName != null ? query.Where(x => x.Info.FirstName.ToLower().Contains(FirstName.ToLower())) : query;
            query = LastName != null ? query.Where(x => x.Info.LastName.ToLower().Contains(LastName.ToLower())) : query;
            query = MiddleName != null ? query.Where(x => x.Info.MiddleName.ToLower().Contains(MiddleName.ToLower())) : query;
            query = Specialization != null ? query.Where(x => x.Specialization.ToLower().Contains(Specialization.ToLower())) : query;
            query = OfficeId != null ? query.Where(x => x.Office.Id == OfficeId) : query;

            return query;
        }
    }
}
