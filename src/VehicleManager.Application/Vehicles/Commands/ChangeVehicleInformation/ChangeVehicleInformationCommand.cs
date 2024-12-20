using VehicleManager.Core.Vehicles.Entities.Enums;

namespace VehicleManager.Application.Vehicles.Commands.ChangeVehicleInformation;

internal record ChangeVehicleInformationCommand(
    string Brand,
    string Model,
    int Year,
    string LicensePlate,
    string Vin,
    int EngineCapacity,
    int EnginePower,
    FuelType FuelType,
    GearboxType GearboxType,
    VehicleType VehicleType
) : IRequest
{
    internal Guid VehicleId { get; init; }
}