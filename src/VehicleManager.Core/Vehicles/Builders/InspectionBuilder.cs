using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Core.Vehicles.Entities.Enums;

namespace VehicleManager.Core.Vehicles.Builders;

public class InspectionBuilder(Inspection inspection)
{
    public InspectionBuilder() : this(Inspection.Create())
    {
    }

    public Inspection Build() => inspection;

    public InspectionBuilder WithScheduledDate(DateTimeOffset scheduledDate)
    {
        ArgumentNullException.ThrowIfNull(scheduledDate);
        inspection.ScheduledDate = scheduledDate;
        return this;
    }

    public InspectionBuilder WithPerformDate(DateTimeOffset? performDate)
    {
        inspection.PerformDate = performDate;
        return this;
    }

    public InspectionBuilder WithInspectionType(InspectionType inspectionType)
    {
        if (!Enum.IsDefined(typeof(InspectionType), inspectionType))
        {
            throw new InvalidEnumArgumentException("InspectionType is invalid");
        }

        inspection.InspectionType = inspectionType;
        return this;
    }

    public InspectionBuilder WithServiceBook(ServiceBook serviceBook)
    {
        inspection.ServiceBook = serviceBook;
        inspection.ServiceBookId = serviceBook.Id;
        return this;
    }

    public InspectionBuilder WithTitle(string title)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        inspection.Title = title;
        return this;
    }

    public InspectionBuilder WithReminderSent(bool reminderSent)
    {
        inspection.ReminderSent = reminderSent;
        return this;
    }
}