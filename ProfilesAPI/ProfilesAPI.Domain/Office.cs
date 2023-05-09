namespace ProfilesAPI.Domain
{
    public class Office
    {
        public Guid Id { get; private set; }
        private Office() { }
        public Office(Guid id)
        {
            Id = id;
        }
    }
}
