using VehicleManager.Application.Vehicles.Commands.AddInsurance;
using VehicleManager.Tests.Unit.Vehicles.Factories;

namespace VehicleManager.Tests.Unit.Vehicles.Validators.AddInsurance;

public class AddInsuranceCommandValidatorTests
{
    private const string VehicleIdRequired = "VehicleId is required";
    private const string TitleRequired = "Title is required";
    private const string TitleMaxLength = "Title must not exceed 250 characters";
    private const string ProviderRequired = "Provider is required";
    private const string ProviderMaxLength = "Provider must not exceed 100 characters";
    private const string PolicyNumberRequired = "PolicyNumber is required";
    private const string PolicyNumberMaxLength = "PolicyNumber must not exceed 50 characters";
    private const string ValidFromPast = "ValidFrom must be in the past";
    private const string ValidToGreaterThanValidFrom = "ValidTo must be greater than ValidFrom";

    [Fact]
    public async Task given_valid_command_when_validating_then_should_not_have_errors()
    {
        // Arrange
        var command = _factory.CreateAddInsuranceCommand(Guid.NewGuid());

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task given_empty_vehicle_id_when_validating_then_should_have_validation_error()
    {
        // Arrange
        var command = _factory.CreateAddInsuranceCommand(Guid.Empty);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.VehicleId)
            .WithErrorMessage(VehicleIdRequired);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task given_invalid_title_when_validating_then_should_have_validation_error(string title)
    {
        // Arrange
        var command = _factory.CreateAddInsuranceCommand(Guid.NewGuid());
        command = command with { Title = title };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Title)
            .WithErrorMessage(TitleRequired);
    }

    [Fact]
    public async Task given_title_exceeding_max_length_when_validating_then_should_have_validation_error()
    {
        // Arrange
        var command = _factory.CreateAddInsuranceCommand(Guid.NewGuid());
        command = command with { Title = new string('A', 251) };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Title)
            .WithErrorMessage(TitleMaxLength);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task given_invalid_provider_when_validating_then_should_have_validation_error(string provider)
    {
        // Arrange
        var command = _factory.CreateAddInsuranceCommand(Guid.NewGuid());
        command = command with { Provider = provider };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Provider)
            .WithErrorMessage(ProviderRequired);
    }

    [Fact]
    public async Task given_provider_exceeding_max_length_when_validating_then_should_have_validation_error()
    {
        // Arrange
        var command = _factory.CreateAddInsuranceCommand(Guid.NewGuid());
        command = command with { Provider = new string('A', 101) };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Provider)
            .WithErrorMessage(ProviderMaxLength);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task given_invalid_policy_number_when_validating_then_should_have_validation_error(string policyNumber)
    {
        // Arrange
        var command = _factory.CreateAddInsuranceCommand(Guid.NewGuid());
        command = command with { PolicyNumber = policyNumber };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.PolicyNumber)
            .WithErrorMessage(PolicyNumberRequired);
    }

    [Fact]
    public async Task given_policy_number_exceeding_max_length_when_validating_then_should_have_validation_error()
    {
        // Arrange
        var command = _factory.CreateAddInsuranceCommand(Guid.NewGuid());
        command = command with { PolicyNumber = new string('A', 51) };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.PolicyNumber)
            .WithErrorMessage(PolicyNumberMaxLength);
    }
    

    [Fact]
    public async Task given_valid_to_less_than_valid_from_when_validating_then_should_have_validation_error()
    {
        // Arrange
        var command = _factory.CreateAddInsuranceCommand(Guid.NewGuid());
        command = command with
        {
            ValidFrom = DateTimeOffset.Now,
            ValidTo = DateTimeOffset.Now.AddDays(-1)
        };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ValidTo)
            .WithErrorMessage(ValidToGreaterThanValidFrom);
    }

    private readonly IValidator<AddInsuranceCommand> _validator = new AddInsuranceCommandValidator();
    private readonly VehicleTestFactory _factory = new();
}