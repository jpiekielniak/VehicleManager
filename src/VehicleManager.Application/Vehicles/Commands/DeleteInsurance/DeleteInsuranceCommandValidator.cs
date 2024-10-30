using VehicleManager.Application.Vehicles.Commands.DeleteVehicle;

namespace VehicleManager.Application.Vehicles.Commands.DeleteInsurance;

internal sealed class DeleteInsuranceCommandValidator : AbstractValidator<DeleteVehicleCommand>
{
    public DeleteInsuranceCommandValidator()
    {
        RuleFor(x => x.VehicleId)
            .NotNull().WithMessage("VehicleId is required")
            .NotEqual(Guid.Empty).WithMessage("VehicleId is required");
    }
}