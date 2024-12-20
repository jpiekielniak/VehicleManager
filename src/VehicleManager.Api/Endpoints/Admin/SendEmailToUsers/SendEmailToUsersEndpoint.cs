using VehicleManager.Shared.Endpoints;
using VehicleManager.Shared.Middlewares.Exceptions;

namespace VehicleManager.Api.Endpoints.Admin.SendEmailToUsers;

internal sealed class SendEmailToUsersEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost(AdminEndpoints.SendEmail, async (
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                //TODO: Implementation
            })
            .RequireAuthorization()
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint allows admin send email to user in system"
            })
            .WithTags(AdminEndpoints.Admin)
            .Produces(StatusCodes.Status200OK)
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden);
    }
}