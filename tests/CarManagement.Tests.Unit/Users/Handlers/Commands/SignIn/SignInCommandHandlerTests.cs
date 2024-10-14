using CarManagement.Application.Users.Commands.SignIn;
using CarManagement.Core.Users.Entities;
using CarManagement.Core.Users.Entities.Builders;
using CarManagement.Core.Users.Exceptions.Users;
using CarManagement.Core.Users.Repositories;
using CarManagement.Shared.Auth;
using CarManagement.Shared.Hash;
using CarManagement.Tests.Unit.Users.Helpers;

namespace CarManagement.Tests.Unit.Users.Handlers.Commands.SignIn;

public class SignInCommandHandlerTests
{
    [Fact]
    public async Task given_non_existing_user_should_throw_user_not_found_exception()
    {
        //arrange
        var command = CreateCommand();
        _userRepository
            .GetByEmailAsync(command.Email, Arg.Any<CancellationToken>())
            .ReturnsNull();

        //act
        var exception = await Record.ExceptionAsync(() => _handler.Handle(command, CancellationToken.None));

        //assert
        exception.ShouldBeOfType<UserNotFoundException>();
        await _userRepository.Received(1).GetByEmailAsync(command.Email, Arg.Any<CancellationToken>());
        await _userRepository.AddAsync(Arg.Any<User>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task given_invalid_password_should_throw_invalid_password_exception()
    {
        //arrange
        var command = CreateCommand();
        var role = CreateRole();
        var user = CreateUser(role);
        _userRepository
            .GetByEmailAsync(command.Email, Arg.Any<CancellationToken>())
            .Returns(user);
        _passwordHasher.VerifyHashedPassword(command.Password, user.Password)
            .Returns(false);

        //act
        var exception = await Record.ExceptionAsync(() => _handler.Handle(command, CancellationToken.None));

        //assert
        exception.ShouldBeOfType<InvalidPasswordException>();
        await _userRepository.Received(1).GetByEmailAsync(command.Email, Arg.Any<CancellationToken>());
        _passwordHasher.Received(1).VerifyHashedPassword(command.Password, user.Password);
    }


    [Fact]
    public async Task given_valid_data_should_return_sign_in_response()
    {
        //arrange
        var command = CreateCommand();
        var role = CreateRole();
        var user = CreateUser(role);
        var token = JwtHelper.CreateToken(user.Id.ToString(), role.Name);
        _userRepository
            .GetByEmailAsync(command.Email, Arg.Any<CancellationToken>())
            .Returns(user);
        _passwordHasher.VerifyHashedPassword(command.Password, user.Password)
            .Returns(true);
        _authManager.GenerateToken(user.Id, role.Name)
            .Returns(token);

        //act
        var response = await _handler.Handle(command, CancellationToken.None);

        //assert
        response.ShouldNotBeNull();
        response.ShouldBeOfType<SignInResponse>();
        await _userRepository.Received(1).GetByEmailAsync(command.Email, Arg.Any<CancellationToken>());
        _passwordHasher.Received(1).VerifyHashedPassword(command.Password, user.Password);
        _authManager.Received(1).GenerateToken(user.Id, user.Role.Name);
    }

    private static SignInCommand CreateCommand()
        => new("car.management@test.com", "password");

    private static Role CreateRole() => Role.Create("User");

    private static User CreateUser(Role role) => new UserBuilder()
        .WithEmail("car.management@test.com")
        .WithUsername("carmanagement")
        .WithPhoneNumber("123456789")
        .WithPassword("password")
        .WithRole(role)
        .Build();


    private readonly IRequestHandler<SignInCommand, SignInResponse> _handler;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAuthManager _authManager;

    public SignInCommandHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _passwordHasher = Substitute.For<IPasswordHasher>();
        _authManager = Substitute.For<IAuthManager>();

        _handler = new SignInCommandHandler(
            _userRepository,
            _passwordHasher,
            _authManager
        );
    }
}