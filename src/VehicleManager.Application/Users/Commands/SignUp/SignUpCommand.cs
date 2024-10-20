namespace VehicleManager.Application.Users.Commands.SignUp;

internal record SignUpCommand(
    string Email,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Password
) : IRequest;