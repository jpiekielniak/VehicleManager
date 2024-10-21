namespace VehicleManager.Api.Endpoints.ServiceBooks.Queries.GetService;

internal class GetServiceRequest
{
    [FromRoute(Name = "serviceBookId")] public Guid ServiceBookId { get; init; }
    [FromRoute(Name = "serviceId")] public Guid ServiceId { get; init; }
}