using TimeTracker.Core.Auth.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TimeTracker.Core.Auth.Persistence.Configurations;

public class AccessPermissionConfiguration : IEntityTypeConfiguration<AccessPermission>
{
    public void Configure(EntityTypeBuilder<AccessPermission> builder)
    {
        builder.Property(x => x.Name).HasConversion<string>().IsRequired();
        builder.Property(x => x.DisplayName).IsRequired();
        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasMany(x => x.RolePermissions)
            .WithOne(x => x.Permission)
            .HasForeignKey(x => x.PermissionId);

        builder.Metadata.FindNavigation(nameof(AccessPermission.RolePermissions))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}