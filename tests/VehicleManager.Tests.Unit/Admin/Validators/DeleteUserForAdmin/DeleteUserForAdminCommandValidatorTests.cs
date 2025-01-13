using VehicleManager.Application.Admin.Commands.DeleteUserForAdmin;
using VehicleManager.Tests.Unit.Admin.Factories;

namespace VehicleManager.Tests.Unit.Admin.Validators.DeleteUserForAdmin;

public class DeleteUserForAdminCommandValidatorTests
{
    private const string UserIdEmptyGuidError = "User id not be equal to empty guid";

    [Fact]
    public async Task given_valid_command_when_validating_then_should_not_have_errors()
    {
        // Arrange
        var command = _factory.CreateDeleteUserForAdminCommand();

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
    

    [Fact]
    public async Task given_empty_guid_user_id_when_validating_then_should_have_empty_guid_error()
    {
        // Arrange
        var command = _factory.CreateDeleteUserForAdminCommand(Guid.Empty);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserId)
            .WithErrorMessage(UserIdEmptyGuidError);
    }

    private readonly IValidator<DeleteUserForAdminCommand> _validator = new DeleteUserForAdminCommandValidator();
    private readonly AdminTestFactory _factory = new();
}