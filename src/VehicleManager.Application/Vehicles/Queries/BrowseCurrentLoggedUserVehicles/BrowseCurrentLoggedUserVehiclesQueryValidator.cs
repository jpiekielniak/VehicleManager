namespace VehicleManager.Application.Vehicles.Queries.BrowseCurrentLoggedUserVehicles;

internal sealed class BrowseCurrentLoggedUserVehiclesQueryValidator
    : AbstractValidator<BrowseCurrentLoggedUserVehiclesQuery>
{
    private readonly int[] _pageSizes = [5, 10, 15];

    public BrowseCurrentLoggedUserVehiclesQueryValidator()
    {
        RuleFor(x => x.SieveModel.PageSize.Value)
            .Must(pageSize => _pageSizes.Contains(pageSize))
            .WithMessage("PageSize must be one of the following values: 5, 10, 15");
    }
}