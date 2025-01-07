using Microsoft.AspNetCore.Http.Features;

namespace VehicleManager.Api.Configuration.FormFile;

public static class FormFileConfiguration
{
    private const int Megabyte = 1024 * 1024;
    private const int MaxFileSizeMb = 5;

    public static IServiceCollection AddFormFileConfiguration(this IServiceCollection services)
    {
        services.Configure<FormOptions>(options =>
            options.MultipartBodyLengthLimit = MaxFileSizeMb * Megabyte
        );

        return services;
    }
}