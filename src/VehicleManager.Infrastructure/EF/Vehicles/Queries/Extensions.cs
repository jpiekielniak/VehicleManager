using VehicleManager.Application.Vehicles.Queries.BrowseCurrentLoggedUserVehicles.DTO;
using VehicleManager.Application.Vehicles.Queries.GetVehicle.DTO;
using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Shared.Enums;

namespace VehicleManager.Infrastructure.EF.Vehicles.Queries;

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
            vehicle.GearboxType.GetDisplay(),
            vehicle.VehicleType.GetDisplay(),
            vehicle.CreatedAt,
            vehicle.ServiceBookId
        );

    public static VehicleDto AsDto(this Vehicle vehicle)
        => new(
            vehicle.Id,
            vehicle.Brand,
            vehicle.Model,
            vehicle.LicensePlate
        );
}