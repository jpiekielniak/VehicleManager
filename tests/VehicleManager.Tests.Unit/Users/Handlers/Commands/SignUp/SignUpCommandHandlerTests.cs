using VehicleManager.Application.Users.Commands.SignUp;
using VehicleManager.Core.Users.Entities;
using VehicleManager.Core.Users.Repositories;
using VehicleManager.Shared.Hash;
using VehicleManager.Tests.Unit.Users.Factories;

namespace VehicleManager.Tests.Unit.Users.Handlers.Commands.SignUp;

public class SignUpCommandHandlerTests
{
    [Fact]
    public async Task given_valid_data_when_handle_should_add_user()
    {
        //arrange
        var command = _factory.CreateSignUpCommand();
        _passwordHasher.HashPassword(Arg.Any<string>()).Returns(command.Password);

        //act
        await _handler.Handle(command, CancellationToken.None);

        //assert
        await _userRepository.Received(1).AddAsync(Arg.Any<User>(), Arg.Any<CancellationToken>());
        _passwordHasher.Received(1).HashPassword(Arg.Any<string>());
    }

    private readonly IRequestHandler<SignUpCommand> _handler;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly UserTestFactory _factory = new();

    public SignUpCommandHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _passwordHasher = Substitute.For<IPasswordHasher>();

        _handler = new SignUpCommandHandler(
            _userRepository,
            _passwordHasher
        );
    }
}