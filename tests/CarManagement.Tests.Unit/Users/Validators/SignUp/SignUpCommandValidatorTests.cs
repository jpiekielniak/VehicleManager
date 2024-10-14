using CarManagement.Application.Users.Commands.SignUp;
using CarManagement.Core.Users.Entities;
using CarManagement.Core.Users.Repositories;

namespace CarManagement.Tests.Unit.Users.Validators.SignUp;

public class SignUpCommandValidatorTests
{
    [Fact]
    public async Task validate_sign_up_command_with_valid_data_should_return_no_errors()
    {
        //arrange
        var command = CreateCommand();

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
        var command = CreateCommand();
        _userRepository
            .AnyAsync(Arg.Any<System.Linq.Expressions.Expression<Func<User, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(true);

        //act 
        var result = await _validator.TestValidateAsync(command);

        //assert
        result.IsValid.ShouldBeFalse();
        result.ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorMessage("Email already exists");
    }

    [Fact]
    public async Task validate_sign_up_command_with_existing_username_should_return_error()
    {
        //arrange
        var command = CreateCommand();
        _userRepository.AnyAsync(Arg.Any<System.Linq.Expressions.Expression<Func<User, bool>>>(),
                Arg.Any<CancellationToken>())
            .Returns(true);

        //act
        var result = await _validator.TestValidateAsync(command);

        //assert
        result.IsValid.ShouldBeFalse();
        result.ShouldHaveValidationErrorFor(x => x.Username)
            .WithErrorMessage("Username already exists");
    }


    private static SignUpCommand CreateCommand() => new(
        "car.management@test.com",
        "username",
        "123456789",
        "password",
        "password"
    );

    private readonly SignUpCommandValidator _validator;
    private readonly IUserRepository _userRepository;

    public SignUpCommandValidatorTests()
    {
        _userRepository = Substitute.For<IUserRepository>();

        _validator = new SignUpCommandValidator(_userRepository);
    }
}