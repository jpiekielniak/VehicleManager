using VehicleManager.Core.Vehicles.Entities;

namespace VehicleManager.Core.Vehicles.Builders;

public class InsuranceBuilder
{
    private readonly Insurance _insurance = Insurance.Create();

    public Insurance Build() => _insurance;

    public InsuranceBuilder WithTitle(string title)
    {
        ArgumentNullException.ThrowIfNull(title);
        _insurance.Title = title;
        return this;
    }

    public InsuranceBuilder WithProvider(string provider)
    {
        ArgumentNullException.ThrowIfNull(provider);
        _insurance.Provider = provider;
        return this;
    }

    public InsuranceBuilder WithPolicyNumber(string policyNumber)
    {
        ArgumentNullException.ThrowIfNull(policyNumber);
        _insurance.PolicyNumber = policyNumber;
        return this;
    }

    public InsuranceBuilder WithValidFrom(DateTimeOffset validFrom)
    {
        ArgumentNullException.ThrowIfNull(validFrom);
        _insurance.ValidFrom = validFrom;
        return this;
    }

    public InsuranceBuilder WithValidTo(DateTimeOffset validTo)
    {
        ArgumentNullException.ThrowIfNull(validTo);
        _insurance.ValidTo = validTo;
        return this;
    }

    public InsuranceBuilder WithVehicle(Vehicle vehicle)
    {
        ArgumentNullException.ThrowIfNull(vehicle);
        _insurance.Vehicle = vehicle;
        _insurance.VehicleId = vehicle.Id;
        return this;
    }
}