using VehicleManager.Infrastructure.Common.BackgroundJobs.InsuranceChecker.Options;
using VehicleManager.Infrastructure.Common.BackgroundJobs.InsuranceChecker.Services;

namespace VehicleManager.Infrastructure.Common.BackgroundJobs;

public static class BackgroundJobExtensions
{
    public static IServiceCollection AddBackgroundJobs(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<InsuranceCheckerOptions>(
            configuration.GetSection(InsuranceCheckerOptions.SectionName)
        );

        services.AddHostedService<InsuranceCheckerBackgroundService>();

        return services;
    }
}