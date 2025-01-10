namespace VehicleManager.Infrastructure.Common.QuartzJobs.Options;

public class QuartzJobOptions
{
    public const string SectionName = "QuartzJobs";
    public string DailyAtNoonCronExpression { get; init; }
}