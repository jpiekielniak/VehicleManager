using VehicleManager.Core.Vehicles.Entities;

namespace VehicleManager.Infrastructure.Common.Sieve.Mappers.Vehicles;

internal sealed class VehicleSieveMapper : ISieveConfiguration
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