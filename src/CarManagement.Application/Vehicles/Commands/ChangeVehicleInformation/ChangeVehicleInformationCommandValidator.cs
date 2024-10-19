using CarManagement.Core.Vehicles.Repositories;

namespace CarManagement.Application.Vehicles.Commands.ChangeVehicleInformation;

internal sealed class ChangeVehicleInformationCommandValidator
    : AbstractValidator<ChangeVehicleInformationCommand>
{
    public ChangeVehicleInformationCommandValidator(IVehicleRepository vehicleRepository)
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
                !await vehicleRepository.ExistsWithLicensePlateAsync(command.LicensePlate, command.VehicleId,
                    cancellationToken)
            )
            .WithMessage(command =>
                $"The license plate '{command.LicensePlate}' is already in use by another vehicle");

        RuleFor(x => x.Vin)
            .NotEmpty()
            .MaximumLength(17)
            .MustAsync(async (command, context, cancellationToken) =>
                !await vehicleRepository.ExistsWithVinAsync(command.Vin, command.VehicleId,
                    cancellationToken)
            )
            .WithMessage(command => $"The VIN '{command.Vin}' is already in use by another vehicle");

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