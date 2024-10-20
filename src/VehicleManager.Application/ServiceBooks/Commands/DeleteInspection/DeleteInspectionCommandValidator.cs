namespace VehicleManager.Application.ServiceBooks.Commands.DeleteInspection;

internal sealed class DeleteInspectionCommandValidator : AbstractValidator<DeleteInspectionCommand>
{
    public DeleteInspectionCommandValidator()
    {
        RuleFor(x => x.InspectionId)
            .NotEmpty().WithMessage("InspectionId is required")
            .NotNull().WithMessage("InspectionId is required");
    }
}