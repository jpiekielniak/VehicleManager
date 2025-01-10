using VehicleManager.Application.Emails.Interfaces;
using VehicleManager.Core.Common.Helpers;
using VehicleManager.Core.Vehicles.Builders;
using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Core.Vehicles.Repositories;

namespace VehicleManager.Infrastructure.Common.BackgroundJobs.InspectionChecker.Services;

internal sealed class InspectionCheckerBackgroundService(IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await CheckInspectionsAsync(stoppingToken);
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }

    private async Task CheckInspectionsAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
        var inspectionRepository = scope.ServiceProvider.GetRequiredService<IInspectionRepository>();

        var expiringInspections = await GetExpiringInspectionsAsync(inspectionRepository, cancellationToken);

        foreach (var inspection in expiringInspections)
        {
            await SendExpirationNotificationAsync(
                inspection,
                emailService,
                cancellationToken
            );

            await UpdateInspectionReminderSentAsync(inspection, inspectionRepository, cancellationToken);
        }
    }

    private async Task UpdateInspectionReminderSentAsync(Inspection inspection, IInspectionRepository inspectionRepository, CancellationToken cancellationToken)
    {
        var updatedInspection = new InspectionBuilder(inspection)
            .WithReminderSent(true)
            .Build();
        
        await inspectionRepository.UpdateAsync(updatedInspection, cancellationToken);
    }

    private static async Task<List<Inspection>> GetExpiringInspectionsAsync(IInspectionRepository inspectionRepository,
        CancellationToken cancellationToken)
    {
        var inspectionsReadyForReminder = await inspectionRepository.GetExpiringInspectionsAsync(cancellationToken);

        return inspectionsReadyForReminder;
    }

    private static async Task SendExpirationNotificationAsync(Inspection inspection, IEmailService emailService,
        CancellationToken cancellationToken)
    {
        var email = inspection.ServiceBook.Vehicle.User.Email;
        var vehicleInfo = inspection.ServiceBook.Vehicle.Brand + " " + inspection.ServiceBook.Vehicle.Model + " " +
                          inspection.ServiceBook.Vehicle.LicensePlate;

        await emailService.SendInspectionExpirationEmailAsync(
            email,
            vehicleInfo,
            inspection.PerformDate?.AddYears(1),
            inspection.InspectionType.GetDisplay(),
            cancellationToken
        );
    }
}