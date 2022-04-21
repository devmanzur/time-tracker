using TimeTracker.Core.Auth.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TimeTracker.Core.Auth.Persistence.Configurations;

public class ApplicationRoleAccessPermissionConfiguration : IEntityTypeConfiguration<ApplicationRoleAccessPermission>
{
    public void Configure(EntityTypeBuilder<ApplicationRoleAccessPermission> builder)
    {
        builder.HasKey(x => new { x.RoleId, x.PermissionId });
    }
}