using VehicleManager.Application.Common.Interfaces.Auth;
using VehicleManager.Application.Emails.Interfaces;
using VehicleManager.Core.Users.Exceptions.Users;
using VehicleManager.Core.Users.Repositories;

namespace VehicleManager.Application.Users.Commands.RequestPasswordReset;

internal sealed class RequestPasswordResetCommandHandler(
    IUserRepository userRepository,
    IEmailService emailService,
    IAuthManager authManager
) : IRequestHandler<RequestPasswordResetCommand>
{
    public async Task Handle(RequestPasswordResetCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(command.Email, cancellationToken)
                   ?? throw new UserWithEmailNotFoundException(command.Email);
        
        var token = await authManager.GeneratePasswordResetTokenAsync(user.Email);
        await emailService.SendPasswordResetEmailAsync(user.Email, token, cancellationToken);
    }
}