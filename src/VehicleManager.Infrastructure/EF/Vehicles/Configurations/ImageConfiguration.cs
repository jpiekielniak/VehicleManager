using VehicleManager.Core.Vehicles.Entities;

namespace VehicleManager.Infrastructure.EF.Vehicles.Configurations;

internal sealed class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    private const string TableName = "Images";

    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.BlobUrl)
            .IsRequired();

        builder.Property(i => i.FileName)
            .IsRequired();

        builder.Property(i => i.UploadedAt)
            .IsRequired();

        builder.ToTable(TableName);
    }
}