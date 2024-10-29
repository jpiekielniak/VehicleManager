namespace VehicleManager.Application.Users.Commands.CompleteUserData;

internal record CompleteUserDataCommand(string FirstName, string LastName, string PhoneNumber) : IRequest
{
    internal Guid UserId { get; init; }
}