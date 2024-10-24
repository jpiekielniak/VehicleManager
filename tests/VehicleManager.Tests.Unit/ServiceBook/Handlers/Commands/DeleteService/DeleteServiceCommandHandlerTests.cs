using VehicleManager.Application.ServiceBooks.Commands.DeleteService;
using VehicleManager.Core.Vehicles.Exceptions.ServiceBooks;
using VehicleManager.Core.Vehicles.Repositories;
using VehicleManager.Tests.Unit.ServiceBook.Factories;

namespace VehicleManager.Tests.Unit.ServiceBook.Handlers.Commands.DeleteService;

public class DeleteServiceCommandHandlerTests
{
    private async Task Act(DeleteServiceCommand command)
        => await _handler.Handle(command, CancellationToken.None);

    [Fact]
    public async Task given_invalid_service_book_id_should_throw_service_book_not_found_exception()
    {
        // Arrange
        var command = _factory.CreateDeleteServiceCommand();

        _serviceBookRepository.GetAsync(command.ServiceBookId, Arg.Any<CancellationToken>())
            .ReturnsNull();

        // Act
        var exception = await Record.ExceptionAsync(() => Act(command));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ServiceBookNotFoundException>();
        await _serviceBookRepository.Received(1).GetAsync(command.ServiceBookId, Arg.Any<CancellationToken>());
        await _serviceBookRepository.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }
    
    [Fact]
    public async Task given_invalid_service_id_should_throw_service_not_found_exception()
    {
        // Arrange
        var command = _factory.CreateDeleteServiceCommand();

        var serviceBook = _factory.CreateServiceBook();
        _serviceBookRepository.GetAsync(command.ServiceBookId, Arg.Any<CancellationToken>())
            .Returns(serviceBook);

        // Act
        var exception = await Record.ExceptionAsync(() => Act(command));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ServiceNotFoundException>();
        await _serviceBookRepository.Received(1).GetAsync(command.ServiceBookId, Arg.Any<CancellationToken>());
        await _serviceBookRepository.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }
    
    [Fact]
    public async Task given_valid_service_book_id_and_service_id_should_remove_service()
    {
        // Arrange

        var serviceBook = _factory.CreateServiceBook();
        var service = _factory.CreateService();
        var command = _factory.CreateDeleteServiceCommand(serviceBook.Id, service.Id);
        serviceBook.AddService(service);
        _serviceBookRepository.GetAsync(command.ServiceBookId, Arg.Any<CancellationToken>())
            .Returns(serviceBook);

        // Act
        await Act(command);

        // Assert
        serviceBook.Services.ShouldNotContain(service);
        await _serviceBookRepository.Received(1).GetAsync(command.ServiceBookId, Arg.Any<CancellationToken>());
        await _serviceBookRepository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    private readonly IServiceBookRepository _serviceBookRepository;
    private readonly IRequestHandler<DeleteServiceCommand> _handler;
    private readonly ServiceBookTestFactory _factory = new();

    public DeleteServiceCommandHandlerTests()
    {
        _serviceBookRepository = Substitute.For<IServiceBookRepository>();

        _handler = new DeleteServiceCommandHandler(
            _serviceBookRepository
        );
    }
}