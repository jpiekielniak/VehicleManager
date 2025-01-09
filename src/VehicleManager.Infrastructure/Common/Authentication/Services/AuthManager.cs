using VehicleManager.Application.Common.Interfaces.Auth;
using VehicleManager.Application.Common.Models;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace VehicleManager.Infrastructure.Common.Authentication.Services;

public class AuthManager(IConfiguration configuration) : IAuthManager
{
    private readonly string _issuer = configuration.GetSection("Jwt:Issuer").Get<string>();
    private readonly string _key = configuration.GetSection("Jwt:Key").Get<string>();

    public JsonWebToken GenerateToken(Guid userId, string role)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Role, role)
        };

        var expires = DateTime.Now.AddHours(3);

        var jwt = new JwtSecurityToken(
            issuer: _issuer,
            audience: _issuer,
            claims: claims,
            expires: expires,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key!)),
                SecurityAlgorithms.HmacSha256)
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new JsonWebToken
        {
            AccessToken = token,
            Expires = expires,
            Id = userId.ToString(),
            Role = role
        };
    }

    public Task<string> GeneratePasswordResetTokenAsync(string email)
    {
        var claims = new[]
        {
            new Claim("jti", Guid.NewGuid().ToString()),
            new Claim("purpose", "password_reset"),
            new Claim("email", email)
        };

        var jwt = new JwtSecurityToken(
            issuer: _issuer,
            audience: _issuer,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)),
                SecurityAlgorithms.HmacSha256)
        );

        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(jwt));
    }
    
    public bool VerifyPasswordResetToken(string token, out string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)),
            ValidateIssuer = true,
            ValidIssuer = _issuer,
            ValidateAudience = true,
            ValidAudience = _issuer,
            ValidateLifetime = true
        };

        try
        {
            tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            email = jwtToken.Claims.FirstOrDefault(x => x.Type == "email")?.Value ?? string.Empty;

            return true;
        }
        catch
        {
            email = string.Empty;
            return false;
        }
    }
}