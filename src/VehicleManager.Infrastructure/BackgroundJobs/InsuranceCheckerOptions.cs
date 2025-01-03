namespace VehicleManager.Infrastructure.BackgroundJobs;

public class InsuranceCheckerOptions
{
    public const string SectionName = "InsuranceChecker";
    public TimeSpan CheckTime { get; init; }
    public int DaysBeforeExpiration { get; init; }
}