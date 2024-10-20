using VehicleManager.Core.Vehicles.Entities.Enums;

namespace VehicleManager.Core.Vehicles.Entities;

public sealed class Inspection
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ServiceBookId { get; set; }
    public ServiceBook ServiceBook { get; set; } = default!;
    public string Title { get; set; } = string.Empty;
    public DateTimeOffset ScheduledDate { get; set; }
    public DateTimeOffset? PerformDate { get; set; }
    public InspectionType InspectionType { get; set; }

    private Inspection()
    {
    }

    public static Inspection Create() => new();
}