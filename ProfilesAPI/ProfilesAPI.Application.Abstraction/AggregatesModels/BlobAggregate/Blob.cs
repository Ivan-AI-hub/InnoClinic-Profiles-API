namespace ProfilesAPI.Application.Abstraction.AggregatesModels.BlobAggregate
{
    public record Blob(string FileName, string ContentType, byte[] Content);
}
