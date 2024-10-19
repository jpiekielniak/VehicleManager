using CarManagement.Application.ServiceBooks.AddInspection;
using CarManagement.Shared.Endpoints;
using CarManagement.Shared.Middlewares.Exceptions;

namespace CarManagement.Api.Endpoints.ServiceBooks.Commands.AddInspection;

internal sealed class AddInspectionEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost(ServiceBookEndpoints.Inspections, async (
                    [AsParameters] AddInspectionRequest request,
                    [FromServices] IMediator mediator,
                    CancellationToken cancellationToken) =>
                {
                    var command = request.Command with { ServiceBookId = request.ServiceBookId };
                    var response = await mediator.Send(command, cancellationToken);
                    return Results.Created($"{ServiceBookEndpoints.Inspections}/{response.InspectionId}", response);
                }
            ).RequireAuthorization()
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint is used to add a new inspection to a service book",
            })
            .WithTags(ServiceBookEndpoints.ServiceBooks)
            .Produces<AddInspectionResponse>(StatusCodes.Status201Created)
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}