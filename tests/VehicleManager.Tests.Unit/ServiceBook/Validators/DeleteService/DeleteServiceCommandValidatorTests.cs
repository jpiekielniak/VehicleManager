using VehicleManager.Application.ServiceBooks.Commands.DeleteService;
using VehicleManager.Tests.Unit.ServiceBook.Factories;

namespace VehicleManager.Tests.Unit.ServiceBook.Validators.DeleteService;

public class DeleteServiceCommandValidatorTests
{
    private const string ServiceBookIdRequiredError = "ServiceBookId is required";
    private const string ServiceIdRequiredError = "ServiceId is required";

    [Fact]
    public async Task given_valid_command_when_validating_then_should_not_have_errors()
    {
        // Arrange
        var command = _factory.CreateDeleteServiceCommand();

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task given_empty_service_book_id_when_validating_then_should_have_required_error()
    {
        // Arrange
        var command = _factory.CreateDeleteServiceCommand(serviceBookId: Guid.Empty);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ServiceBookId)
            .WithErrorMessage(ServiceBookIdRequiredError);
    }

    [Fact]
    public async Task given_empty_service_id_when_validating_then_should_have_required_error()
    {
        // Arrange
        var command = _factory.CreateDeleteServiceCommand(serviceId: Guid.Empty);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ServiceId)
            .WithErrorMessage(ServiceIdRequiredError);
    }

    private readonly IValidator<DeleteServiceCommand> _validator = new DeleteServiceCommandValidator();
    private readonly ServiceBookTestFactory _factory = new();
}