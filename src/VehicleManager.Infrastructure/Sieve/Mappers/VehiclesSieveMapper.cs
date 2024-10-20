using VehicleManager.Core.Vehicles.Entities;

namespace VehicleManager.Infrastructure.Sieve.Mappers;

internal sealed class VehiclesSieveMapper : ISieveConfiguration
{
    public void Configure(SievePropertyMapper mapper)
    {
        mapper.Property<Vehicle>(v => v.Brand)
            .CanFilter()
            .CanSort()
            .HasName("brand");

        mapper.Property<Vehicle>(v => v.EnginePower)
            .CanFilter()
            .CanSort()
            .HasName("enginePower");
        
        mapper.Property<Vehicle>(v => v.EngineCapacity)
            .CanFilter()
            .CanSort()
            .HasName("engineCapacity");

    }
}