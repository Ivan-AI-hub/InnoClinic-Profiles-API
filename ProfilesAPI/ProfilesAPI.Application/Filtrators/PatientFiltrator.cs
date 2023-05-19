using ProfilesAPI.Application.Abstraction.AggregatesModels.PatientAggregate;
using ProfilesAPI.Domain;
using ProfilesAPI.Domain.Interfaces;

namespace ProfilesAPI.Application.Filtrators
{
    public class PatientFiltrator : PatientFiltrationModel, IFiltrator<Patient>
    {
        public IQueryable<Patient> Filtrate(IQueryable<Patient> query)
        {
            query = FirstName != null ? query.Where(x => x.Info.FirstName.ToLower().Contains(FirstName.ToLower())) : query;
            query = LastName != null ? query.Where(x => x.Info.LastName.ToLower().Contains(LastName.ToLower())) : query;
            query = MiddleName != null ? query.Where(x => x.Info.MiddleName.ToLower().Contains(MiddleName.ToLower())) : query;
            return query;
        }
    }
}
