using TimeTracker.Core.Shared.Interfaces;

namespace TimeTracker.Core.Auth.Models.Entities;

public class ApplicationRoleAccessPermission : IAuditable
{
    public string RoleId { get; set; }
    public ApplicationRole Role { get; set; }
    public int PermissionId { get; set; }
    public AccessPermission Permission { get; set; }
    public DateTime CreatedTime { get; set; }
    public string CreatedBy { get; set; }
    public DateTime LastModifiedTime { get; set; }
    public string LastModifiedBy { get; set; }
}