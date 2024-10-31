using VehicleManager.Application.Vehicles.Queries.GetInsuranceDetailsForVehicle;
using VehicleManager.Application.Vehicles.Queries.GetInsuranceDetailsForVehicle.DTO;
using VehicleManager.Core.Vehicles.Exceptions.Vehicles;
using VehicleManager.Core.Vehicles.Repositories;
using VehicleManager.Infrastructure.EF.Vehicles.Queries.GetInsuranceDetailsForVehicle;
using VehicleManager.Tests.Unit.Vehicles.Factories;

namespace VehicleManager.Tests.Unit.Vehicles.Handlers.Queries.GetInsuranceDetailsForVehicle;

public class GetInsuranceDetailsForVehicleQueryHandlerTests
{
    private async Task<InsuranceDetailsDto> Act(GetInsuranceDetailsForVehicleQuery query)
        => await _handler.Handle(query, CancellationToken.None);

    [Fact]
    public async Task given_invalid_vehicle_id_should_throw_vehicle_not_found_exception()
    {
        //Arrange
        var query = _factory.CreateGetInsuranceDetailsForVehicleQuery();
        _vehicleRepository.GetAsync(query.VehicleId, Arg.Any<CancellationToken>())
            .ReturnsNull();

        //Act
        var exception = await Record.ExceptionAsync(() => Act(query));

        //Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<VehicleNotFoundException>();
        await _vehicleRepository.Received(1).GetAsync(query.VehicleId, Arg.Any<CancellationToken>(), Arg.Any<bool>());
    }

    [Fact]
    public async Task given_invalid_insurance_id_should_throw_insurance_not_found_exception()
    {
        //Arrange
        var vehicle = _factory.CreateVehicle();
        var query = _factory.CreateGetInsuranceDetailsForVehicleQuery(vehicle.Id);
        _vehicleRepository.GetAsync(query.VehicleId, Arg.Any<CancellationToken>(), asNoTracking: true)
            .Returns(vehicle);

        //Act
        var exception = await Record.ExceptionAsync(() => Act(query));

        //Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InsuranceNotFoundException>();
        await _vehicleRepository.Received(1).GetAsync(query.VehicleId, Arg.Any<CancellationToken>(), Arg.Any<bool>());
    }

    [Fact]
    public async Task given_valid_data_should_return_insurance_details()
    {
        //Arrange
        var vehicle = _factory.CreateVehicle();
        var insurance = _factory.CreateInsurance(vehicle);
        vehicle.AddInsurance(insurance);
        var query = _factory.CreateGetInsuranceDetailsForVehicleQuery(vehicle.Id, insurance.Id);
        _vehicleRepository.GetAsync(query.VehicleId, Arg.Any<CancellationToken>(), Arg.Any<bool>())
            .Returns(vehicle);

        //Act
        var result = await Act(query);

        //Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<InsuranceDetailsDto>();
        result.Id.ShouldBe(insurance.Id);
        await _vehicleRepository.Received(1).GetAsync(query.VehicleId, Arg.Any<CancellationToken>(), Arg.Any<bool>());
    }


    private readonly IVehicleRepository _vehicleRepository;
    private readonly IRequestHandler<GetInsuranceDetailsForVehicleQuery, InsuranceDetailsDto> _handler;
    private readonly VehicleTestFactory _factory = new();

    public GetInsuranceDetailsForVehicleQueryHandlerTests()
    {
        _vehicleRepository = Substitute.For<IVehicleRepository>();

        _handler = new GetInsuranceDetailsForVehicleQueryHandler(
            _vehicleRepository
        );
    }
}