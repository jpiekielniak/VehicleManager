using VehicleManager.Core.Users.Entities.Enums;
using VehicleManager.Core.Vehicles.Entities;

namespace VehicleManager.Core.Users.Entities;

public sealed class User
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public Role Role { get; set; }
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
    public IEnumerable<Vehicle> Vehicles => _vehicles;
    private readonly HashSet<Vehicle> _vehicles = [];

    private User()
    {
    }

    public static User Create() => new();
}