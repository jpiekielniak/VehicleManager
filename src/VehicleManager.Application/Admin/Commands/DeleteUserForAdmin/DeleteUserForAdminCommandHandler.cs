using VehicleManager.Core.Users.Exceptions.Users;
using VehicleManager.Core.Users.Repositories;

namespace VehicleManager.Application.Admin.Commands.DeleteUserForAdmin;

internal sealed class DeleteUserForAdminCommandHandler(
    IUserRepository userRepository
) : IRequestHandler<DeleteUserForAdminCommand>
{
    public async Task Handle(DeleteUserForAdminCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetAsync(command.UserId, cancellationToken)
                   ?? throw new UserNotFoundException(command.UserId);

        await userRepository.DeleteAsync(user, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);
    }
}