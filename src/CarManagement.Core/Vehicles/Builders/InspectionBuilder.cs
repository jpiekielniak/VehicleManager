using CarManagement.Core.Vehicles.Entities;
using CarManagement.Core.Vehicles.Entities.Enums;

namespace CarManagement.Core.Vehicles.Builders;

public class InspectionBuilder
{
    private readonly Inspection _inspection = Inspection.Create();

    public Inspection Build() => _inspection;

    public InspectionBuilder WithScheduledDate(DateTimeOffset scheduledDate)
    {
        ArgumentNullException.ThrowIfNull(scheduledDate);
        _inspection.ScheduledDate = scheduledDate;
        return this;
    }

    public InspectionBuilder WithPerformDate(DateTimeOffset? performDate)
    {
        _inspection.PerformDate = performDate;
        return this;
    }

    public InspectionBuilder WithInspectionType(InspectionType inspectionType)
    {
        if (!Enum.IsDefined(typeof(InspectionType), inspectionType))
        {
            throw new InvalidEnumArgumentException("InspectionType is invalid");
        }

        _inspection.InspectionType = inspectionType;
        return this;
    }

    public InspectionBuilder WithServiceBook(ServiceBook serviceBook)
    {
        _inspection.ServiceBook = serviceBook;
        _inspection.ServiceBookId = serviceBook.Id;
        return this;
    }
}