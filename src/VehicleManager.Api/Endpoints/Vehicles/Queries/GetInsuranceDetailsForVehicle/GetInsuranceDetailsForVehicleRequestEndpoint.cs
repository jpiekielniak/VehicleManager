using VehicleManager.Application.Vehicles.Queries.GetInsuranceDetailsForVehicle;
using VehicleManager.Application.Vehicles.Queries.GetInsuranceDetailsForVehicle.DTO;
using VehicleManager.Shared.Endpoints;

namespace VehicleManager.Api.Endpoints.Vehicles.Queries.GetInsuranceDetailsForVehicle;

internal sealed class GetInsuranceDetailsForVehicleRequestEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet(VehicleEndpoints.InsuranceById, async (
                [AsParameters] GetInsuranceDetailsForVehicleRequest request,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var query = new GetInsuranceDetailsForVehicleQuery(request.VehicleId, request.InsuranceId);
                return await mediator.Send(query, cancellationToken);
            })
            .RequireAuthorization()
            .WithTags(VehicleEndpoints.Vehicles)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint allow user to get insurance details for vehicle"
            })
            .Produces<InsuranceDetailsDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}