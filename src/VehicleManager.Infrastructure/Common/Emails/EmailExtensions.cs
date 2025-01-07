using VehicleManager.Application.Emails.Interfaces;
using VehicleManager.Infrastructure.Common.Emails.Options;
using VehicleManager.Infrastructure.Common.Emails.Services;

namespace VehicleManager.Infrastructure.Common.Emails;

public static class EmailExtensions
{
    public static IServiceCollection AddEmail(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailOptions>(
            configuration.GetSection(EmailOptions.SectionName));

        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}