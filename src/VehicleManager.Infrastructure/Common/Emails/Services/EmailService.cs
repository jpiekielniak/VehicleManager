using System.Net.Mail;
using VehicleManager.Application.Emails.Interfaces;
using VehicleManager.Infrastructure.Common.Emails.Options;

namespace VehicleManager.Infrastructure.Common.Emails.Services;

public sealed class EmailService : IEmailService, IDisposable
{
    private readonly EmailOptions _emailOptions;
    private readonly IDictionary<string, string> _emailTemplates;
    private readonly SmtpClient _smtpClient;

    private const string InsuranceExpirationTemplateName = "InsuranceExpiration.html";
    private const string InsuranceExpirationSubject = "Wygasa termin ubezpieczenia";

    public EmailService(
        IOptions<EmailOptions> emailOptions)
    {
        _emailOptions = emailOptions.Value;
        _smtpClient = CreateSmtpClient();
        _emailTemplates = new Dictionary<string, string>
        {
            [InsuranceExpirationTemplateName] = LoadEmailTemplate(InsuranceExpirationTemplateName)
        };
    }

    public async Task SendInsuranceExpirationEmailAsync(
        string email,
        string vehicleInfo,
        DateTimeOffset expirationDate,
        string policyNumber,
        string provider,
        CancellationToken cancellationToken)
    {
        var message = CreateInsuranceExpirationMessage(
            email,
            vehicleInfo,
            expirationDate,
            policyNumber,
            provider);

        await SendEmailAsync(message, cancellationToken);
    }

    private MailMessage CreateInsuranceExpirationMessage(
        string email,
        string vehicleInfo,
        DateTimeOffset expirationDate,
        string policyNumber,
        string provider)
    {
        var emailBody = BuildEmailBody(
            vehicleInfo,
            expirationDate,
            policyNumber,
            provider
        );

        return new MailMessage
        {
            From = new MailAddress(_emailOptions.Username),
            Subject = InsuranceExpirationSubject,
            Body = emailBody,
            IsBodyHtml = true,
            To = { new MailAddress(email) }
        };
    }

    private string BuildEmailBody(
        string vehicleInfo,
        DateTimeOffset expirationDate,
        string policyNumber,
        string provider)
    {
        var template = _emailTemplates[InsuranceExpirationTemplateName];
        var replacements = new Dictionary<string, string>
        {
            ["{{vehicleInfo}}"] = vehicleInfo,
            ["{{expirationDate}}"] = expirationDate.ToString("dd.MM.yyyy"),
            ["{{policyNumber}}"] = policyNumber,
            ["{{provider}}"] = provider
        };

        return replacements.Aggregate(
            template,
            (current, replacement) =>
                current.Replace(replacement.Key, replacement.Value));
    }

    private async Task SendEmailAsync(MailMessage message, CancellationToken cancellationToken)
    {
        try
        {
            await _smtpClient.SendMailAsync(message, cancellationToken);
        }
        finally
        {
            message.Dispose();
        }
    }

    private SmtpClient CreateSmtpClient()
    {
        return new SmtpClient(_emailOptions.SmtpHost, _emailOptions.SmtpPort)
        {
            Credentials = new NetworkCredential(_emailOptions.Username, _emailOptions.Password),
            EnableSsl = true
        };
    }

    private static string LoadEmailTemplate(string templateName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourcePath = assembly.GetManifestResourceNames()
                               .FirstOrDefault(x => x.EndsWith(templateName))
                           ?? throw new InvalidOperationException($"Could not find email template: {templateName}");

        using var stream = assembly.GetManifestResourceStream(resourcePath)
                           ?? throw new InvalidOperationException($"Could not load email template: {templateName}");

        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    public void Dispose()
    {
        _smtpClient.Dispose();
    }
}