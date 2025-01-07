using Microsoft.Extensions.Configuration;
using VehicleManager.Application.Integrations.BlobStorage.Options;
using VehicleManager.Application.Integrations.BlobStorage.Services;

namespace VehicleManager.Application.Integrations.BlobStorage;

public static class BlobStorageExtensions
{
    public static IServiceCollection AddBlobStorage(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<BlobStorageOptions>(
            configuration.GetSection(BlobStorageOptions.SectionName));

        services.AddScoped<IBlobStorageService, BlobStorageService>();

        return services;
    }
}