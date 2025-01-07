using VehicleManager.Core.Vehicles.Entities;

namespace VehicleManager.Infrastructure.Common.Sieve.Mappers.ServiceBooks.Inspections;

internal sealed class InspectionSieveMapper : ISieveConfiguration
{
    public void Configure(SievePropertyMapper mapper)
    {
        mapper.Property<Inspection>(i => i.Title)
            .CanFilter()
            .CanSort()
            .HasName("title");
    }
}