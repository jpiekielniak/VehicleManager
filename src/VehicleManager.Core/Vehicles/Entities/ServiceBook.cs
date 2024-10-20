namespace VehicleManager.Core.Vehicles.Entities;

public sealed class ServiceBook
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = default!;
    public List<Service> Services { get; set; } = [];
    public IEnumerable<Inspection> Inspections => _inspections;
    private readonly HashSet<Inspection> _inspections = [];

    private ServiceBook()
    {
    }

    public static ServiceBook Create() => new();

    public void AddInspection(Inspection inspection) => _inspections.Add(inspection);

    public void RemoveInspection(Inspection inspection) => _inspections.Remove(inspection);
}