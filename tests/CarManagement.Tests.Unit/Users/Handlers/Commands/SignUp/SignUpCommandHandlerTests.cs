using CarManagement.Application.Users.Commands.SignUp;
using CarManagement.Core.Users.Entities;
using CarManagement.Core.Users.Exceptions.Roles;
using CarManagement.Core.Users.Repositories;
using CarManagement.Shared.Hash;

namespace CarManagement.Tests.Unit.Users.Handlers.Commands.SignUp;

public class SignUpCommandHandlerTests
{
    [Fact]
    public async Task given_non_existing_role_when_handle_should_throw_role_not_found_exception()
    {
        //arrange
        var command = CreateCommand();

        _roleRepository
            .GetAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .ReturnsNull();

        //act
        var exception = await Record.ExceptionAsync(() => _handler.Handle(command, CancellationToken.None));

        //assert
        exception.ShouldBeOfType<RoleNotFoundException>();
        await _userRepository.DidNotReceive().AddAsync(Arg.Any<User>(), Arg.Any<CancellationToken>());
        await _roleRepository.Received(1).GetAsync(Role.Name, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task given_valid_data_when_handle_should_add_user()
    {
        //arrange
        var command = CreateCommand();
        _roleRepository.GetAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(Role);
        _passwordHasher.HashPassword(Arg.Any<string>()).Returns(command.Password);

        //act
        await _handler.Handle(command, CancellationToken.None);

        //assert
        await _userRepository.Received(1).AddAsync(Arg.Any<User>(), Arg.Any<CancellationToken>());
        _passwordHasher.Received(1).HashPassword(Arg.Any<string>());
        await _roleRepository.Received(1).GetAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
    }

    private static SignUpCommand CreateCommand() => new(
        "car.management@test.com",
        "car.management.",
        "123456789",
        "password",
        "password"
    );

    private static Role Role => Role.Create("User");

    private readonly IRequestHandler<SignUpCommand> _handler;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHasher _passwordHasher;

    public SignUpCommandHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _roleRepository = Substitute.For<IRoleRepository>();
        _passwordHasher = Substitute.For<IPasswordHasher>();

        _handler = new SignUpCommandHandler(
            _userRepository,
            _roleRepository,
            _passwordHasher
        );
    }
}