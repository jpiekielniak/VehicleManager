namespace VehicleManager.Application.Users.Commands.RequestPasswordReset;

internal record RequestPasswordResetCommand(string Email) : IRequest;
