namespace VehicleManager.Core.Vehicles.Entities;

public sealed class Insurance
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = default!;
    public string Title { get; set; }
    public string Provider { get; set; } = default!;
    public string PolicyNumber { get; set; } = default!;
    public DateTimeOffset ValidFrom { get; set; }
    public DateTimeOffset ValidTo { get; set; }
    public bool ReminderSent { get; set; } = false;

    private Insurance()
    {
    }

    public static Insurance Create() => new();
    
    public void setReminderSent(bool reminderSent)
    {
        ReminderSent = reminderSent;
    }
}