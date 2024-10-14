using CarManagement.Core.Users.Exceptions.Users;
using CarManagement.Core.Users.Repositories;
using CarManagement.Shared.Auth;
using CarManagement.Shared.Hash;

namespace CarManagement.Application.Users.Commands.SignIn;

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
            throw new UserNotFoundException(command.Email);
        }

        var isValidPassword = passwordHasher.VerifyHashedPassword(command.Password, user.Password);

        if (!isValidPassword)
        {
            throw new InvalidPasswordException();
        }

        var jwt = authManager.GenerateToken(user.Id, user.Role.Name);

        return new SignInResponse(jwt.AccessToken, jwt.Expires);
    }
}