using VehicleManager.Core.Users.Entities;

namespace VehicleManager.Infrastructure.EF.Users.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    private const string TableName = "Users";

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.FirstName)
            .HasMaxLength(150);

        builder.Property(u => u.LastName)
            .HasMaxLength(150);

        builder.Property(u => u.Password)
            .IsRequired();

        builder.Property(u => u.PhoneNumber)
            .IsRequired(false);

        builder.Property(u => u.CreatedAt)
            .IsRequired();

        builder.Property(u => u.Role)
            .IsRequired();

        builder
            .HasMany(user => user.Vehicles)
            .WithOne(vehicle => vehicle.User)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(TableName);
    }
}