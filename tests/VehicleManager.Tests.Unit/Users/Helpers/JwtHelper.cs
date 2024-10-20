using System.IdentityModel.Tokens.Jwt;
using VehicleManager.Shared.Auth;

namespace VehicleManager.Tests.Unit.Users.Helpers;

public class JwtHelper
{
    public static JsonWebToken CreateToken(string userId, string role = null)
    {
        var jwt = new JwtSecurityToken();

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new JsonWebToken
        {
            AccessToken = token,
            Expires = DateTime.Now.AddHours(3),
            Id = userId,
            Role = role ?? string.Empty,
        };
    }
}