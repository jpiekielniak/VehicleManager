using VehicleManager.Application.ServiceBooks.Commands.AddInspection;
using VehicleManager.Core.Vehicles.Exceptions.ServiceBooks;
using VehicleManager.Core.Vehicles.Repositories;
using VehicleManager.Tests.Unit.ServiceBook.Factories;

namespace VehicleManager.Tests.Unit.ServiceBook.Handlers.Commands.AddInspection;

public class AddInspectionCommandHandlerTests
{
    private async Task<AddInspectionResponse> Act(AddInspectionCommand command)
        => await _handler.Handle(command, CancellationToken.None);

    [Fact]
    public async Task given_invalid_service_book_id_should_throw_service_book_not_found_exception()
    {
        // arrange
        var command = _factory.CreateAddInspectionCommand();
        _serviceBookRepository
            .GetAsync(command.ServiceBookId, Arg.Any<CancellationToken>())
            .Returns((Core.Vehicles.Entities.ServiceBook)null);

        //act
        var exception = await Record.ExceptionAsync(() => Act(command));

        //assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ServiceBookNotFoundException>();
        await _serviceBookRepository.Received(1).GetAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        await _serviceBookRepository.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task given_valid_data_should_return_add_inspection_response_success()
    {
        // arrange
        var serviceBook = _factory.CreateServiceBook();
        var command = _factory.CreateAddInspectionCommand() with { ServiceBookId = serviceBook.Id };
        _serviceBookRepository
            .GetAsync(serviceBook.Id, Arg.Any<CancellationToken>())
            .Returns(serviceBook);
        _serviceBookRepository
            .UpdateAsync(serviceBook, Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);
        _serviceBookRepository
            .SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        //act
        var response = await Act(command);

        //assert
        response.ShouldNotBeNull();
        response.InspectionId.ShouldNotBe(Guid.Empty);
        response.ShouldBeOfType<AddInspectionResponse>();
        await _serviceBookRepository.Received(1).GetAsync(serviceBook.Id, Arg.Any<CancellationToken>());
        await _serviceBookRepository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }


    private readonly IServiceBookRepository _serviceBookRepository;
    private readonly IRequestHandler<AddInspectionCommand, AddInspectionResponse> _handler;
    private readonly ServiceBookTestFactory _factory = new();

    public AddInspectionCommandHandlerTests()
    {
        _serviceBookRepository = Substitute.For<IServiceBookRepository>();

        _handler = new AddInspectionCommandHandler(_serviceBookRepository);
    }
}