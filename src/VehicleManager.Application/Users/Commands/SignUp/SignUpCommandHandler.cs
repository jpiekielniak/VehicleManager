using VehicleManager.Application.Users.Commands.SignUp.Notifications;
using VehicleManager.Core.Common.Security;
using VehicleManager.Core.Users.Entities.Builders;
using VehicleManager.Core.Users.Entities.Enums;
using VehicleManager.Core.Users.Repositories;

namespace VehicleManager.Application.Users.Commands.SignUp;

internal sealed class SignUpCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IMediator mediator
) : IRequestHandler<SignUpCommand>
{
    public async Task Handle(SignUpCommand command,
        CancellationToken cancellationToken)
    {
        var hashedPassword = passwordHasher.HashPassword(command.Password);

        var user = new UserBuilder()
            .WithEmail(command.Email.ToLowerInvariant())
            .WithPassword(hashedPassword)
            .WithRole(Role.User)
            .Build();

        await userRepository.AddAsync(user, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);
        
        _ = mediator.Publish(new UserSignedUpNotification(user.Email), cancellationToken);
    }
}