namespace VehicleManager.Application.Integrations.BlobStorage.Options;

public class BlobStorageOptions
{
    public const string SectionName = "BlobStorage";
    
    public string ConnectionString { get; set; } = default!;
    public string ContainerName { get; set; } = default!;
}