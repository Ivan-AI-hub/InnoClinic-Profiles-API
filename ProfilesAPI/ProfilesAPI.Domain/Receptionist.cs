namespace ProfilesAPI.Domain
{
    public class Receptionist
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public HumanInfo Info { get; private set; }
        public Office Office { get; private set; }
        private Receptionist() { }
        public Receptionist(HumanInfo info, Office office)
        {
            Info = info;
            Office = office;
        }
    }
}
