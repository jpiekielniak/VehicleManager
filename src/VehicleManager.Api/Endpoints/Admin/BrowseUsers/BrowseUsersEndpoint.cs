using VehicleManager.Api.Common.Endpoints;
using VehicleManager.Application.Admin.Queries.BrowseUsers;
using VehicleManager.Core.Users.Entities.Enums;
using VehicleManager.Infrastructure.Common.Middlewares.Exceptions.Models;

namespace VehicleManager.Api.Endpoints.Admin.BrowseUsers;

internal sealed class BrowseUsersEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet(AdminEndpoints.BrowseUsers, async (
                [AsParameters] SieveModel sieveModel,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var query = new BrowseUsersQuery(sieveModel);
                var result = await mediator.Send(query, cancellationToken);
                return result;
            })
            .RequireAuthorization(options =>
                options.RequireRole(Role.Admin.ToString())
            )
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint allows admin to browse users in system"
            })
            .WithTags(AdminEndpoints.Admin)
            .Produces(StatusCodes.Status200OK)
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden);
    }
}