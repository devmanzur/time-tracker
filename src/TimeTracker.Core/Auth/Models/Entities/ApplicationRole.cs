using Microsoft.AspNetCore.Identity;

namespace TimeTracker.Core.Auth.Models.Entities
{
    public class ApplicationRole : IdentityRole
    {
        private readonly List<ApplicationUserRole> _roles = new List<ApplicationUserRole>();
        public IReadOnlyList<ApplicationUserRole> Roles => _roles.AsReadOnly();

        private readonly List<ApplicationRoleAccessPermission> _rolePermissions =
            new List<ApplicationRoleAccessPermission>();

        public IReadOnlyList<ApplicationRoleAccessPermission> RolePermissions => _rolePermissions.AsReadOnly();

        public void GrantAccess(AccessPermission permission)
        {
            _rolePermissions.Add(new ApplicationRoleAccessPermission()
            {
                RoleId = Id,
                PermissionId = permission.Id
            });
        }

        public void RevokeAccess(AccessPermission permission)
        {
            var rolePermission = _rolePermissions.FirstOrDefault(x => x.PermissionId == permission.Id);
            _rolePermissions.Remove(rolePermission!);
        }
    }
}