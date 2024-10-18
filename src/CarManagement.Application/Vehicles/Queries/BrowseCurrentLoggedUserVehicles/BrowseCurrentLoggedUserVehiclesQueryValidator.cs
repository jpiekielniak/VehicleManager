namespace CarManagement.Application.Vehicles.Queries.BrowseCurrentLoggedUserVehicles;

internal sealed class BrowseCurrentLoggedUserVehiclesQueryValidator : AbstractValidator<BrowseCurrentLoggedUserVehiclesQuery>
{
    private readonly int[] _pageSizes = [5, 10, 15];
    private readonly string[] _allowedFields = ["brand", "createdAt", "engineCapacity", "enginePower"];

    public BrowseCurrentLoggedUserVehiclesQueryValidator()
    {
        RuleFor(x => x.SieveModel.PageSize.Value)
            .Must(pageSize => _pageSizes.Contains(pageSize))
            .WithMessage("PageSize must be one of the following values: 5, 10, 15");

        RuleFor(x => x.SieveModel.Filters)
            .Must(filter => _allowedFields.Contains(filter))
            .WithMessage("Filter must be one of the following values: brand, createdAt, engineCapacity, enginePower");

        RuleFor(x => x.SieveModel.Sorts)
            .Must(sort => _allowedFields.Contains(sort))
            .WithMessage("Sort must be one of the following values: brand, createdAt, engineCapacity, enginePower");
    }
}