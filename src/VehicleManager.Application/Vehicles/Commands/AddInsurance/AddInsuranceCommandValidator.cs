namespace VehicleManager.Application.Vehicles.Commands.AddInsurance;

internal sealed class AddInsuranceCommandValidator : AbstractValidator<AddInsuranceCommand>
{
    public AddInsuranceCommandValidator()
    {
        RuleFor(x => x.VehicleId)
            .NotNull().WithMessage("VehicleId is required")
            .NotEqual(Guid.Empty).WithMessage("VehicleId is required");

        RuleFor(x => x.Title)
            .NotNull().WithMessage("Title is required")
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(250).WithMessage("Title must not exceed 250 characters");

        RuleFor(x => x.PolicyNumber)
            .NotNull().WithMessage("PolicyNumber is required")
            .NotEmpty().WithMessage("PolicyNumber is required")
            .MaximumLength(50).WithMessage("PolicyNumber must not exceed 50 characters");

        RuleFor(x => x.Provider)
            .NotNull().WithMessage("Provider is required")
            .NotEmpty().WithMessage("Provider is required")
            .MaximumLength(100).WithMessage("Provider must not exceed 100 characters");

        RuleFor(x => x.ValidFrom)
            .NotNull().WithMessage("ValidFrom is required")
            .LessThan(DateTime.Now).WithMessage("ValidFrom must be in the past");

        RuleFor(x => x.ValidTo)
            .NotNull().WithMessage("ValidTo is required")
            .GreaterThan(x => x.ValidFrom).WithMessage("ValidTo must be greater than ValidFrom");
    }
}