using CarManagement.Core.Vehicles.Entities;

namespace CarManagement.Infrastructure.Sieve.Mappers;

internal sealed class VehiclesSieveMapper : ISieveConfiguration
{
    public void Configure(SievePropertyMapper mapper)
    {
        mapper.Property<Vehicle>(v => v.Brand)
            .CanFilter()
            .CanSort()
            .HasName("brand");

        mapper.Property<Vehicle>(v => v.CreatedAt)
            .CanFilter()
            .CanSort()
            .HasName("createdAt");

        mapper.Property<Vehicle>(v => v.EngineCapacity)
            .CanFilter()
            .CanSort()
            .HasName("engineCapacity");

        mapper.Property<Vehicle>(v => v.EnginePower)
            .CanFilter()
            .CanSort()
            .HasName("enginePower");
    }
}