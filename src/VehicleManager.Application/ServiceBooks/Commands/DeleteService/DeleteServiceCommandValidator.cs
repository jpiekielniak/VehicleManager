namespace VehicleManager.Application.ServiceBooks.Commands.DeleteService;

internal sealed class DeleteServiceCommandValidator : AbstractValidator<DeleteServiceCommand>
{
    public DeleteServiceCommandValidator()
    {
        RuleFor(x => x.ServiceBookId)
            .NotEmpty().WithMessage("ServiceBookId is required")
            .NotNull().WithMessage("ServiceBookId is required");

        RuleFor(x => x.ServiceId)
            .NotEmpty().WithMessage("ServiceId is required")
            .NotNull().WithMessage("ServiceId is required");
    }
}