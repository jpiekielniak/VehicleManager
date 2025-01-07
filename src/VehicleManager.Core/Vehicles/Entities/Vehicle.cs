using VehicleManager.Core.Users.Entities;
using VehicleManager.Core.Vehicles.Entities.Enums;

namespace VehicleManager.Core.Vehicles.Entities;

public sealed class Vehicle
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Brand { get; set; } = default!;
    public string Model { get; set; } = default!;
    public int Year { get; set; }
    public string LicensePlate { get; set; } = default!;
    public string VIN { get; set; } = default!;
    public int EngineCapacity { get; set; }
    public FuelType FuelType { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }

    public int EnginePower { get; set; }
    public GearboxType GearboxType { get; set; }
    public VehicleType VehicleType { get; set; }

    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

    public Guid ServiceBookId { get; set; }
    public ServiceBook ServiceBook { get; set; } = default!;
    public Guid ImageId { get; set; }
    public Image Image { get; set; } = default!;

    public IEnumerable<Insurance> Insurances => _insurances;
    private readonly HashSet<Insurance> _insurances = [];

    private Vehicle()
    {
    }

    public static Vehicle Create() => new();

    public void AddInsurance(Insurance insurance) => _insurances.Add(insurance);

    public void RemoveInsurance(Insurance insurance) => _insurances.Remove(insurance);
}