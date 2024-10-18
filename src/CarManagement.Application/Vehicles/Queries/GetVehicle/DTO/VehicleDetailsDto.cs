
namespace CarManagement.Application.Vehicles.Queries.GetVehicle.DTO;

public record VehicleDetailsDto(
    Guid Id,
    string Brand,
    string Model,
    int Year,
    string LicensePlate,
    string VIN,
    double EngineCapacity,
    string FuelType,
    int EnginePower,
    string GearboxType,
    string VehicleType,
    DateTimeOffset CreatedAt,
    Guid ServiceBookId
);