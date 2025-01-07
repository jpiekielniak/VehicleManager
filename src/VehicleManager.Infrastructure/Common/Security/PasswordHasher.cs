using VehicleManager.Core.Common.Security;

namespace VehicleManager.Infrastructure.Common.Security;

internal sealed class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);

    public bool VerifyHashedPassword(string password, string hashedPassword)
        => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}