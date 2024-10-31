using VehicleManager.Api.Endpoints.Vehicles;
using VehicleManager.Core.Users.Entities.Enums;
using VehicleManager.Tests.Integration.Vehicles.Factories;

namespace VehicleManager.Tests.Integration.Vehicles.Endpoints.DeleteInsurance;

public class DeleteInsuranceEndpointTests : VehicleEndpointTest
{
    [Fact]
    public async Task delete_insurance_with_non_existing_vehicle_should_return_400_status_code()
    {
        //Arrange
        Authorize(Guid.NewGuid(), Role.User.ToString());
        var url = $"{VehicleEndpoints.InsuranceById
            .Replace("{vehicleId:guid}", Guid.NewGuid().ToString())
            .Replace("{insuranceId:guid}", Guid.NewGuid().ToString())
        }";

        //Act
        var response = await Client.DeleteAsync(url);

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task delete_insurance_with_non_existing_insurance_should_return_400_status_code()
    {
        //Arrange
        var user = _factory.CreateUser();
        var vehicle = _factory.CreateVehicle(user.Id);
        Authorize(user.Id, user.Role.ToString());
        await SeedDataAsync(user, vehicle);
        var url = $"{VehicleEndpoints.InsuranceById
            .Replace("{vehicleId:guid}", vehicle.Id.ToString())
            .Replace("{insuranceId:guid}", Guid.NewGuid().ToString())
        }";

        //Act
        var response = await Client.DeleteAsync(url);

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task delete_insurance_with_existing_insurance_should_return_204_status_code()
    {
        //Arrange
        var user = _factory.CreateUser();
        var vehicle = _factory.CreateVehicle(user.Id);
        var insurance = _factory.CreateInsurance(vehicle);
        Authorize(user.Id, user.Role.ToString());
        await SeedDataAsync(user, vehicle, insurance: insurance);
        var url = $"{VehicleEndpoints.InsuranceById
            .Replace("{vehicleId:guid}", vehicle.Id.ToString())
            .Replace("{insuranceId:guid}", insurance.Id.ToString())
        }";

        //Act
        var response = await Client.DeleteAsync(url);

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    private readonly VehicleTestFactory _factory = new();
}