using CarManagement.Shared.Auth.Policies;
using static CarManagement.Shared.Auth.AvailableRole;

namespace CarManagement.Shared.Auth;

public static class Extensions
{
    public static IServiceCollection AddPolicy(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy(AuthorizationPolicies.UserPolicy,
                policy => policy.RequireRole(nameof(User), nameof(Admin)))
            .AddPolicy(AuthorizationPolicies.AdminPolicy,
                policy => policy.RequireRole(nameof(Admin)));

        return services;
    }
    
}