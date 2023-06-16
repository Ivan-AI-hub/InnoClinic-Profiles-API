namespace ProfilesAPI.Application.Abstraction.AggregatesModels.HumanInfoAggregate
{
    public class PictureDTO
    {
        public string Name { get; set; }

        public PictureDTO() { }
        public PictureDTO(string name)
        {
            Name = name;
        }
    }
}
