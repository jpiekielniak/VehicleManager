using VehicleManager.Application.ServiceBooks.Commands.AddService;
using VehicleManager.Tests.Unit.ServiceBook.Factories;

namespace VehicleManager.Tests.Unit.ServiceBook.Validators.AddService;

public class AddServiceCommandValidatorTests
{
    private const string ServiceBookIdRequiredError = "ServiceBookId is required";
    private const string DateRequiredError = "Date is required";
    private const string TitleRequiredError = "Title is required";
    private const string CostsRequiredError = "Costs is required";
    private const string CostsPositiveAmountError = "Costs must have a positive amount";
    private const string CostsTitleRequiredError = "Costs must have a title";

    [Fact]
    public async Task given_valid_command_when_validating_then_should_not_have_errors()
    {
        // Arrange
        var command = _factory.CreateAddServiceCommand();

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task given_empty_service_book_id_when_validating_then_should_have_required_error()
    {
        // Arrange
        var command = _factory.CreateAddServiceCommand() with { ServiceBookId = Guid.Empty };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ServiceBookId)
            .WithErrorMessage(ServiceBookIdRequiredError);
    }

    [Fact]
    public async Task given_default_date_when_validating_then_should_have_required_error()
    {
        // Arrange
        var command = _factory.CreateAddServiceCommand() with { Date = default };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Date)
            .WithErrorMessage(DateRequiredError);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task given_empty_title_when_validating_then_should_have_required_error(string title)
    {
        // Arrange
        var command = _factory.CreateAddServiceCommand() with { Title = title };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Title)
            .WithErrorMessage(TitleRequiredError);
    }

    [Fact]
    public async Task given_null_costs_when_validating_then_should_have_required_error()
    {
        // Arrange
        var command = _factory.CreateAddServiceCommandWithNullCosts();

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Costs)
            .WithErrorMessage(CostsRequiredError);
    }

    [Fact]
    public async Task given_empty_costs_list_when_validating_then_should_have_required_error()
    {
        // Arrange
        var command = _factory.CreateAddServiceCommandWithEmptyCosts();

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Costs)
            .WithErrorMessage(CostsRequiredError);
    }

    [Fact]
    public async Task given_costs_with_negative_amount_when_validating_then_should_have_error()
    {
        // Arrange
        var command = _factory.CreateAddServiceCommand() with { Costs = _factory.CreateCostsWithNegativeAmount() };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Costs)
            .WithErrorMessage(CostsPositiveAmountError);
    }

    [Fact]
    public async Task given_costs_with_zero_amount_when_validating_then_should_have_error()
    {
        // Arrange
        var command = _factory.CreateAddServiceCommand() with { Costs = _factory.CreateCostsWithZeroAmount() };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Costs)
            .WithErrorMessage(CostsPositiveAmountError);
    }

    [Fact]
    public async Task given_costs_with_null_title_when_validating_then_should_have_error()
    {
        // Arrange
        var command = _factory.CreateAddServiceCommand() with { Costs = _factory.CreateCostsWithNullTitle() };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Costs)
            .WithErrorMessage(CostsTitleRequiredError);
    }

    private readonly IValidator<AddServiceCommand> _validator = new AddServiceCommandValidator();
    private readonly ServiceBookTestFactory _factory = new();
}