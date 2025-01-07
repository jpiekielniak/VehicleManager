using VehicleManager.Application.Common.Interfaces.Auth;
using VehicleManager.Application.Users.Commands.SignIn;
using VehicleManager.Core.Common.Security;
using VehicleManager.Core.Users.Entities;
using VehicleManager.Core.Users.Exceptions.Users;
using VehicleManager.Core.Users.Repositories;
using VehicleManager.Tests.Unit.Users.Factories;

namespace VehicleManager.Tests.Unit.Users.Handlers.Commands.SignIn;

public class SignInCommandHandlerTests
{
    private async Task<SignInResponse> Act(SignInCommand command)
        => await _handler.Handle(command, CancellationToken.None);

    [Fact]
    public async Task given_non_existing_user_should_throw_user_not_found_exception()
    {
        //arrange
        var command = _factory.CreateSignInCommand();
        _userRepository
            .GetByEmailAsync(command.Email, Arg.Any<CancellationToken>())
            .ReturnsNull();

        //act
        var exception = await Record.ExceptionAsync(() => Act(command));

        //assert
        exception.ShouldBeOfType<InvalidCredentialsException>();
        await _userRepository.Received(1).GetByEmailAsync(command.Email, Arg.Any<CancellationToken>());
        await _userRepository.AddAsync(Arg.Any<User>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task given_invalid_password_should_throw_invalid_password_exception()
    {
        //arrange
        var command = _factory.CreateSignInCommand();
        var user = _factory.CreateUser();
        _userRepository
            .GetByEmailAsync(command.Email, Arg.Any<CancellationToken>())
            .Returns(user);
        _passwordHasher.VerifyHashedPassword(command.Password, user.Password)
            .Returns(false);

        //act
        var exception = await Record.ExceptionAsync(() => Act(command));

        //assert
        exception.ShouldBeOfType<InvalidCredentialsException>();
        await _userRepository.Received(1).GetByEmailAsync(command.Email, Arg.Any<CancellationToken>());
        _passwordHasher.Received(1).VerifyHashedPassword(command.Password, user.Password);
    }


    [Fact]
    public async Task given_valid_data_should_return_sign_in_response()
    {
        //arrange
        var command = _factory.CreateSignInCommand();
        var user = _factory.CreateUser();
        var token = _factory.CreateToken(user.Id, user.Role.ToString());
        _userRepository
            .GetByEmailAsync(command.Email, Arg.Any<CancellationToken>())
            .Returns(user);
        _passwordHasher.VerifyHashedPassword(command.Password, user.Password)
            .Returns(true);
        _authManager.GenerateToken(user.Id, user.Role.ToString())
            .Returns(token);

        //act
        var response = await Act(command);

        //assert
        response.ShouldNotBeNull();
        response.ShouldBeOfType<SignInResponse>();
        await _userRepository.Received(1).GetByEmailAsync(command.Email, Arg.Any<CancellationToken>());
        _passwordHasher.Received(1).VerifyHashedPassword(command.Password, user.Password);
        _authManager.Received(1).GenerateToken(user.Id, user.Role.ToString());
    }


    private readonly IRequestHandler<SignInCommand, SignInResponse> _handler;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAuthManager _authManager;
    private readonly UserTestFactory _factory = new();

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