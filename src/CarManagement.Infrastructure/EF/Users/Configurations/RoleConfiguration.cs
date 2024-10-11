using CarManagement.Core.Users.Entities;

namespace CarManagement.Infrastructure.EF.Users.Configurations;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    private const string TableName = "Roles";

    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(r => r.CreatedAt)
            .IsRequired();

        builder.ToTable(TableName);
    }
}