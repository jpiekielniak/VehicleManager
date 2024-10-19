using CarManagement.Core.Vehicles.Entities.Enums;

namespace CarManagement.Application.Vehicles.Commands.ChangeVehicleInformation;

public record ChangeVehicleInformationCommand(
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