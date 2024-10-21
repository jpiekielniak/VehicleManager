namespace VehicleManager.Core.Vehicles.Entities;

public sealed class ServiceBook
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = default!;
    public IEnumerable<Service> Services => _services;
    private readonly HashSet<Service> _services = [];
    public IEnumerable<Inspection> Inspections => _inspections;
    private readonly HashSet<Inspection> _inspections = [];

    private ServiceBook()
    {
    }

    public static ServiceBook Create() => new();

    public void AddInspection(Inspection inspection) => _inspections.Add(inspection);

    public void RemoveInspection(Inspection inspection) => _inspections.Remove(inspection);

    public void AddService(Service service) => _services.Add(service);

    public void RemoveService(Service service) => _services.Remove(service);
}