namespace VehicleManager.Core.Vehicles.Entities;

public sealed class Cost
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = default!;
    public decimal Amount { get; set; }
    public Guid ServiceId { get; set; }
    public Service Service { get; set; } = default!;

    private Cost()
    {
    }

    private Cost(string title, decimal amount, Guid serviceId)
    {
        Title = title;
        Amount = amount;
        ServiceId = serviceId;
    }

    public static Cost Create(string title, decimal amount, Guid serviceId) => new(title, amount, serviceId);
}