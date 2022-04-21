using TimeTracker.Core.Shared.Interfaces;

namespace TimeTracker.Core.Auth.Models.Entities;

public class AccessPermission : BaseEntity, IAuditable
{
    protected AccessPermission()
    {

    }

    public AccessPermission(Permission name, string displayName)
    {
        Name = name;
        DisplayName = displayName;
    }

    public Permission Name { get; private set; }
    public string DisplayName { get; private set; }
    public DateTime CreatedTime { get; set; }
    public string CreatedBy { get; set; }
    public DateTime LastModifiedTime { get; set; }
    public string LastModifiedBy { get; set; }

    private readonly List<ApplicationRoleAccessPermission> _rolePermissions = new List<ApplicationRoleAccessPermission>();
    public  IReadOnlyList<ApplicationRoleAccessPermission> RolePermissions => _rolePermissions.AsReadOnly();
    
}