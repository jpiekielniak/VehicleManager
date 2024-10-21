namespace VehicleManager.Api.Endpoints.ServiceBooks.Queries.GetInspection;

public class GetInspectionRequest
{
    [FromRoute(Name = "serviceBookId")] public Guid ServiceBookId { get; init; }
    [FromRoute(Name = "inspectionId")] public Guid InspectionId { get; init; }
}