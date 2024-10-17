namespace CarManagement.Core.Vehicles.Entities;

public sealed class ServiceBook
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = default!;
    public List<Service> Services { get; set; } = [];
    public List<Inspection> Inspections { get; set; } = [];

    private ServiceBook()
    {
    }

    public static ServiceBook Create() => new();
}