using VehicleManager.Api.Common.Endpoints;
using VehicleManager.Application.ServiceBooks.Commands.DeleteInspection;
using VehicleManager.Infrastructure.Common.Middlewares.Exceptions.Models;

namespace VehicleManager.Api.Endpoints.ServiceBooks.Commands.DeleteInspection;

internal sealed class DeleteInspectionRequestEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapDelete(ServiceBookEndpoints.Inspection, async (
                [AsParameters] DeleteInspectionRequest request,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var command = new DeleteInspectionCommand(request.ServiceBookId, request.InspectionId);
                await mediator.Send(command, cancellationToken);
                return Results.NoContent();
            })
            .RequireAuthorization()
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint is used to delete an inspection from a service book"
            })
            .WithTags(ServiceBookEndpoints.ServiceBooks)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}