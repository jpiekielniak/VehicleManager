using VehicleManager.Api.Endpoints.ServiceBooks;
using VehicleManager.Application.ServiceBooks.Queries.GetInspection.DTO;
using VehicleManager.Core.Users.Entities.Enums;
using VehicleManager.Tests.Integration.ServiceBooks.Factories;

namespace VehicleManager.Tests.Integration.ServiceBooks.Endpoints.GetInspection;

public class GetInspectionEndpointTest : ServiceBookTest
{
    [Fact]
    public async Task get_inspection_without_authentication_should_return_401_status_code()
    {
        // Arrange
        var url = ServiceBookEndpoints.Inspection
            .Replace("{serviceBookId:guid}", Guid.NewGuid().ToString())
            .Replace("{inspectionId:guid}", Guid.NewGuid().ToString());

        // Act
        var response = await Client.GetAsync(url);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task get_inspection_with_invalid_service_book_id_should_return_400_status_code()
    {
        // Arrange
        Authorize(Guid.NewGuid(), Role.User.ToString());
        var url = ServiceBookEndpoints.Inspection
            .Replace("{serviceBookId:guid}", Guid.NewGuid().ToString())
            .Replace("{inspectionId:guid}", Guid.NewGuid().ToString());

        // Act
        var response = await Client.GetAsync(url);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task get_inspection_with_invalid_service_id_should_return_400_status_code()
    {
        // Arrange
        var user = _factory.CreateUser();
        var vehicle = _factory.CreateVehicle(user.Id);
        Authorize(user.Id, Role.User.ToString());
        var url = ServiceBookEndpoints.Inspection
            .Replace("{serviceBookId:guid}", vehicle.ServiceBookId.ToString())
            .Replace("{inspectionId:guid}", Guid.NewGuid().ToString());

        // Act
        var response = await Client.GetAsync(url);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task get_inspection_with_valid_data_should_return_200_status_code()
    {
        // Arrange
        var user = _factory.CreateUser();
        var vehicle = _factory.CreateVehicle(user.Id);
        var inspection = _factory.CreateInspection(vehicle.ServiceBook);
        Authorize(user.Id, Role.User.ToString());
        await SeedDataAsync(user, vehicle, inspection: inspection);
        var url = ServiceBookEndpoints.Inspection
            .Replace("{serviceBookId:guid}", vehicle.ServiceBookId.ToString())
            .Replace("{inspectionId:guid}", inspection.Id.ToString());

        // Act
        var response = await Client.GetAsync(url);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<InspectionDetailsDto>();
        result.ShouldNotBeNull();
        result.Id.ShouldBe(inspection.Id);
    }

    private readonly ServiceBookTestFactory _factory = new();
}