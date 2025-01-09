using VehicleManager.Core.Common.Security;
using VehicleManager.Core.Users.Entities.Builders;
using VehicleManager.Core.Users.Exceptions.Users;
using VehicleManager.Core.Users.Repositories;

namespace VehicleManager.Application.Users.Commands.ResetPassword;

internal sealed class ResetPasswordCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher
) : IRequestHandler<ResetPasswordCommand>
{
    public async Task Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(command.Email, cancellationToken)
                   ?? throw new UserWithEmailNotFoundException(command.Email);

        var hashedPassword = passwordHasher.HashPassword(command.NewPassword);

        var updatedUser = new UserBuilder(user)
            .WithPassword(hashedPassword)
            .Build();

        await userRepository.UpdateAsync(updatedUser, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);
    }
}