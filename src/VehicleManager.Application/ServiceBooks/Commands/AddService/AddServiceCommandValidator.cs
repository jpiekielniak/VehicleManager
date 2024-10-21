namespace VehicleManager.Application.ServiceBooks.Commands.AddService;

internal sealed class AddServiceCommandValidator : AbstractValidator<AddServiceCommand>
{
    public AddServiceCommandValidator()
    {
        RuleFor(x => x.ServiceBookId)
            .NotEmpty().WithMessage("ServiceBookId is required")
            .NotNull().WithMessage("ServiceBookId is required");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required")
            .NotNull().WithMessage("Date is required");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .NotNull().WithMessage("Title is required");

        RuleFor(x => x.Costs)
            .NotEmpty().WithMessage("Costs is required")
            .NotNull().WithMessage("Costs is required")
            .Must(x => x.All(c => c.Amount > 0)).WithMessage("Costs must have a positive amount");

        RuleFor(x => x.Costs)
            .Must(x => x.All(c => c.Title != null)).WithMessage("Costs must have a title");
    }
}