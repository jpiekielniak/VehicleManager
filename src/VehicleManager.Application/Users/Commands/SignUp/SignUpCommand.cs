namespace VehicleManager.Application.Users.Commands.SignUp;

internal record SignUpCommand(
    string Email,
    string Password
) : IRequest;