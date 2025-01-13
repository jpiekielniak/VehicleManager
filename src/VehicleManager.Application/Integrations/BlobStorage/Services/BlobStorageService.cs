using Azure.Storage.Blobs;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Options;
using VehicleManager.Application.Integrations.BlobStorage.Options;

namespace VehicleManager.Application.Integrations.BlobStorage.Services;

public class BlobStorageService(IOptions<BlobStorageOptions> options) : IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient = new(options.Value.ConnectionString);
    private readonly string _containerName = options.Value.ContainerName;

    public async Task<string> UploadImageAsync(
        IFormFile file,
        CancellationToken cancellationToken)
    {
        var account = CloudStorageAccount.Parse(options.Value.ConnectionString);
        var client = account.CreateCloudBlobClient();
        var container = client.GetContainerReference(_containerName);
        await container.CreateIfNotExistsAsync(cancellationToken);
        var photo = container.GetBlockBlobReference(Path.GetFileName(file.FileName));
        await using var stream = file.OpenReadStream();
        await photo.UploadFromStreamAsync(stream, cancellationToken);

        return photo.Uri.ToString();
    }

    public async Task DeleteImageAsync(
        string blobUrl,
        CancellationToken cancellationToken)
    {
        var uri = new Uri(blobUrl);
        var blobName = Path.GetFileName(uri.LocalPath);

        var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        var blobClient = containerClient.GetBlobClient(blobName);

        await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }
}