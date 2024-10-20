using VehicleManager.Core.Vehicles.Entities.Enums;

namespace VehicleManager.Application.Vehicles.Commands.ChangeVehicleInformation;

internal record ChangeVehicleInformationCommand(
    string Brand,
    string Model,
    int Year,
    string LicensePlate,
    string Vin,
    double EngineCapacity,
    FuelType FuelType,
    int EnginePower,
    GearboxType GearboxType,
    VehicleType VehicleType
) : IRequest
{
    internal Guid VehicleId { get; init; }
}