namespace VehicleManager.Application.Vehicles.Commands.DeleteVehicleImage;

internal sealed class DeleteVehicleImageCommandValidator : AbstractValidator<DeleteVehicleImageCommand>
{
    public DeleteVehicleImageCommandValidator()
    {
        RuleFor(x => x.VehicleId)
            .NotEmpty().WithMessage("VehicleId is required.")
            .NotEqual(Guid.Empty).WithMessage("VehicleId is required.");
    }
}