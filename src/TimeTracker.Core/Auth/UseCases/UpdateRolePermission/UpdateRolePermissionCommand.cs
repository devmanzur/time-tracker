using TimeTracker.Core.Auth.Models.Dto;
using CSharpFunctionalExtensions;
using MediatR;

namespace TimeTracker.Core.Auth.UseCases.UpdateRolePermission;

public class UpdateRolePermissionCommand : IRequest<Result<AccessPermissionDto>>
{
    public UpdateRolePermissionCommand()
    {
        
    }

    public string RoleId { get; set; }
    public int PermissionId { get; set; }
}