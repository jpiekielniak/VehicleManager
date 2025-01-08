using VehicleManager.Api.Common.Endpoints;
using VehicleManager.Application.Admin.Commands.SendEmailToUsers;
using VehicleManager.Infrastructure.Common.Authentication.Policies;
using VehicleManager.Infrastructure.Common.Middlewares.Exceptions.Models;

namespace VehicleManager.Api.Endpoints.Admin.SendEmailToUsers;

internal sealed class SendEmailToUsersEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost(AdminEndpoints.SendEmail, async (
                [FromBody] SendEmailToUsersCommand command,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                await mediator.Send(command, cancellationToken);
                return Results.Ok();
            })
            .RequireAuthorization(AuthorizationPolicies.RequireAdmin)
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