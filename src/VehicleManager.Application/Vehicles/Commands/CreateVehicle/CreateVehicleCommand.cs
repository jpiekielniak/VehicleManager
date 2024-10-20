using VehicleManager.Core.Vehicles.Entities.Enums;

namespace VehicleManager.Application.Vehicles.Commands.CreateVehicle;

internal record CreateVehicleCommand(
    string Brand,
    string Model,
    int Year,
    string LicensePlate,
    string Vin,
    double EngineCapacity,
    int EnginePower,
    FuelType FuelType,
    GearboxType GearboxType,
    VehicleType VehicleType
) : IRequest<CreateVehicleResponse>;