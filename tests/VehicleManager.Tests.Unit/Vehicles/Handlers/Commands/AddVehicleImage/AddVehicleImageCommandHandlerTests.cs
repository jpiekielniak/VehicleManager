using VehicleManager.Application.Integrations.BlobStorage.Services;
using VehicleManager.Application.Vehicles.Commands.AddVehicleImage;
using VehicleManager.Core.Vehicles.Builders;
using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Core.Vehicles.Exceptions.Vehicles;
using VehicleManager.Core.Vehicles.Repositories;
using VehicleManager.Tests.Unit.Vehicles.Factories;

namespace VehicleManager.Tests.Unit.Vehicles.Handlers.Commands.AddVehicleImage;

public class AddVehicleImageCommandHandlerTests
{
    private async Task<AddVehicleImageResponse> Act(AddVehicleImageCommand command)
        => await _handler.Handle(command, CancellationToken.None);
    
    [Fact]
    public async Task given_invalid_vehicle_id_should_throw_vehicle_not_found_exception()
    {
        // Arrange
        var command = _factory.CreateAddVehicleImageCommand();
        _vehicleRepository
            .GetAsync(command.VehicleId, Arg.Any<CancellationToken>())
            .ReturnsNull();

        // Act
        var exception = await Record.ExceptionAsync(() => Act(command));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<VehicleNotFoundException>();
        await _vehicleRepository.Received(1).GetAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        await _vehicleRepository.DidNotReceive().UpdateAsync(Arg.Any<Vehicle>(), Arg.Any<CancellationToken>());
    }
    
    [Fact]
    public async Task given_existing_vehicle_should_add_image()
    {
        // Arrange
        var vehicle = _factory.CreateVehicle();
        var command = _factory.CreateAddVehicleImageCommand(vehicle.Id);
        _vehicleRepository
            .GetAsync(command.VehicleId, Arg.Any<CancellationToken>())
            .Returns(vehicle);

        // Act
        var response = await Act(command);

        // Assert
        response.ShouldNotBeNull();
        await _vehicleRepository.Received(1).GetAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        await _imageRepository.Received(1).AddAsync(Arg.Any<Image>(), Arg.Any<CancellationToken>());
        await _imageRepository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        await _blobStorageService.Received(1).UploadImageAsync(Arg.Any<IFormFile>(), Arg.Any<CancellationToken>());
    }
    
    [Fact]
    public async Task given_existing_vehicle_with_image_should_delete_image()
    {
        // Arrange
        var vehicle = _factory.CreateVehicle();
        var image = _factory.CreateImage(vehicle.Id);
        var vehicleWithImage = new VehicleBuilder(vehicle)
            .WithImage(image)
            .Build();
        
        var command = _factory.CreateAddVehicleImageCommand(vehicleWithImage.Id);
        _vehicleRepository
            .GetAsync(command.VehicleId, Arg.Any<CancellationToken>())
            .Returns(vehicleWithImage);

        // Act
        _ = await Act(command);

        // Assert
        await _blobStorageService.Received(1).DeleteImageAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
        await _imageRepository.Received(1).DeleteAsync(Arg.Any<Image>(), Arg.Any<CancellationToken>());
        await _imageRepository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }


    private readonly IVehicleRepository _vehicleRepository;
    private readonly IBlobStorageService _blobStorageService;
    private readonly IImageRepository _imageRepository;
    private readonly IRequestHandler<AddVehicleImageCommand, AddVehicleImageResponse> _handler;
    private readonly VehicleTestFactory _factory = new();

    public AddVehicleImageCommandHandlerTests()
    {
        _vehicleRepository = Substitute.For<IVehicleRepository>();
        _blobStorageService = Substitute.For<IBlobStorageService>();
        _imageRepository = Substitute.For<IImageRepository>();

        _handler = new AddVehicleImageCommandHandler(
            _blobStorageService,
            _vehicleRepository,
            _imageRepository
        );
    }
}