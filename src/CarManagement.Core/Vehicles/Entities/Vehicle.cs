using CarManagement.Core.Users.Entities;
using CarManagement.Core.Vehicles.Entities.Enums;
using DriveType = CarManagement.Core.Vehicles.Entities.Enums.DriveType;

namespace CarManagement.Core.Vehicles.Entities;

public abstract class Vehicle
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Brand { get; set; } = default!;
    public string Model { get; set; } = default!;
    public int Year { get; set; }
    public string LicensePlate { get; set; } = default!;
    public string VIN { get; set; } = default!;
    public double EngineCapacity { get; set; }
    public FuelType FuelType { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }

    public int EnginePower { get; set; }

    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
}