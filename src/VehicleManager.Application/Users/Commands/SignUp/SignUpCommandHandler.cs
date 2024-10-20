using VehicleManager.Core.Users.Entities.Builders;
using VehicleManager.Core.Users.Exceptions.Roles;
using VehicleManager.Core.Users.Repositories;
using VehicleManager.Shared.Hash;

namespace VehicleManager.Application.Users.Commands.SignUp;

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
            .WithFirstName(command.FirstName)
            .WithLastName(command.LastName)
            .WithPassword(hashedPassword)
            .WithPhoneNumber(command.PhoneNumber)
            .WithRole(role)
            .Build();

        await userRepository.AddAsync(user, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);
    }
}