
namespace VehicleManager.Api.Configuration.Swagger;

internal static class SwaggerConfiguration
{
    private const string Version = "v1";
    private const string Title = "VehicleManager API";
    private const string AuthScheme = "Bearer";
    private const string JwtFormat = "JWT";
    private const string AuthHeader = "Authorization";
    private const string AuthDescription = "Please insert JWT with Bearer into field";

    internal static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(swagger =>
        {
            swagger.CustomSchemaIds(x => x.FullName?.Replace("+", "."));
            swagger.SwaggerDoc(Version, new OpenApiInfo
            {
                Title = Title,
                Version = Version
            });

            AddSecurity(swagger);
        });

        return services;
    }

    private static void AddSecurity(SwaggerGenOptions swagger)
    {
        swagger.AddSecurityDefinition(AuthScheme, new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = AuthDescription,
            Name = AuthHeader,
            Type = SecuritySchemeType.Http,
            BearerFormat = JwtFormat,
            Scheme = AuthScheme
        });

        swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = AuthScheme
                    }
                },
                Array.Empty<string>()
            }
        });
    }
}