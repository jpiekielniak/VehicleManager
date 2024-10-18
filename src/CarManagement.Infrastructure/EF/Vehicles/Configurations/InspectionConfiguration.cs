using CarManagement.Core.Vehicles.Entities;

namespace CarManagement.Infrastructure.EF.Vehicles.Configurations;

internal sealed class InspectionConfiguration : IEntityTypeConfiguration<Inspection>
{
    private const string TableName = "Inspections";

    public void Configure(EntityTypeBuilder<Inspection> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.ScheduledDate)
            .IsRequired();

        builder.Property(i => i.PerformDate);

        builder.Property(i => i.InspectionType)
            .IsRequired();

        builder.ToTable(TableName);
    }
}