using VehicleManager.Api.Endpoints.ServiceBooks;
using VehicleManager.Core.Users.Entities.Enums;
using VehicleManager.Tests.Integration.ServiceBooks.Factories;

namespace VehicleManager.Tests.Integration.ServiceBooks.Endpoints.AddInspection;

public class AddInspectionEndpointTests : EndpointTests
{
    [Fact]
    public async Task post_add_inspection_without_authentication_should_return_401_status_code()
    {
        // Arrange
        var command = _factory.CreateAddInspectionCommand();

        // Act
        var response = await Client.PostAsJsonAsync(
            ServiceBookEndpoints.Inspections.Replace("{serviceBookId:guid}", Guid.NewGuid().ToString()), command);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task post_add_inspection_with_non_existing_service_book_should_return_400_status_code()
    {
        // Arrange
        Authorize(Guid.NewGuid(), Role.User.ToString());
        var command = _factory.CreateAddInspectionCommand();

        // Act
        var response = await Client.PostAsJsonAsync(
            ServiceBookEndpoints.Inspections.Replace("{serviceBookId:guid}", Guid.NewGuid().ToString()), command);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task post_add_inspection_with_with_valid_data_should_return_201_status_code()
    {
        // Arrange
        var user = _factory.CreateUser();
        var vehicle = _factory.CreateVehicle(user.Id);
        var command = _factory.CreateAddInspectionCommand(vehicle.ServiceBook.Id);
        await SeedDataAsync(user, vehicle);
        Authorize(user.Id, Role.User.ToString());

        // Act
        var response = await Client.PostAsJsonAsync(
            ServiceBookEndpoints.Inspections.Replace("{serviceBookId:guid}", command.ServiceBookId.ToString()),
            command);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        response.Headers.Location.ShouldNotBeNull();
        response.Content.ShouldNotBeNull();
    }


    private readonly ServiceBookTestFactory _factory = new();
}