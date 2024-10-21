namespace VehicleManager.Core.Vehicles.Entities;

public sealed class Service
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset Date { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; }
    public Guid ServiceBookId { get; set; }
    public ServiceBook ServiceBook { get; set; } = default!;

    public IEnumerable<Cost> Costs => _costs;
    private readonly HashSet<Cost> _costs = [];

    private Service()
    {
    }

    public static Service Create() => new();

    public void AddCost(Cost cost) => _costs.Add(cost);
    public void AddRangeCosts(List<Cost> costs) => costs.ForEach(AddCost);
}