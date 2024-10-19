using CarManagement.Core.Vehicles.Entities.Enums;

namespace CarManagement.Application.Vehicles.Commands.CreateVehicle;

public record CreateVehicleCommand(
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