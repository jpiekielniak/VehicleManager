using VehicleManager.Core.Users.Entities.Builders;
using VehicleManager.Core.Users.Exceptions.Users;
using VehicleManager.Core.Users.Repositories;
using VehicleManager.Shared.Auth.Context;

namespace VehicleManager.Application.Users.Commands.CompleteUserData;

internal sealed class CompleteUserDataCommandHandler(
    IContext context,
    IUserRepository userRepository
) : IRequestHandler<CompleteUserDataCommand>
{
    public async Task Handle(CompleteUserDataCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetAsync(command.UserId, cancellationToken)
                   ?? throw new UserNotFoundException(command.UserId);

        if (user.Id != context.Id)
        {
            throw new ActionNotAllowedException();
        }

        new UserBuilder(user)
            .WithFirstName(command.FirstName)
            .WithLastName(command.LastName)
            .WithPhoneNumber(command.PhoneNumber)
            .Build();

        await userRepository.SaveChangesAsync(cancellationToken);
    }
}