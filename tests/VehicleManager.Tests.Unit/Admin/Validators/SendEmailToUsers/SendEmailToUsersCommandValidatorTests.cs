using VehicleManager.Application.Admin.Commands.SendEmailToUsers;
using VehicleManager.Tests.Unit.Admin.Factories;

namespace VehicleManager.Tests.Unit.Admin.Validators.SendEmailToUsers;

public class SendEmailToUsersCommandValidatorTests
{
    private const string TitleRequiredError = "The title must not be empty.";

    private const string TitleMaxLengthError =
        "The length of title must be 100 characters or fewer.";

    private const string ContentRequiredError = "Content must not be empty.";

    private const string ContentMaxLengthError =
        "The length of content must be 1500 characters or fewer.";

    [Fact]
    public async Task Validate_WithValidData_ShouldNotHaveErrors()
    {
        // Arrange
        var command = _factory.CreateSendEmailToUsersCommand();

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Validate_WithEmptyTitle_ShouldHaveError(string title)
    {
        // Arrange
        var command = _factory.CreateSendEmailToUsersCommand() with { Title = title };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Title)
            .WithErrorMessage(TitleRequiredError);
    }

    [Fact]
    public async Task Validate_WithTitleExceeding100Characters_ShouldHaveError()
    {
        // Arrange
        var titleExceedingMaxLength = new string('x', 101);
        var command = _factory.CreateSendEmailToUsersCommand() with { Title = titleExceedingMaxLength };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Title)
            .WithErrorMessage(TitleMaxLengthError);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Validate_WithEmptyContent_ShouldHaveError(string content)
    {
        // Arrange
        var command = _factory.CreateSendEmailToUsersCommand() with { Content = content };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Content)
            .WithErrorMessage(ContentRequiredError);
    }

    [Fact]
    public async Task Validate_WithContentExceeding1500Characters_ShouldHaveError()
    {
        // Arrange
        var contentExceedingMaxLength = new string('x', 1501);
        var command = _factory.CreateSendEmailToUsersCommand() with { Content = contentExceedingMaxLength };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Content)
            .WithErrorMessage(ContentMaxLengthError);
    }

    private readonly IValidator<SendEmailToUsersCommand> _validator = new SendEmailToUsersCommandValidator();
    private readonly AdminTestFactory _factory = new();
}