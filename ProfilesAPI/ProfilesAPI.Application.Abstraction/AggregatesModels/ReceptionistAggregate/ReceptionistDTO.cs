using ProfilesAPI.Application.Abstraction.AggregatesModels.DoctorAggregate;

namespace ProfilesAPI.Application.Abstraction.AggregatesModels.ReceptionistAggregate
{
    public class ReceptionistDTO
    {
        public Guid Id { get; private set; }
        public HumanInfoDTO Info { get; private set; }
        public OfficeDTO Office { get; private set; }

        public ReceptionistDTO(Guid id, HumanInfoDTO info, OfficeDTO office)
        {
            Id = id;
            Info = info;
            Office = office;
        }
    }
}