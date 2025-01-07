using VehicleManager.Core.Vehicles.Entities;

namespace VehicleManager.Infrastructure.Common.Sieve.Mappers.Vehicles;

internal sealed class InsuranceSieveMapper : ISieveConfiguration
{
    public void Configure(SievePropertyMapper mapper)
    {
        mapper.Property<Insurance>(i => i.Title)
            .CanFilter()
            .CanSort()
            .HasName("title");

        mapper.Property<Insurance>(i => i.PolicyNumber)
            .CanFilter()
            .CanSort()
            .HasName("policyNumber");

        mapper.Property<Insurance>(i => i.ValidFrom)
            .CanFilter()
            .CanSort()
            .HasName("validFrom");

        mapper.Property<Insurance>(i => i.ValidTo)
            .CanFilter()
            .CanSort()
            .HasName("validTo");
    }
}