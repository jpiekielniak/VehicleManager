using VehicleManager.Application.Vehicles.Queries.BrowseCurrentLoggedUserVehicles.DTO;
using VehicleManager.Application.Vehicles.Queries.GetVehicle.DTO;
using VehicleManager.Core.Common.Helpers;
using VehicleManager.Core.Vehicles.Entities;

namespace VehicleManager.Infrastructure.EF.Vehicles.Queries;

public static class VehicleQueryExtensions
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
            vehicle.FuelType.GetDisplay(),
            vehicle.EnginePower,
            vehicle.GearboxType.GetDisplay(),
            vehicle.VehicleType.GetDisplay(),
            vehicle.CreatedAt,
            vehicle.ServiceBookId,
            vehicle.Image?.BlobUrl
        );

    public static VehicleDto AsDto(this Vehicle vehicle)
        => new(
            vehicle.Id,
            vehicle.Brand,
            vehicle.Model,
            vehicle.LicensePlate,
            vehicle.Image?.BlobUrl
        );
}