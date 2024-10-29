using VehicleManager.Application.Users.Commands.CompleteUserData;

namespace VehicleManager.Api.Endpoints.Users.Commands.CompleteUserData;

internal class CompleteUserDataRequest
{
    [FromRoute(Name = "userId")] public Guid UserId { get; init; }
    [FromBody] public CompleteUserDataCommand Command { get; init; } = default!;
}