namespace VehicleManager.Infrastructure.BackgroundJobs;

public static class Extensions
{
    public static IServiceCollection AddInsuranceCheckerBackgroundJob(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<InsuranceCheckerOptions>(
            configuration.GetSection(InsuranceCheckerOptions.SectionName)
        );

        services.AddHostedService<InsuranceCheckerBackgroundService>();

        return services;
    }
}