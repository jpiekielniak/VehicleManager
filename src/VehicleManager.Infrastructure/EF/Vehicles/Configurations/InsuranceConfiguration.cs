using VehicleManager.Core.Vehicles.Entities;

namespace VehicleManager.Infrastructure.EF.Vehicles.Configurations;

internal sealed class InsuranceConfiguration : IEntityTypeConfiguration<Insurance>
{
    private const string TableName = "Insurances";

    public void Configure(EntityTypeBuilder<Insurance> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Title)
            .IsRequired();

        builder.Property(i => i.Provider)
            .IsRequired();

        builder.Property(i => i.PolicyNumber)
            .IsRequired();

        builder.Property(i => i.ValidFrom)
            .IsRequired();

        builder.Property(i => i.ValidTo)
            .IsRequired();
        
        builder.Property(i => i.ReminderSent)
            .IsRequired();

        builder.ToTable(TableName);
    }
}