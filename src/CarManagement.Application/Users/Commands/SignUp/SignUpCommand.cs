namespace CarManagement.Application.Users.Commands.SignUp;

internal record SignUpCommand(
    string Email,
    string Username,
    string PhoneNumber,
    string Password,
    string ConfirmPassword
) : IRequest;