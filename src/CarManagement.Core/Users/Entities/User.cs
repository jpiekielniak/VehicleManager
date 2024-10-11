namespace CarManagement.Core.Users.Entities;

public sealed class User
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public Guid RoleId { get; init; }
    public Role Role { get; set; }
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

    private User()
    {
    }

    public static User Create() => new();
}