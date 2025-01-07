namespace VehicleManager.Application.Vehicles.Commands.CreateVehicle;

internal sealed class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
{
    public CreateVehicleCommandValidator()
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
            .MaximumLength(10);

        RuleFor(x => x.Vin)
            .NotEmpty()
            .MaximumLength(17);


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