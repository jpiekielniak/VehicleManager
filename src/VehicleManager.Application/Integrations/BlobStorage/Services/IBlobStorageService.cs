using Microsoft.AspNetCore.Http;

namespace VehicleManager.Application.Integrations.BlobStorage.Services;

public interface IBlobStorageService
{
    Task<string> UploadImageAsync(IFormFile image, CancellationToken cancellationToken);
    Task DeleteImageAsync(string blobUrl, CancellationToken cancellationToken);
}