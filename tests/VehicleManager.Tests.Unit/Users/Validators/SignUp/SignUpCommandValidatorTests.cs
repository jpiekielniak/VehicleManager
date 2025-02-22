using VehicleManager.Application.Users.Commands.SignUp;
using VehicleManager.Core.Users.Entities;
using VehicleManager.Core.Users.Repositories;
using VehicleManager.Tests.Unit.Users.Factories;

namespace VehicleManager.Tests.Unit.Users.Validators.SignUp;

public class SignUpCommandValidatorTests
{
    [Fact]
    public async Task validate_sign_up_command_with_valid_data_should_return_no_errors()
    {
        //arrange
        var command = _factory.CreateSignUpCommand();

        //act
        var result = await _validator.ValidateAsync(command);

        //assert
        result.IsValid.ShouldBeTrue();
        result.Errors.ShouldBeEmpty();
    }

    [Fact]
    public async Task validate_sign_up_command_with_existing_email_should_return_error()
    {
        //arrange
        var command = _factory.CreateSignUpCommand();
        _userRepository
            .AnyAsync(Arg.Any<Expression<Func<User, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(true);

        //act 
        var result = await _validator.TestValidateAsync(command);

        //assert
        result.IsValid.ShouldBeFalse();
        result.ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorMessage(EmailAlreadyExists);
    }

    private const string EmailAlreadyExists = "Email already exists";
    private readonly SignUpCommandValidator _validator;
    private readonly IUserRepository _userRepository;
    private readonly UserTestFactory _factory = new();

    public SignUpCommandValidatorTests()
    {
        _userRepository = Substitute.For<IUserRepository>();

        _validator = new SignUpCommandValidator(_userRepository);
    }
}