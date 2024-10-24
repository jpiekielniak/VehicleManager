using VehicleManager.Application.ServiceBooks.Queries.GetService;
using VehicleManager.Application.ServiceBooks.Queries.GetService.DTO;
using VehicleManager.Core.Vehicles.Exceptions.ServiceBooks;
using VehicleManager.Core.Vehicles.Repositories;
using VehicleManager.Infrastructure.EF.ServiceBooks.Queries.GetService;
using VehicleManager.Tests.Unit.ServiceBook.Factories;

namespace VehicleManager.Tests.Unit.ServiceBook.Handlers.Queries.GetInspection;

public class GetServiceQueryHandlerTests
{
    private async Task<ServiceDetailsDto> Act(GetServiceQuery query)
        => await _handler.Handle(query, CancellationToken.None);

    [Fact]
    public async Task given_invalid_service_book_id_should_throw_service_book_not_found_exception()
    {
        // Arrange
        var query = _factory.GetServiceQuery();

        _serviceBookRepository.GetAsync(query.ServiceBookId, Arg.Any<CancellationToken>(), true)
            .ReturnsNull();

        // Act
        var exception = await Record.ExceptionAsync(() => Act(query));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ServiceBookNotFoundException>();
        await _serviceBookRepository.Received(1).GetAsync(query.ServiceBookId, Arg.Any<CancellationToken>(), Arg.Any<bool>());
    }

    [Fact]
    public async Task given_invalid_service_id_should_throw_service_not_found_exception()
    {
        // Arrange
        var query = _factory.GetServiceQuery();

        _serviceBookRepository.GetAsync(query.ServiceBookId, Arg.Any<CancellationToken>(), true)
            .Returns(_factory.CreateServiceBook());

        // Act
        var exception = await Record.ExceptionAsync(() => Act(query));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ServiceNotFoundException>();
        await _serviceBookRepository.Received(1).GetAsync(query.ServiceBookId, Arg.Any<CancellationToken>(), Arg.Any<bool>());
    }

    [Fact]
    public async Task given_valid_service_id_should_return_service_details()
    {
        // Arrange
        var serviceBook = _factory.CreateServiceBook();
        var service = _factory.CreateService();
        serviceBook.AddService(service);
        var query = _factory.GetServiceQuery(serviceBook.Id, service.Id);

        _serviceBookRepository.GetAsync(query.ServiceBookId, Arg.Any<CancellationToken>(), true)
            .Returns(serviceBook);

        // Act
        var result = await Act(query);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<ServiceDetailsDto>();
        result.Id.ShouldBe(service.Id);
        result.Costs.Count.ShouldBe(service.Costs.Count());
        await _serviceBookRepository.Received(1).GetAsync(query.ServiceBookId, Arg.Any<CancellationToken>(), Arg.Any<bool>());
    }

    private readonly IServiceBookRepository _serviceBookRepository;
    private readonly IRequestHandler<GetServiceQuery, ServiceDetailsDto> _handler;
    private readonly ServiceBookTestFactory _factory = new();

    public GetServiceQueryHandlerTests()
    {
        _serviceBookRepository = Substitute.For<IServiceBookRepository>();

        _handler = new GetServiceQueryHandler(
            _serviceBookRepository
        );
    }
}