using VehicleManager.Core.Users.Exceptions.Users;
using VehicleManager.Core.Users.Repositories;
using VehicleManager.Shared.Auth;
using VehicleManager.Shared.Hash;

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
        var user = await userRepository.GetByEmailAsync(command.Email, cancellationToken);

        if (user is null)
        {
            throw new InvalidCredentialsException();
        }

        var isValidPassword = passwordHasher.VerifyHashedPassword(command.Password, user.Password);

        if (!isValidPassword)
        {
            throw new InvalidCredentialsException();
        }

        var jwt = authManager.GenerateToken(user.Id, user.Role.Name);

        return new SignInResponse(jwt.AccessToken, jwt.Expires);
    }
}