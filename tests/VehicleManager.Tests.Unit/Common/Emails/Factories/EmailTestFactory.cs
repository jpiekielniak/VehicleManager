using VehicleManager.Core.Common.Helpers;
using VehicleManager.Core.Vehicles.Entities.Enums;
using VehicleManager.Infrastructure.Common.Emails.Options;
using VehicleManager.Tests.Unit.Common.Emails.Factories.Models;

namespace VehicleManager.Tests.Unit.Common.Emails.Factories;

public class EmailTestFactory
{
    private readonly Faker _faker = new();

    public IOptions<EmailOptions> CreateEmailOptions()
    {
        var options = Substitute.For<IOptions<EmailOptions>>();
        options.Value.Returns(new EmailOptions
        {
            SmtpHost = "smtp.test.com",
            SmtpPort = 587,
            Username = _faker.Internet.Email(),
            Password = _faker.Internet.Password(),
            BaseUrl = _faker.Internet.Url()
        });
        return options;
    }

    public string CreateEmail() => _faker.Internet.Email();

    public InsuranceExpirationEmailRequest CreateInsuranceExpirationEmailRequest()
        => new(
            Email: CreateEmail(),
            VehicleInfo: $"{_faker.Vehicle.Manufacturer()} {_faker.Vehicle.Model()}",
            ExpirationDate: DateTimeOffset.UtcNow.AddDays(_faker.Random.Int(1, 30)),
            PolicyNumber: _faker.Random.AlphaNumeric(10),
            Provider: _faker.Company.CompanyName()
        );

    public BulkEmailRequest CreateBulkEmailRequest()
        => new(
            Title: _faker.Lorem.Sentence(),
            Content: _faker.Lorem.Paragraph(),
            Emails: _faker.Make(5, CreateEmail)
        );

    public PasswordResetRequest CreatePasswordResetRequest()
        => new(
            Email: CreateEmail(),
            Token: _faker.Random.Guid().ToString()
        );

    public InspectionExpirationEmailRequest CreateInspectionExpirationEmailRequest()
        => new(
            Email: CreateEmail(),
            VehicleInfo: $"{_faker.Vehicle.Manufacturer()} {_faker.Vehicle.Model()}",
            ExpirationDate: DateTimeOffset.UtcNow.AddDays(_faker.Random.Int(1, 30)),
            InspectionType: InspectionType.Technical.GetDisplay());
}