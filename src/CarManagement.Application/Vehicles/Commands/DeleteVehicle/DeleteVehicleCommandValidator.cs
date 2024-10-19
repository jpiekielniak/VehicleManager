namespace CarManagement.Application.Vehicles.Commands.DeleteVehicle;

internal sealed class DeleteVehicleCommandValidator : AbstractValidator<DeleteVehicleCommand>
{
    public DeleteVehicleCommandValidator()
    {
        RuleFor(x => x.VehicleId)
            .NotEmpty().WithMessage("VehicleId is required")
            .NotNull().WithMessage("VehicleId is required");
    }
}