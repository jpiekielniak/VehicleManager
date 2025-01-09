using VehicleManager.Api.Common.Endpoints;
using VehicleManager.Application.Users.Queries.GetResetPasswordForm;

namespace VehicleManager.Api.Endpoints.Users.Queries.GetResetPasswordForm;

internal sealed class GetResetPasswordFormEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet(UserEndpoints.ResetPassword, async (
                [FromRoute] string token,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var query = new GetResetPasswordFormQuery(token);
                var formHtml = await mediator.Send(query, cancellationToken);
                return Results.Content(formHtml, "text/html");
            })
            .WithTags(UserEndpoints.Users)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint return reset password form",
            });
    }
}