using VehicleManager.Infrastructure.EF;
using VehicleManager.Infrastructure.Emails;
using VehicleManager.Core.Vehicles.Entities;

namespace VehicleManager.Infrastructure.BackgroundJobs;

public sealed class InsuranceCheckerBackgroundService(
    IServiceProvider serviceProvider,
    IOptions<InsuranceCheckerOptions> options
) : BackgroundService
{
    private readonly InsuranceCheckerOptions _options = options.Value;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
           // await CheckInsurancesAsync(stoppingToken);
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }

    private async Task CheckInsurancesAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<VehicleManagerDbContext>();
        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

        var expiringInsurances = await GetExpiringInsurancesAsync(dbContext, cancellationToken);

        foreach (var insurance in expiringInsurances)
        {
            await SendExpirationNotificationAsync(
                insurance,
                emailService,
                cancellationToken
            );
        }
    }

    private async Task<List<Insurance>> GetExpiringInsurancesAsync(
        VehicleManagerDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var today = DateTimeOffset.UtcNow.Date;

        return await dbContext.Insurances
            .Include(i => i.Vehicle)
            .ThenInclude(i => i.User)
            .Where(i =>
                (i.ValidTo.Date - today).TotalDays <= _options.DaysBeforeExpiration &&
                (i.ValidTo.Date - today).TotalDays >= 0)
            .ToListAsync(cancellationToken);
    }

    private static async Task SendExpirationNotificationAsync(
        Insurance insurance,
        IEmailService emailService,
        CancellationToken cancellationToken)
    {
        var vehicleInfo = FormatVehicleInfo(insurance.Vehicle);

        await emailService.SendInsuranceExpirationEmailAsync(
            insurance.Vehicle.User.Email,
            vehicleInfo,
            insurance.ValidTo,
            insurance.PolicyNumber,
            insurance.Provider,
            cancellationToken
        );
    }

    private static string FormatVehicleInfo(Vehicle vehicle)
        => $"{vehicle.Brand} {vehicle.Model} ({vehicle.LicensePlate})";
}