using CarManagement.Core.Vehicles.Entities;

namespace CarManagement.Infrastructure.EF.Vehicles.Configurations;

internal sealed class ServiceBookConfiguration : IEntityTypeConfiguration<ServiceBook>
{
    private const string TableName = "ServiceBooks";

    public void Configure(EntityTypeBuilder<ServiceBook> builder)
    {
        builder.HasKey(sb => sb.Id);

        builder
            .HasMany(serviceBook => serviceBook.Services)
            .WithOne(service => service.ServiceBook)
            .HasForeignKey(service => service.ServiceBookId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(serviceBook => serviceBook.Inspections)
            .WithOne(inspection => inspection.ServiceBook)
            .HasForeignKey(inspection => inspection.ServiceBookId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(TableName);
    }
}