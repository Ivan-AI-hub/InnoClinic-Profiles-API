using Microsoft.AspNetCore.Http;
using ProfilesAPI.Application.Abstraction.AggregatesModels;

namespace ProfilesAPI.Application.Abstraction
{
    public interface IBlobService
    {
        Task DeleteAsync(string blobFileName, CancellationToken cancellationToken = default);
        Task<Blob?> DownloadAsync(string blobFileName, CancellationToken cancellationToken = default);
        Task<bool> IsBlobExist(string blobFileName, CancellationToken cancellationToken = default);
        Task UploadAsync(IFormFile blob, CancellationToken cancellationToken = default);
    }
}