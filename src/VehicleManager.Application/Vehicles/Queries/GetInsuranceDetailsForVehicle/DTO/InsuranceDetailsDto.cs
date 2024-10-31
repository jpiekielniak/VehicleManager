namespace VehicleManager.Application.Vehicles.Queries.GetInsuranceDetailsForVehicle.DTO;

public record InsuranceDetailsDto(
    Guid Id,
    string Title,
    string Provider,
    string PolicyNumber,
    DateTimeOffset ValidFrom,
    DateTimeOffset ValidTo
);