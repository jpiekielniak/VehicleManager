using VehicleManager.Application.ServiceBooks.Queries.GetInspection;
using VehicleManager.Application.ServiceBooks.Queries.GetInspection.DTO;
using VehicleManager.Core.Vehicles.Exceptions.ServiceBooks;
using VehicleManager.Core.Vehicles.Repositories;
using VehicleManager.Infrastructure.EF.ServiceBooks.Queries.GetInspection;
using VehicleManager.Tests.Unit.ServiceBook.Factories;

namespace VehicleManager.Tests.Unit.ServiceBook.Handlers.Queries.GetService;

public class GetInspectionQueryHandlerTests
{
    private async Task<InspectionDetailsDto> Act(GetInspectionQuery query)
        => await _handler.Handle(query, CancellationToken.None);

    [Fact]
    public async Task given_invalid_service_book_id_should_throw_service_book_not_found_exception()
    {
        // Arrange
        var query = _factory.GetInspectionQuery();

        _serviceBookRepository.GetAsync(query.ServiceBookId, Arg.Any<CancellationToken>(), true)
            .ReturnsNull();

        // Act
        var exception = await Record.ExceptionAsync(() => Act(query));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ServiceBookNotFoundException>();
        await _serviceBookRepository.Received(1)
            .GetAsync(query.ServiceBookId, Arg.Any<CancellationToken>(), Arg.Any<bool>());
    }

    [Fact]
    public async Task given_invalid_inspection_id_should_throw_inspection_not_found_exception()
    {
        // Arrange

        var query = _factory.GetInspectionQuery();


        _serviceBookRepository.GetAsync(query.ServiceBookId, Arg.Any<CancellationToken>(), true)
            .Returns(_factory.CreateServiceBook());

        // Act
        var exception = await Record.ExceptionAsync(() => Act(query));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InspectionNotFoundException>();
        await _serviceBookRepository.Received(1)
            .GetAsync(query.ServiceBookId, Arg.Any<CancellationToken>(), Arg.Any<bool>());
    }

    [Fact]
    public async Task given_valid_service_book_id_and_inspection_id_should_return_inspection_details()
    {
        // Arrange
        var serviceBook = _factory.CreateServiceBook();
        var inspection = _factory.CreateInspection();
        serviceBook.AddInspection(inspection);
        var query = _factory.GetInspectionQuery(serviceBook.Id, inspection.Id);

        _serviceBookRepository.GetAsync(query.ServiceBookId, Arg.Any<CancellationToken>(), true)
            .Returns(serviceBook);

        // Act
        var result = await Act(query);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<InspectionDetailsDto>();
        result.Id.ShouldBe(inspection.Id);
        await _serviceBookRepository.Received(1)
            .GetAsync(query.ServiceBookId, Arg.Any<CancellationToken>(), Arg.Any<bool>());
    }

    private readonly IServiceBookRepository _serviceBookRepository;
    private readonly IRequestHandler<GetInspectionQuery, InspectionDetailsDto> _handler;
    private readonly ServiceBookTestFactory _factory = new();

    public GetInspectionQueryHandlerTests()
    {
        _serviceBookRepository = Substitute.For<IServiceBookRepository>();

        _handler = new GetInspectionQueryHandler(
            _serviceBookRepository
        );
    }
}