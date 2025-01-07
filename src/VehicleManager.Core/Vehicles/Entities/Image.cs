namespace VehicleManager.Core.Vehicles.Entities;

public class Image
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = default!;
    public string BlobUrl { get; set; } = default!;
    public string FileName { get; set; } = default!;
    public DateTimeOffset UploadedAt { get; init; } = DateTimeOffset.UtcNow;

    private Image()
    {
    }

    private Image(Guid vehicleId, string blobUrl, string fileName)
    {
        VehicleId = vehicleId;
        BlobUrl = blobUrl;
        FileName = fileName;
    }

    public static Image Create(Guid vehicleId, string blobUrl, string fileName)
        => new(vehicleId, blobUrl, fileName);
}