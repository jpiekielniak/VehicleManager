using VehicleManager.Application.Common.Interfaces.Auth;
using VehicleManager.Core.Common.Security;
using VehicleManager.Core.Users.Exceptions.Users;
using VehicleManager.Core.Users.Repositories;

namespace VehicleManager.Application.Users.Commands.SignIn;

internal sealed class SignInCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IAuthManager authManager
) : IRequestHandler<SignInCommand, SignInResponse>
{
    public async Task<SignInResponse> Handle(SignInCommand command,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(command.Email, cancellationToken)
                   ?? throw new InvalidCredentialsException();

        var isValidPassword = passwordHasher.VerifyHashedPassword(command.Password, user.Password);

        if (!isValidPassword)
        {
            throw new InvalidCredentialsException();
        }

        var jwt = authManager.GenerateToken(user.Id, user.Role.ToString());

        return new SignInResponse(jwt.AccessToken, jwt.Expires);
    }
}