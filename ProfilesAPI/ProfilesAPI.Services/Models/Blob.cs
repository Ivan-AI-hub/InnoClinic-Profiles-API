namespace ProfilesAPI.Services.Models
{
    public record Blob(string FileName, string ContentType, byte[] Content);
}
