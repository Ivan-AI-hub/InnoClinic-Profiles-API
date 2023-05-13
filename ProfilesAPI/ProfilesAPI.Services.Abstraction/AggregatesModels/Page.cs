namespace ProfilesAPI.Services.Abstraction.AggregatesModels
{
    public class Page
    {
        public int Number { get; }
        public int Size { get; }

        public Page(int number, int size)
        {
            Number = number;
            Size = size;
        }
    }
}
