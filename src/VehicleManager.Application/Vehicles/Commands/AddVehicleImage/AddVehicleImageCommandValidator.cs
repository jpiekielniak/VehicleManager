namespace VehicleManager.Application.Vehicles.Commands.AddVehicleImage;

internal sealed class AddVehicleImageCommandValidator : AbstractValidator<AddVehicleImageCommand>
{
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".gif"];
    private const int MaxFileSizeInMegaBytes = 5;

    public AddVehicleImageCommandValidator()
    {
        RuleFor(x => x.VehicleId)
            .NotEmpty();

        RuleFor(x => x.Image)
            .NotNull()
            .Must(image => image.Length <= MaxFileSizeInMegaBytes * 1024 * 1024)
            .WithMessage($"Image size must not exceed {MaxFileSizeInMegaBytes}MB")
            .Must(image => AllowedExtensions.Contains(Path.GetExtension(image.FileName).ToLowerInvariant()))
            .WithMessage($"Only {string.Join(", ", AllowedExtensions)} files are allowed");
    }
}