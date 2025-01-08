using VehicleManager.Application.Emails.Interfaces;
using VehicleManager.Core.Vehicles.Entities;using VehicleManager.Core.Vehicles.Repositories;
using VehicleManager.Infrastructure.Common.BackgroundJobs.InsuranceChecker.Options;

namespace VehicleManager.Infrastructure.Common.BackgroundJobs.InsuranceChecker.Services;

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
            await CheckInsurancesAsync(stoppingToken);
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }

    private async Task CheckInsurancesAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
        var insuranceRepository = scope.ServiceProvider.GetRequiredService<IInsuranceRepository>();

        var expiringInsurances = await GetExpiringInsurancesAsync(insuranceRepository, cancellationToken);

        foreach (var insurance in expiringInsurances)
        {
            await SendExpirationNotificationAsync(
                insurance,
                emailService,
                cancellationToken
            );

            await UpdateInsuranceReminderSentAsync(insurance, insuranceRepository, cancellationToken);
        }
    }

    private async Task UpdateInsuranceReminderSentAsync(Insurance insurance, IInsuranceRepository insuranceRepository,
        CancellationToken cancellationToken)
    {
        insurance.setReminderSent(true);
        await insuranceRepository.UpdateAsync(insurance, cancellationToken);
    }

    private async Task<List<Insurance>> GetExpiringInsurancesAsync(
        IInsuranceRepository insuranceRepository,
        CancellationToken cancellationToken)
    {
        var policiesReadyForReminder = await insuranceRepository
            .GetExpiringInsurancesAsync(cancellationToken);

        return policiesReadyForReminder;
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