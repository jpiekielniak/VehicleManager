using VehicleManager.Core.Users.Entities;

namespace VehicleManager.Tests.Unit.Users.Entities.Builders;

public sealed class UserBuilderParams
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public string Password { get; init; }
    public Role Role { get; init; }
}