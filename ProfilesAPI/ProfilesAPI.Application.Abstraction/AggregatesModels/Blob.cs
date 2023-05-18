namespace ProfilesAPI.Application.Abstraction.AggregatesModels
{
    public record Blob(string FileName, string ContentType, byte[] Content);
}
