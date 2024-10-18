using CarManagement.Application.Vehicles.Queries.GetVehicle.DTO;
using CarManagement.Core.Vehicles.Entities;
using CarManagement.Shared.Enums;

namespace CarManagement.Infrastructure.EF.Vehicles.Queries;

public static class Extensions
{
    public static VehicleDetailsDto AsDetailsDto(this Vehicle vehicle)
        => new(
            vehicle.Id,
            vehicle.Brand,
            vehicle.Model,
            vehicle.Year,
            vehicle.LicensePlate,
            vehicle.VIN,
            vehicle.EngineCapacity,
            vehicle.FuelType.ToString(),
            vehicle.EnginePower,
            vehicle.GearboxType.GetDescription(),
            vehicle.VehicleType.GetDescription(),
            vehicle.CreatedAt,
            vehicle.ServiceBookId
        );
}