using VehicleManager.Core.Vehicles.Entities;

namespace VehicleManager.Infrastructure.EF.Vehicles.Configurations;

internal sealed class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    private const string TableName = "Vehicles";

    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Brand)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(v => v.Model)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(v => v.Year)
            .IsRequired();

        builder.Property(v => v.LicensePlate)
            .IsRequired()
            .HasMaxLength(9);

        builder.Property(v => v.VIN)
            .IsRequired()
            .HasMaxLength(17);

        builder.Property(v => v.EngineCapacity)
            .IsRequired();

        builder.Property(v => v.FuelType)
            .IsRequired();

        builder.Property(v => v.EnginePower)
            .IsRequired();

        builder.Property(v => v.GearboxType)
            .IsRequired();

        builder.Property(v => v.VehicleType)
            .IsRequired();

        builder
            .HasOne(vehicle => vehicle.ServiceBook)
            .WithOne(serviceBook => serviceBook.Vehicle)
            .HasForeignKey<ServiceBook>(serviceBook => serviceBook.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(vehicle => vehicle.Insurances)
            .WithOne(insurance => insurance.Vehicle)
            .HasForeignKey(vehicle => vehicle.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(vehicle => vehicle.Image)
            .WithOne(image => image.Vehicle)
            .HasForeignKey<Image>(image => image.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(TableName);
    }
}