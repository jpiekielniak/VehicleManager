using VehicleManager.Core.Users.Entities.Enums;

namespace VehicleManager.Infrastructure.Common.Authentication.Policies;

public static class AuthorizationPolicies
{
    public const string RequireAdmin = nameof(RequireAdmin);

    public static AuthorizationOptions AddAuthorizationPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(RequireAdmin, policy =>
            policy.RequireRole(nameof(Role.Admin))
        );

        return options;
    }
}