using VehicleManager.Application.Common.Interfaces.Context;
using VehicleManager.Core.Users.Exceptions.Users;
using VehicleManager.Core.Users.Repositories;

namespace VehicleManager.Application.Users.Commands.DeleteUser;

internal sealed class DeleteUserCommandHandler(
    IContext context,
    IUserRepository userRepository
) : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetAsync(command.UserId, cancellationToken)
                   ?? throw new UserNotFoundException(command.UserId);

        if (user.Id != context.Id)
        {
            throw new ActionNotAllowedException();
        }

        await userRepository.DeleteAsync(user, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);
    }
}