using VehicleManager.Core.Users.Entities.Builders;
using VehicleManager.Core.Users.Entities.Enums;
using VehicleManager.Core.Users.Repositories;
using VehicleManager.Shared.Hash;

namespace VehicleManager.Application.Users.Commands.SignUp;

internal sealed class SignUpCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher
) : IRequestHandler<SignUpCommand>
{
    public async Task Handle(SignUpCommand command,
        CancellationToken cancellationToken)
    {
        var hashedPassword = passwordHasher.HashPassword(command.Password);

        var user = new UserBuilder()
            .WithEmail(command.Email.ToLowerInvariant())
            .WithFirstName(command.FirstName)
            .WithLastName(command.LastName)
            .WithPassword(hashedPassword)
            .WithPhoneNumber(command.PhoneNumber)
            .WithRole(Role.User)
            .Build();

        await userRepository.AddAsync(user, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);
    }
}