namespace VehicleManager.Api.Endpoints.ServiceBooks.Commands.DeleteService;

public class DeleteServiceRequest
{
    [FromRoute(Name = "serviceBookId")] public Guid ServiceBookId { get; init; }
    [FromRoute(Name = "serviceId")] public Guid ServiceId { get; init; }
}