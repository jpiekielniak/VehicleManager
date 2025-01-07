using VehicleManager.Application.Common.Models;

namespace VehicleManager.Application.Common.Interfaces.Auth;

public interface IAuthManager
{
    JsonWebToken GenerateToken(Guid userId, string role);
}