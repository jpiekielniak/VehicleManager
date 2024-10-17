using CarManagement.Core.Vehicles.Entities.Enums;

namespace CarManagement.Tests.Unit.Vehicles.Entities.Builders;

public sealed class VehicleBuilderParams
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public double EngineCapacity { get; set; }
    public int EnginePower { get; set; }
    public string LicensePlate { get; set; }
    public string VIN { get; set; }
    public FuelType FuelType { get; set; }
    public GearboxType GearboxType { get; set; }
    public VehicleType VehicleType { get; set; }
}