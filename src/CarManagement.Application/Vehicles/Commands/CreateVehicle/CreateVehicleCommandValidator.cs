using CarManagement.Core.Vehicles.Repositories;

namespace CarManagement.Application.Vehicles.Commands.CreateVehicle;

internal sealed class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
{
    public CreateVehicleCommandValidator(IVehicleRepository vehicleRepository)
    {
        RuleFor(x => x.Brand)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Model)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Year)
            .InclusiveBetween(1900, DateTime.UtcNow.Year);

        RuleFor(x => x.LicensePlate)
            .NotEmpty()
            .MaximumLength(10)
            .MustAsync(async (command, context, cancellationToken) =>
                !await vehicleRepository.ExistsAsync(
                    v => v.LicensePlate == command.LicensePlate, cancellationToken)
            ).WithMessage("Vehicle with this license plate already exists");

        RuleFor(x => x.Vin)
            .NotEmpty()
            .MaximumLength(17)
            .MustAsync(async (command, context, cancellationToken) =>
                !await vehicleRepository.ExistsAsync(
                    v => v.VIN == command.Vin, cancellationToken)
            ).WithMessage("Vehicle with this VIN already exists");

        RuleFor(x => x.EngineCapacity)
            .InclusiveBetween(0, 10000);

        RuleFor(x => x.EnginePower)
            .InclusiveBetween(0, 1000);

        RuleFor(x => x.FuelType)
            .IsInEnum();

        RuleFor(x => x.VehicleType)
            .IsInEnum();

        RuleFor(x => x.GearboxType)
            .IsInEnum();
    }
}