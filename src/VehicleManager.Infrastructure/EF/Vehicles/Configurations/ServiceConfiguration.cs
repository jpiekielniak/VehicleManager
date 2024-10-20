using VehicleManager.Core.Vehicles.Entities;

namespace VehicleManager.Infrastructure.EF.Vehicles.Configurations;

internal sealed class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    private const string TableName = "Services";

    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Date)
            .IsRequired();

        builder.Property(s => s.Title)
            .IsRequired();

        builder.Property(s => s.Description)
            .IsRequired();

        builder
            .HasMany(service => service.Costs)
            .WithOne(cost => cost.Service)
            .HasForeignKey(cost => cost.ServiceId);

        builder.ToTable(TableName);
    }
}