using CarManagement.Core.Users.Entities;

namespace CarManagement.Tests.Unit.Users.Entities.Builders;

public sealed class UserBuilderParams
{
    public string Username { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public string Password { get; init; }
    public Role Role { get; init; }
}