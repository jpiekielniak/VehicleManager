namespace VehicleManager.Application.ServiceBooks.Commands.AddInspection;

internal sealed class AddInspectionCommandValidator : AbstractValidator<AddInspectionCommand>
{
    public AddInspectionCommandValidator()
    {
        RuleFor(x => x.ServiceBookId)
            .NotEmpty().WithMessage("ServiceBookId is required")
            .NotNull().WithMessage("ServiceBookId is required");

        RuleFor(x => x.ScheduledDate)
            .NotEmpty().WithMessage("ScheduledDate is required")
            .NotNull().WithMessage("ScheduledDate is required");

        RuleFor(x => x.PerformDate)
            .NotEmpty().WithMessage("PerformDate is required")
            .NotNull().WithMessage("PerformDate is required");

        RuleFor(x => x.InspectionType)
            .IsInEnum().WithMessage("InspectionType is invalid");
    }
}