using VehicleManager.Application.Common.Interfaces.Context;
using VehicleManager.Application.Users.Commands.DeleteUser.Notifications;
using VehicleManager.Core.Users.Exceptions.Users;
using VehicleManager.Core.Users.Repositories;

namespace VehicleManager.Application.Users.Commands.DeleteUser;

internal sealed class DeleteUserCommandHandler(
    IContext context,
    IUserRepository userRepository,
    IMediator mediator
) : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetAsync(context.Id, cancellationToken)
                   ?? throw new UserNotFoundException(context.Id);

        await userRepository.DeleteAsync(user, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);

        _ = mediator.Publish(new UserDeletedNotification(user.Email), cancellationToken);
    }
}