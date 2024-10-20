namespace VehicleManager.Core.Vehicles.Entities;

public sealed class Service
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset Date { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Guid ServiceBookId { get; set; }
    public ServiceBook ServiceBook { get; set; } = default!;
    public List<Cost> Costs { get; set; } = [];

    private Service()
    {
    }

    public static Service Create() => new();
}