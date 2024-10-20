using VehicleManager.Application.Users.Commands.SignUp;
using VehicleManager.Core.Users.Entities;
using VehicleManager.Core.Users.Exceptions.Roles;
using VehicleManager.Core.Users.Repositories;
using VehicleManager.Shared.Hash;
using VehicleManager.Tests.Unit.Users.Factories;

namespace VehicleManager.Tests.Unit.Users.Handlers.Commands.SignUp;

public class SignUpCommandHandlerTests
{
    [Fact]
    public async Task given_non_existing_role_when_handle_should_throw_role_not_found_exception()
    {
        //arrange
        var command = _factory.CreateSignUpCommand();

        _roleRepository
            .GetAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .ReturnsNull();

        //act
        var exception = await Record.ExceptionAsync(() => _handler.Handle(command, CancellationToken.None));

        //assert
        exception.ShouldBeOfType<RoleNotFoundException>();
        await _userRepository.DidNotReceive().AddAsync(Arg.Any<User>(), Arg.Any<CancellationToken>());
        await _roleRepository.Received(1).GetAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task given_valid_data_when_handle_should_add_user()
    {
        //arrange
        var command = _factory.CreateSignUpCommand();
        var role = _factory.CreateRole();
        _roleRepository.GetAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(role);
        _passwordHasher.HashPassword(Arg.Any<string>()).Returns(command.Password);

        //act
        await _handler.Handle(command, CancellationToken.None);

        //assert
        await _userRepository.Received(1).AddAsync(Arg.Any<User>(), Arg.Any<CancellationToken>());
        _passwordHasher.Received(1).HashPassword(Arg.Any<string>());
        await _roleRepository.Received(1).GetAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
    }

    private readonly IRequestHandler<SignUpCommand> _handler;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly UserTestFactory _factory = new();

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