using CarManagement.Application.Vehicles.Commands.CreateVehicle;
using CarManagement.Core.Users.Entities;
using CarManagement.Core.Users.Repositories;
using CarManagement.Tests.Unit.Vehicles.Factories;

namespace CarManagement.Tests.Unit.Vehicles.Validators.CreateVehicle;

public class CreateVehicleCommandValidatorTests
{
    [Fact]
    public async Task validate_create_vehicle_command_with_valid_data_should_return_no_errors()
    {
        //arrange
        var command = _factory.CreateVehicleCommand();
        userRepository
            .AnyAsync(Arg.Any<Expression<Func<User, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(true);
        
        //act
        var result = await _validator.ValidateAsync(command);

        //assert
        result.IsValid.ShouldBeTrue();
        result.Errors.ShouldBeEmpty();
    }

    [Fact]
    public async Task validate_create_vehicle_command_with_no_existing_user_should_return_error()
    {
        //arrange
        var command = _factory.CreateVehicleCommand();
        userRepository
            .AnyAsync(Arg.Any<Expression<Func<User, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(false);

        //act
        var result = await _validator.TestValidateAsync(command);

        //assert
        result.IsValid.ShouldBeFalse();
        result.ShouldHaveValidationErrorFor(x => x.UserId)
            .WithErrorMessage("User does not exist");
    }

    private readonly IValidator<CreateVehicleCommand> _validator;
    private readonly IUserRepository userRepository;
    private readonly VehicleTestFactory _factory = new();

    public CreateVehicleCommandValidatorTests()
    {
        userRepository = Substitute.For<IUserRepository>();
        _validator = new CreateVehicleCommandValidator(userRepository);
    }
}