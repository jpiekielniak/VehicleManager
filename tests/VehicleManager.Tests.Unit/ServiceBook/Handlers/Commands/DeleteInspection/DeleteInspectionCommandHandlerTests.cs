using VehicleManager.Application.ServiceBooks.Commands.DeleteInspection;
using VehicleManager.Core.Vehicles.Exceptions.ServiceBooks;
using VehicleManager.Core.Vehicles.Repositories;
using VehicleManager.Tests.Unit.ServiceBook.Factories;

namespace VehicleManager.Tests.Unit.ServiceBook.Handlers.Commands.DeleteInspection;

public class DeleteInspectionCommandHandlerTests
{
    private async Task Act(DeleteInspectionCommand command)
        => await _handler.Handle(command, CancellationToken.None);

    [Fact]
    public async Task given_invalid_service_book_id_should_throw_service_book_not_found_exception()
    {
        // Arrange
        var command = _factory.CreateDeleteInspectionCommand();

        _serviceBookRepository.GetAsync(command.ServiceBookId, Arg.Any<CancellationToken>())
            .ReturnsNull();

        // Act
        var exception = await Record.ExceptionAsync(() => Act(command));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ServiceBookNotFoundException>();
        await _serviceBookRepository.Received(1).GetAsync(command.ServiceBookId, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task given_invalid_inspection_id_should_throw_inspection_not_found_exception()
    {
        // Arrange
        var command = _factory.CreateDeleteInspectionCommand();
        var serviceBook = _factory.CreateServiceBook();

        _serviceBookRepository.GetAsync(command.ServiceBookId, Arg.Any<CancellationToken>())
            .Returns(serviceBook);

        // Act
        var exception = await Record.ExceptionAsync(() => Act(command));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InspectionNotFoundException>();
        await _serviceBookRepository.Received(1).GetAsync(command.ServiceBookId, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task given_valid_service_book_and_inspection_id_should_remove_inspection()
    {
        // Arrange
        var serviceBook = _factory.CreateServiceBook();
        var inspection = _factory.CreateInspection();
        var command = _factory.CreateDeleteInspectionCommand(serviceBook.Id, inspection.Id);
        serviceBook.AddInspection(inspection);

        _serviceBookRepository.GetAsync(command.ServiceBookId, Arg.Any<CancellationToken>())
            .Returns(serviceBook);

        // Act
        await Act(command);

        // Assert
        serviceBook.Inspections.ShouldNotContain(inspection);
        await _serviceBookRepository.Received(1).GetAsync(command.ServiceBookId, Arg.Any<CancellationToken>());
        await _serviceBookRepository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    private readonly IServiceBookRepository _serviceBookRepository;
    private readonly IRequestHandler<DeleteInspectionCommand> _handler;
    private readonly ServiceBookTestFactory _factory = new();

    public DeleteInspectionCommandHandlerTests()
    {
        _serviceBookRepository = Substitute.For<IServiceBookRepository>();

        _handler = new DeleteInspectionCommandHandler(
            _serviceBookRepository
        );
    }
}