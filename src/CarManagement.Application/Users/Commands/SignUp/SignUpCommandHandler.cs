using CarManagement.Core.Users.Entities.Builders;
using CarManagement.Core.Users.Exceptions.Roles;
using CarManagement.Core.Users.Repositories;
using CarManagement.Shared.Hash;

namespace CarManagement.Application.Users.Commands.SignUp;

internal sealed class SignUpCommandHandler(
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    IPasswordHasher passwordHasher
) : IRequestHandler<SignUpCommand>
{
    private const string UserRole = "User";

    public async Task Handle(SignUpCommand command,
        CancellationToken cancellationToken)
    {
        var role = await roleRepository
            .GetAsync(UserRole, cancellationToken);

        if (role is null)
        {
            throw new RoleNotFoundException(UserRole);
        }

        var hashedPassword = passwordHasher.HashPassword(command.Password);

        var user = new UserBuilder()
            .WithEmail(command.Email)
            .WithUsername(command.Username)
            .WithPassword(hashedPassword)
            .WithRole(role)
            .Build();

        await userRepository.AddAsync(user, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);
    }
}