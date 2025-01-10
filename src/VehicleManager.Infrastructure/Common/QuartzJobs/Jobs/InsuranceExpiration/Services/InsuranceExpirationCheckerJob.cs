using VehicleManager.Application.Emails.Interfaces;
using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Core.Vehicles.Repositories;

namespace VehicleManager.Infrastructure.Common.QuartzJobs.Jobs.InsuranceExpiration.Services;

internal sealed class InsuranceExpirationCheckerJob(
    IEmailService emailService,
    IInsuranceRepository insuranceRepository
) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var expiringInsurances = await GetExpiringInsurancesAsync(context.CancellationToken);

        foreach (var insurance in expiringInsurances)
        {
            await SendExpirationNotificationAsync(
                insurance,
                context.CancellationToken
            );

            await UpdateInsuranceReminderSentAsync(insurance, context.CancellationToken);
        }
    }

    private async Task UpdateInsuranceReminderSentAsync(Insurance insurance,
        CancellationToken cancellationToken)
    {
        insurance.setReminderSent(true);
        await insuranceRepository.UpdateAsync(insurance, cancellationToken);
    }

    private async Task<List<Insurance>> GetExpiringInsurancesAsync(CancellationToken cancellationToken)
    {
        var policiesReadyForReminder = await insuranceRepository
            .GetExpiringInsurancesAsync(cancellationToken);

        return policiesReadyForReminder;
    }

    private async Task SendExpirationNotificationAsync(Insurance insurance, CancellationToken cancellationToken)
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
        => $"{vehicle.Brand} - {vehicle.Model} - {vehicle.LicensePlate}";
}