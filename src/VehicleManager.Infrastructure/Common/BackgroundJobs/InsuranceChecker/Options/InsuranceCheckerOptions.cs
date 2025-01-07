namespace VehicleManager.Infrastructure.Common.BackgroundJobs.InsuranceChecker.Options;

public class InsuranceCheckerOptions
{
    public const string SectionName = "InsuranceChecker";
    public TimeSpan CheckTime { get; init; }
    public int DaysBeforeExpiration { get; init; }
}