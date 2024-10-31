using VehicleManager.Api.Endpoints.Vehicles;
using VehicleManager.Application.Vehicles.Queries.GetInsuranceDetailsForVehicle.DTO;
using VehicleManager.Core.Users.Entities.Enums;
using VehicleManager.Tests.Integration.Vehicles.Factories;

namespace VehicleManager.Tests.Integration.Vehicles.Endpoints.GetInsuranceDetailsForVehicle;

public class GetInsuranceDetailsForVehicleEndpointTests : VehicleEndpointTest
{
    [Fact]
    public async Task get_insurance_details_fro_vehicle_with_non_existing_vehicle_should_return_400_status_code()
    {
        //Arrange
        Authorize(Guid.NewGuid(), Role.User.ToString());
        var url = $"{VehicleEndpoints.InsuranceById
            .Replace("{vehicleId:guid}", Guid.NewGuid().ToString())
            .Replace("{insuranceId:guid}", Guid.NewGuid().ToString())}";

        //Act
        var response = await Client.GetAsync(url);

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task get_insurance_details_for_vehicle_with_non_existing_insurance_should_return_400_status_code()
    {
        //Arrange
        var user = _factory.CreateUser();
        var vehicle = _factory.CreateVehicle(user.Id);
        await SeedDataAsync(user, vehicle);
        Authorize(user.Id, user.Role.ToString());
        var url = $"{VehicleEndpoints.InsuranceById
            .Replace("{vehicleId:guid}", vehicle.Id.ToString())
            .Replace("{insuranceId:guid}", Guid.NewGuid().ToString())}";

        //Act
        var response = await Client.GetAsync(url);

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task get_insurance_details_for_vehicle_should_return_insurance_details_and_200_status_code()
    {
        // Arrange
        var user = _factory.CreateUser();
        var vehicle = _factory.CreateVehicle(user.Id);
        var insurance = _factory.CreateInsurance(vehicle);
        await SeedDataAsync(user, vehicle, insurance: insurance);
        Authorize(user.Id, user.Role.ToString());
        var url = $"{VehicleEndpoints.InsuranceById
            .Replace("{vehicleId:guid}", vehicle.Id.ToString())
            .Replace("{insuranceId:guid}", insurance.Id.ToString())}";

        // Act
        var response = await Client.GetAsync(url);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var content = await response.Content.ReadFromJsonAsync<InsuranceDetailsDto>();
        content.ShouldNotBeNull();
        content?.Id.ShouldBe(insurance.Id);
    }

    private readonly VehicleTestFactory _factory = new();
}