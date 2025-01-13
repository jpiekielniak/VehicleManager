using VehicleManager.Application.ServiceBooks.Commands.AddInspection;
using VehicleManager.Core.Vehicles.Entities.Enums;
using VehicleManager.Tests.Unit.ServiceBook.Factories;

namespace VehicleManager.Tests.Unit.ServiceBook.Validators.AddInspection;

public class AddInspectionCommandValidatorTests
{
    private const string ServiceBookIdRequiredError = "ServiceBookId is required";
    private const string ScheduledDateRequiredError = "ScheduledDate is required";
    private const string InspectionTypeInvalidError = "InspectionType is invalid";

    [Fact]
    public async Task given_valid_command_when_validating_then_should_not_have_errors()
    {
        // Arrange
        var command = _factory.CreateAddInspectionCommand();

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task given_empty_service_book_id_when_validating_then_should_have_required_error()
    {
        // Arrange
        var command = _factory.CreateAddInspectionCommand() with { ServiceBookId = Guid.Empty };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ServiceBookId)
            .WithErrorMessage(ServiceBookIdRequiredError);
    }

    [Fact]
    public async Task given_default_scheduled_date_when_validating_then_should_have_required_error()
    {
        // Arrange
        var command = _factory.CreateAddInspectionCommand() with { ScheduledDate = default };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ScheduledDate)
            .WithErrorMessage(ScheduledDateRequiredError);
    }

    [Fact]
    public async Task given_invalid_inspection_type_when_validating_then_should_have_invalid_error()
    {
        // Arrange
        const InspectionType invalidInspectionType = (InspectionType)999;
        var command = _factory.CreateAddInspectionCommand() with { InspectionType = invalidInspectionType };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.InspectionType)
            .WithErrorMessage(InspectionTypeInvalidError);
    }

    private readonly IValidator<AddInspectionCommand> _validator = new AddInspectionCommandValidator();
    private readonly ServiceBookTestFactory _factory = new();
}