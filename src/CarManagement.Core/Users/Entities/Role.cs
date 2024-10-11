namespace CarManagement.Core.Users.Entities;

public sealed class Role
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public ICollection<User> Users { get; set; } = [];

    private Role()
    {
    }

    private Role(string name) => Name = name;

    public static Role Create(string name) => new(name);
}