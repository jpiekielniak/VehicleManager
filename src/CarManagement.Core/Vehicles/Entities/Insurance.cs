namespace CarManagement.Core.Vehicles.Entities;

public sealed class Insurance
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = default!;
    public string Provider { get; set; } = default!;
    public string PolicyNumber { get; set; } = default!;
    public DateTimeOffset ValidFrom { get; set; }
    public DateTimeOffset ValidTo { get; set; }

    private Insurance()
    {
    }

    public static Insurance Create() => new();
}