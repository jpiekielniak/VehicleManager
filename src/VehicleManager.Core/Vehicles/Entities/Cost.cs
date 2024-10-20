namespace VehicleManager.Core.Vehicles.Entities;

public sealed class Cost
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public decimal Amount { get; set; }
    public string Title { get; set; } = default!;
    public Guid ServiceId { get; set; }
    public Service Service { get; set; } = default!;

    private Cost()
    {
    }

    public static Cost Create() => new();
}