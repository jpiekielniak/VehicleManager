using VehicleManager.Application.ServiceBooks.Commands.DeleteInspection;
using VehicleManager.Tests.Unit.ServiceBook.Factories;

namespace VehicleManager.Tests.Unit.ServiceBook.Validators.DeleteInspection;

public class DeleteInspectionCommandValidatorTests
{
    private const string ServiceBookIdRequiredError = "ServiceBookId is required";
    private const string InspectionIdRequiredError = "InspectionId is required";

    [Fact]
    public async Task given_valid_command_when_validating_then_should_not_have_errors()
    {
        // Arrange
        var command = _factory.CreateDeleteInspectionCommand();

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task given_empty_service_book_id_when_validating_then_should_have_required_error()
    {
        // Arrange
        var command = _factory.CreateDeleteInspectionCommand(serviceBookId: Guid.Empty);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ServiceBookId)
            .WithErrorMessage(ServiceBookIdRequiredError);
    }

    [Fact]
    public async Task given_empty_inspection_id_when_validating_then_should_have_required_error()
    {
        // Arrange
        var command = _factory.CreateDeleteInspectionCommand(inspectionId: Guid.Empty);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.InspectionId)
            .WithErrorMessage(InspectionIdRequiredError);
    }

    private readonly IValidator<DeleteInspectionCommand> _validator = new DeleteInspectionCommandValidator();
    private readonly ServiceBookTestFactory _factory = new();
}