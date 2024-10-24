using VehicleManager.Application.ServiceBooks.Commands.AddService;
using VehicleManager.Core.Vehicles.Exceptions.ServiceBooks;
using VehicleManager.Core.Vehicles.Repositories;
using VehicleManager.Tests.Unit.ServiceBook.Factories;

namespace VehicleManager.Tests.Unit.ServiceBook.Handlers.Commands.AddService;

public class AddServiceCommandHandlerTests
{
    private async Task<AddServiceResponse> Act(AddServiceCommand command)
        => await _handler.Handle(command, CancellationToken.None);

    [Fact]
    public async Task given_invalid_service_book_id_should_throw_service_book_not_found_exception()
    {
        // arrange
        var command = _factory.CreateAddServiceCommand(Guid.NewGuid());

        _serviceBookRepository.GetAsync(command.ServiceBookId, Arg.Any<CancellationToken>())
            .ReturnsNull();

        // act
        var exception = await Record.ExceptionAsync(() => Act(command));

        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ServiceBookNotFoundException>();
        await _serviceBookRepository.Received(1).GetAsync(command.ServiceBookId, Arg.Any<CancellationToken>());
        await _serviceBookRepository.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task given_valid_data_should_return_add_service_response_success()
    {
        // arrange
        var serviceBook = _factory.CreateServiceBook();
        var command = _factory.CreateAddServiceCommand(serviceBook.Id);

        _serviceBookRepository.GetAsync(command.ServiceBookId, Arg.Any<CancellationToken>())
            .Returns(serviceBook);
        _serviceBookRepository.UpdateAsync(serviceBook, Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);
        _serviceBookRepository.SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        // act
        var response = await Act(command);

        // assert
        response.ShouldNotBeNull();
        response.ServiceId.ShouldNotBeSameAs(Guid.Empty);
        response.ShouldBeOfType<AddServiceResponse>();
        await _serviceBookRepository.Received(1).GetAsync(command.ServiceBookId, Arg.Any<CancellationToken>());
        await _serviceBookRepository.Received(1).UpdateAsync(serviceBook, Arg.Any<CancellationToken>());
        await _serviceBookRepository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    private readonly IServiceBookRepository _serviceBookRepository;
    private readonly IRequestHandler<AddServiceCommand, AddServiceResponse> _handler;
    private readonly ServiceBookTestFactory _factory = new();

    public AddServiceCommandHandlerTests()
    {
        _serviceBookRepository = Substitute.For<IServiceBookRepository>();

        _handler = new AddServiceCommandHandler(
            _serviceBookRepository
        );
    }
}