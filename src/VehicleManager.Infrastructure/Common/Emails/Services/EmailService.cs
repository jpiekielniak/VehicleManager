using VehicleManager.Application.Emails.Interfaces;
using VehicleManager.Infrastructure.Common.Emails.Exceptions;
using VehicleManager.Infrastructure.Common.Emails.Options;

namespace VehicleManager.Infrastructure.Common.Emails.Services;

public sealed class EmailService(IOptions<EmailOptions> emailOptions) : IEmailService
{
    private readonly EmailOptions _emailOptions = emailOptions.Value;
    private readonly IDictionary<string, string> _emailTemplates = LoadAllEmailTemplates();

    private const string InsuranceExpirationTemplateName = "InsuranceExpiration.html";
    private const string InsuranceExpirationSubject = "Wygasa termin ubezpieczenia";
    
    private const string InspectionExpirationTemplateName = "InspectionExpiration.html";
    private const string InspectionExpirationSubject = "Wygasa termin przeglądu";
    
    private const string GeneralNotificationTemplateName = "GeneralNotification.html";
    
    private const string ResetPasswordSubject = "Reset hasła";
    private const string ResetPasswordRequestTemplateName = "ResetPasswordRequest.html";
    
    private const string WelcomeEmailTemplateName = "WelcomeEmail.html";
    private const string WelcomeEmailSubject = "Witamy w Menadżerze pojazdów!";
    
    private const string UserDeletedSubject = "Konto zostało usunięte";
    private const string UserDeletedTemplateName = "UserDeleted.html";

    public async Task SendInsuranceExpirationEmailAsync(
        string email,
        string vehicleInfo,
        DateTimeOffset expirationDate,
        string policyNumber,
        string provider,
        CancellationToken cancellationToken)
    {
        var templateData = new Dictionary<string, string>
        {
            ["vehicleInfo"] = vehicleInfo,
            ["expirationDate"] = expirationDate.ToString("dd.MM.yyyy"),
            ["policyNumber"] = policyNumber,
            ["provider"] = provider
        };

        var message = await CreateEmailMessageAsync(
            email,
            InsuranceExpirationSubject,
            InsuranceExpirationTemplateName,
            templateData
        );

        await SendEmailAsync(message, cancellationToken);
    }

    public async Task SendEmailToUsersAsync(
        string title,
        string content,
        IEnumerable<string> emails,
        CancellationToken cancellationToken)
    {
        var templateData = new Dictionary<string, string>
        {
            ["title"] = title,
            ["content"] = content,
            ["currentYear"] = DateTime.Now.Year.ToString()
        };

        var sendTasks = emails.Select(async email =>
        {
            var message = await CreateEmailMessageAsync(
                email,
                title,
                GeneralNotificationTemplateName,
                templateData
            );

            await SendEmailAsync(message, cancellationToken);
        }).ToArray();

        await Task.WhenAll(sendTasks);
    }

    public async Task SendPasswordResetEmailAsync(string email, string token, CancellationToken cancellationToken)
    {
        var message = await CreatePasswordResetMessage(email, token);
        await SendEmailAsync(message, cancellationToken);
    }

    public async Task SendWelcomeEmailNotificationAsync(string email, CancellationToken cancellationToken)
    {
        var templateData = new Dictionary<string, string>
        {
            ["currentYear"] = DateTime.Now.Year.ToString()
        };

        var message = await CreateEmailMessageAsync(
            email,
            WelcomeEmailSubject,
            WelcomeEmailTemplateName,
            templateData
        );

        await SendEmailAsync(message, cancellationToken);
    }

    public async Task SendInspectionExpirationEmailAsync(string email, string vehicleInfo,
        DateTimeOffset? expirationDate,
        string inspectionType, CancellationToken cancellationToken)
    {
        var daysRemaining = (expirationDate?.Date - DateTime.UtcNow.Date)?.Days ?? 0;

        var templateData = new Dictionary<string, string>
        {
            ["vehicleInfo"] = vehicleInfo,
            ["expirationDate"] = expirationDate?.ToString("dd.MM.yyyy"),
            ["inspectionType"] = inspectionType,
            ["daysRemaining"] = daysRemaining.ToString(),
            ["currentYear"] = DateTime.Now.Year.ToString()
        };

        var message = await CreateEmailMessageAsync(
            email,
            InspectionExpirationSubject,
            InspectionExpirationTemplateName,
            templateData
        );

        await SendEmailAsync(message, cancellationToken);
    }

    public async Task SendUserDeletedEmailAsync(string email, CancellationToken cancellationToken)
    {
        var templateData = new Dictionary<string, string>
        {
            ["currentYear"] = DateTime.Now.Year.ToString(),
            ["email"] = email,
            ["deletedAt"] = DateTime.Now.ToString("dd.MM.yyyy HH:mm")
        };

        var message = await CreateEmailMessageAsync(
            email,
            UserDeletedSubject,
            UserDeletedTemplateName,
            templateData
        );

        await SendEmailAsync(message, cancellationToken);
    }


    private async Task<MailMessage> CreatePasswordResetMessage(string email, string token)
    {
        var resetLink = $"{_emailOptions.BaseUrl}/api/v1/users/reset-password/{Uri.EscapeDataString(token)}";
        var templateData = new Dictionary<string, string>
        {
            ["resetLink"] = resetLink,
            ["currentYear"] = DateTime.Now.Year.ToString()
        };

        var message = await CreateEmailMessageAsync(
            email,
            ResetPasswordSubject,
            ResetPasswordRequestTemplateName,
            templateData
        );

        return message;
    }

    private async Task SendEmailAsync(MailMessage message, CancellationToken cancellationToken)
    {
        try
        {
            using var smtpClient = CreateSmtpClient();
            await smtpClient.SendMailAsync(message, cancellationToken);
        }
        finally
        {
            message.Dispose();
        }
    }


    private async Task<MailMessage> CreateEmailMessageAsync(
        string recipientEmail,
        string subject,
        string templateName,
        IDictionary<string, string> templateData)
    {
        var emailBody = await BuildEmailBodyAsync(templateName, templateData);

        return new MailMessage
        {
            From = new MailAddress(_emailOptions.Username),
            Subject = subject,
            Body = emailBody,
            IsBodyHtml = true,
            To = { new MailAddress(recipientEmail) }
        };
    }

    private Task<string> BuildEmailBodyAsync(
        string templateName,
        IDictionary<string, string> replacements)
    {
        var template = _emailTemplates[templateName];

        return Task.FromResult(replacements.Aggregate(
            template,
            (current, replacement) =>
                current.Replace($"{{{{{replacement.Key}}}}}", replacement.Value)));
    }


    private SmtpClient CreateSmtpClient()
    {
        return new SmtpClient(_emailOptions.SmtpHost, _emailOptions.SmtpPort)
        {
            Credentials = new NetworkCredential(_emailOptions.Username, _emailOptions.Password),
            EnableSsl = true
        };
    }

    private static IDictionary<string, string> LoadAllEmailTemplates()
    {
        return new Dictionary<string, string>
        {
            [InsuranceExpirationTemplateName] = LoadEmailTemplate(InsuranceExpirationTemplateName),
            [GeneralNotificationTemplateName] = LoadEmailTemplate(GeneralNotificationTemplateName),
            [ResetPasswordRequestTemplateName] = LoadEmailTemplate(ResetPasswordRequestTemplateName),
            [WelcomeEmailTemplateName] = LoadEmailTemplate(WelcomeEmailTemplateName),
            [InspectionExpirationTemplateName] = LoadEmailTemplate(InspectionExpirationTemplateName),
            [UserDeletedTemplateName] = LoadEmailTemplate(UserDeletedTemplateName)
        };
    }

    private static string LoadEmailTemplate(string templateName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourcePath = assembly.GetManifestResourceNames()
                               .FirstOrDefault(x => x.EndsWith(templateName))
                           ?? throw new EmailTemplateNotFoundException(templateName);

        using var stream = assembly.GetManifestResourceStream(resourcePath)
                           ?? throw new EmailTemplateNotFoundException(templateName);

        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}