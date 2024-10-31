using VehicleManager.Api.Endpoints.Vehicles;
using VehicleManager.Core.Users.Entities.Enums;
using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Tests.Integration.Vehicles.Factories;

namespace VehicleManager.Tests.Integration.Vehicles.Endpoints.BrowseInsurancesForVehicle;

public class BrowseInsurancesForVehicleEndpointTests : VehicleEndpointTest
{
    [Fact]
    public async Task browse_insurances_for_vehicle_without_authentication_should_return_401_status_code()
    {
      //Act
      var response = await Client.GetAsync(VehicleEndpoints.Insurances.Replace("{vehicleId:guid}", Guid.NewGuid().ToString()));
      
      //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task browse_insurances_for_vehicle_with_non_existing_vehicle_should_return_400_status_code()
    {
        //Arrange
        Authorize(Guid.NewGuid(), Role.User.ToString());
        
        //Act
        var response = await Client.GetAsync(VehicleEndpoints.Insurances.Replace("{vehicleId:guid}", Guid.NewGuid().ToString()));
        
        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task browse_insurances_for_vehicle_should_return_200_status_code()
    {
        // Arrange
        var user = _factory.CreateUser();
        var vehicle = _factory.CreateVehicle(user.Id);
        var insurances = _factory.CreateInsurances(vehicle, 5);
        AddInsuranceRange(vehicle, insurances);
        await SeedDataAsync(user, vehicle);
        Authorize(user.Id, user.Role.ToString());
        

        // Act
        var response = await Client.GetAsync($"{VehicleEndpoints.Insurances.Replace("{vehicleId:guid}", vehicle.Id.ToString())}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.ShouldNotBeNull();
    }

    private void AddInsuranceRange(Vehicle vehicle, List<Insurance> insurances)
    {
        foreach (var insurance in insurances)
        {
            vehicle.AddInsurance(insurance);
        }
    }

    private readonly VehicleTestFactory _factory = new();
}