namespace CarManagement.Shared;

public static class Extensions
{
    public static IServiceCollection AddShared(this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtIssuer = configuration.GetSection("Jwt:Issuer").Get<string>();
        var jwtKey = configuration.GetSection("Jwt:Key").Get<string>();

        services
            .AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    ValidIssuer = jwtIssuer!,
                    ValidAudience = jwtIssuer!,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!))
                };
            });


        return services;
    }
}