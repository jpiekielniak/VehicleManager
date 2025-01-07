namespace VehicleManager.Api.Configuration.Cors;

public static class CorsConfiguration
{
    private const string AllowAllOrigins = "AllowAllOrigins";

    public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(AllowAllOrigins, policy =>
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });

        return services;
    }
}