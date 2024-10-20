using VehicleManager.Shared.Middlewares.Tokens.Exceptions;

namespace VehicleManager.Shared.Middlewares.Tokens;

public class TokenExpirationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var authorizationHeader = context.Request.Headers.Authorization.FirstOrDefault();

        if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
        {
            var token = authorizationHeader["Bearer ".Length..].Trim();

            var handler = new JwtSecurityTokenHandler();
            try
            {
                var jwtToken = handler.ReadJwtToken(token);

                var expirationDateUnix =
                    jwtToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp)?.Value;

                if (expirationDateUnix != null && long.TryParse(expirationDateUnix, out var expirationUnix))
                {
                    var expirationDate = DateTimeOffset.FromUnixTimeSeconds(expirationUnix).UtcDateTime;

                    if (expirationDate < DateTime.UtcNow)
                    {
                        throw new TokenExpiredException();
                    }
                }
            }
            catch (Exception)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new { message = "Invalid token" });
                return;
            }
        }

        await next(context);
    }
}