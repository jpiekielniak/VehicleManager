
namespace VehicleManager.Shared.Auth;

public interface IAuthManager
{
    JsonWebToken GenerateToken(Guid userId, string role);
}