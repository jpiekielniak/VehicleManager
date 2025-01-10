using VehicleManager.Infrastructure.Common.QuartzJobs.Jobs.InspectionExpiration.Services;
using VehicleManager.Infrastructure.Common.QuartzJobs.Jobs.InsuranceExpiration.Services;
using VehicleManager.Infrastructure.Common.QuartzJobs.Options;

namespace VehicleManager.Infrastructure.Common.QuartzJobs;

public static class QuartzJobsExtensions
{
    public static IServiceCollection AddQuartzJobs(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<QuartzJobOptions>(
            configuration.GetSection(QuartzJobOptions.SectionName)
        );

        services.AddSingleton<IJobFactory, MicrosoftDependencyInjectionJobFactory>();

        services.AddQuartz(q =>
        {
            var options = services
                .BuildServiceProvider()
                .GetRequiredService<IOptions<QuartzJobOptions>>()
                .Value;

            AddJob<InspectionExpirationCheckerJob>(q, options.DailyAtNoonCronExpression);
            AddJob<InsuranceExpirationCheckerJob>(q, options.DailyAtNoonCronExpression);
        });

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        return services;
    }

    private static void AddJob<T>(IServiceCollectionQuartzConfigurator q, string cronExpression)
        where T : IJob
    {
        var jobKey = JobKey.Create(typeof(T).Name);

        q.AddJob<T>(j => j
            .WithIdentity(jobKey)
            .StoreDurably()
        );

        q.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity($"{typeof(T).Name}Trigger")
            .WithCronSchedule(cronExpression)
        );
    }
}