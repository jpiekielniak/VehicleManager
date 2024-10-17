using CarManagement.Core.Vehicles.Entities;

namespace CarManagement.Infrastructure.EF.Vehicles.Configurations;

internal sealed class CostConfiguration : IEntityTypeConfiguration<Cost>
{
    private const string TableName = "Costs";

    public void Configure(EntityTypeBuilder<Cost> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Amount)
            .IsRequired();

        builder.Property(c => c.Title)
            .IsRequired();

        builder.ToTable(TableName);
    }
}