using VehicleManager.Application.Common.Interfaces.Auth;
using VehicleManager.Application.Users.Queries.GetResetPasswordForm;
using VehicleManager.Core.Users.Exceptions.Users;
using VehicleManager.Core.Users.Repositories;
using VehicleManager.Infrastructure.EF.Users.Queries.GetResetPasswordForm.Exceptions;

namespace VehicleManager.Infrastructure.EF.Users.Queries.GetResetPasswordForm;

internal sealed class GetResetPasswordFormQueryHandler(
    IAuthManager authManager,
    IUserRepository userRepository
) : IRequestHandler<GetResetPasswordFormQuery, string>
{
    private const string ResetPasswordFormResourceName = "ResetPasswordForm.html";

    public async Task<string> Handle(GetResetPasswordFormQuery query, CancellationToken cancellationToken)
    {
        if (!authManager.VerifyPasswordResetToken(query.Token, out var email))
        {
            throw new InvalidPasswordResetTokenException();
        }

        var user = await userRepository.GetByEmailAsync(email, cancellationToken)
                   ?? throw new UserWithEmailNotFoundException(email);
        
        var formHtml = await LoadFormTemplate(ResetPasswordFormResourceName);

        formHtml = formHtml.Replace("{token}", query.Token);
        formHtml = formHtml.Replace("{email}", user.Email);

        return formHtml;
    }

    private static async Task<string> LoadFormTemplate(string resourceName)
    {
        var assembly = typeof(GetResetPasswordFormQueryHandler).Assembly;
        var resource = assembly.GetManifestResourceNames()
                           .FirstOrDefault(x => x.EndsWith(resourceName))
                       ?? throw new CouldNotFindEmbeddedResourceException(resourceName);

        await using var stream = assembly.GetManifestResourceStream(resource)
                                 ?? throw new CouldNotFindEmbeddedResourceException(resource);

        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync();
    }
}