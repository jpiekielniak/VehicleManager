namespace VehicleManager.Api.Endpoints.ServiceBooks.Commands.DeleteInspection;

public class DeleteInspectionRequest
{
    [FromRoute(Name = "serviceBookId")] public Guid ServiceBookId { get; init; }
    [FromRoute(Name = "inspectionId")] public Guid InspectionId { get; init; }
}