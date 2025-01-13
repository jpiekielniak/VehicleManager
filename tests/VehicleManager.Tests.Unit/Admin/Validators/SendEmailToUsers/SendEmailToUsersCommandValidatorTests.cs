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
    public async Task given_valid_command_when_validating_then_should_not_have_errors()
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
    public async Task given_empty_title_when_validating_then_should_have_required_error(string title)
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
    public async Task given_title_101_characters_when_max_100_then_should_have_max_length_error()
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
    public async Task given_empty_content_when_validating_then_should_have_required_error(string content)
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
    public async Task given_content_1501_characters_when_max_1500_then_should_have_max_length_error()
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