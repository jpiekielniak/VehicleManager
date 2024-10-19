namespace CarManagement.Application.Vehicles.Queries.BrowseCurrentLoggedUserVehicles.DTO;

public record VehicleDto(Guid VehicleId, string Brand, string Model, string LicensePlate);