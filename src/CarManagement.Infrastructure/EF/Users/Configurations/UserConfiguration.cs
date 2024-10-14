using CarManagement.Core.Users.Entities;

namespace CarManagement.Infrastructure.EF.Users.Configurations;

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

        builder.HasIndex(u => u.Username)
            .IsUnique();

        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(u => u.Password)
            .IsRequired();

        builder.Property(u => u.CreatedAt)
            .IsRequired();

        builder
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);

        builder.ToTable(TableName);
    }
}