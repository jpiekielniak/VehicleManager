namespace VehicleManager.Application.Vehicles.Commands.AddInsurance;

internal record AddInsuranceCommand(
    string Title,
    string Provider,
    string PolicyNumber,
    DateTimeOffset ValidFrom,
    DateTimeOffset ValidTo) : IRequest<AddInsuranceResponse>
{
    internal Guid VehicleId { get; init; }
}