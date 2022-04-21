using TimeTracker.Core.Auth.Models.Dto;
using CSharpFunctionalExtensions;
using MediatR;

namespace TimeTracker.Core.Auth.UseCases.GetRolePermissions;

public class GetRolePermissionsQuery : IRequest<Maybe<List<AccessPermissionDto>>>
{
    public GetRolePermissionsQuery(string roleId)
    {
        RoleId = roleId;
    }

    public string RoleId { get; private set; }
}