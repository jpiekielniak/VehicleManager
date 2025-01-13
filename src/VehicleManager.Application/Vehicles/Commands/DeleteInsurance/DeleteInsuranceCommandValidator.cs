namespace VehicleManager.Application.Vehicles.Commands.DeleteInsurance;

internal sealed class DeleteInsuranceCommandValidator : AbstractValidator<DeleteInsuranceCommand>
{
    public DeleteInsuranceCommandValidator()
    {
        RuleFor(x => x.VehicleId)
            .NotEmpty().WithMessage("VehicleId is required")
            .NotNull().WithMessage("VehicleId is required")
            .NotEqual(Guid.Empty).WithMessage("VehicleId is required");

        RuleFor(x => x.InsuranceId)
            .NotNull().WithMessage("InsuranceId is required")
            .NotEmpty().WithMessage("InsuranceId is required")
            .NotEqual(Guid.Empty).WithMessage("InsuranceId is required");
    }
}