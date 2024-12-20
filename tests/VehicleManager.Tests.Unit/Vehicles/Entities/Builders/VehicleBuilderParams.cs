using VehicleManager.Core.Vehicles.Entities.Enums;

namespace VehicleManager.Tests.Unit.Vehicles.Entities.Builders;

public sealed class VehicleBuilderParams
{
    public string Brand { get; init; }
    public string Model { get; init; }
    public int Year { get; init; }
    public int EngineCapacity { get; init; }
    public int EnginePower { get; init; }
    public string LicensePlate { get; init; }
    public string VIN { get; init; }
    public FuelType FuelType { get; init; }
    public GearboxType GearboxType { get; init; }
    public VehicleType VehicleType { get; init; }
}