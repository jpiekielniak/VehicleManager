using VehicleManager.Application.Emails.Interfaces;
using VehicleManager.Core.Common.Helpers;
using VehicleManager.Core.Vehicles.Builders;
using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Core.Vehicles.Repositories;

namespace VehicleManager.Infrastructure.Common.QuartzJobs.Jobs.InspectionExpiration.Services;

internal sealed class InspectionExpirationCheckerJob(
    IEmailService emailService,
    IInspectionRepository inspectionRepository
) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var expiringInspections = await GetExpiringInspectionsAsync(context.CancellationToken);

        foreach (var inspection in expiringInspections)
        {
            await SendExpirationNotificationAsync(inspection, context.CancellationToken);
            await UpdateInspectionReminderSentAsync(inspection, context.CancellationToken);
        }
    }

    private async Task<List<Inspection>> GetExpiringInspectionsAsync(CancellationToken cancellationToken) =>
        await inspectionRepository.GetExpiringInspectionsAsync(cancellationToken);

    private async Task SendExpirationNotificationAsync(Inspection inspection,
        CancellationToken cancellationToken)
    {
        var email = inspection.ServiceBook.Vehicle.User.Email;
        var vehicleInfo = FormatVehicleInfo(inspection);

        await emailService.SendInspectionExpirationEmailAsync(
            email,
            vehicleInfo,
            inspection.PerformDate?.AddYears(1),
            inspection.InspectionType.GetDisplay(),
            cancellationToken
        );
    }

    private async Task UpdateInspectionReminderSentAsync(Inspection inspection, CancellationToken cancellationToken)
    {
        var updatedInspection = new InspectionBuilder(inspection)
            .WithReminderSent(true)
            .Build();

        await inspectionRepository.UpdateAsync(updatedInspection, cancellationToken);
    }

    private static string FormatVehicleInfo(Inspection inspection) =>
        $"{inspection.ServiceBook.Vehicle.Brand} - {inspection.ServiceBook.Vehicle.Model} - {inspection.ServiceBook.Vehicle.LicensePlate}";
}