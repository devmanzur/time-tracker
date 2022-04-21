using TimeTracker.Core.Auth.Models.Dto;
using TimeTracker.Core.Auth.Models.Entities;
using TimeTracker.Core.Auth.Persistence;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TimeTracker.Core.Auth.UseCases.UpdateRolePermission;

public class
    UpdateRolePermissionCommandHandler : IRequestHandler<UpdateRolePermissionCommand, Result<AccessPermissionDto>>
{
    private readonly IdentityContext _context;

    public UpdateRolePermissionCommandHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Result<AccessPermissionDto>> Handle(UpdateRolePermissionCommand request,
        CancellationToken cancellationToken)
    {
        var getRole =
            _context.Roles.FirstOrDefaultAsync(x => x.Id == request.RoleId, cancellationToken: cancellationToken);
        var getPermission = _context.AccessPermissions.FirstOrDefaultAsync(x => x.Id == request.PermissionId,
            cancellationToken: cancellationToken);

        await Task.WhenAll(getRole, getPermission);

        Maybe<ApplicationRole?> role = getRole.Result;
        Maybe<AccessPermission?> permission = getPermission.Result;

        if (role.HasNoValue || permission.HasNoValue)
        {
            return Result.Failure<AccessPermissionDto>("Invalid role/ permission");
        }

        Maybe<ApplicationRoleAccessPermission?> rolePermission =
            await _context.RoleAccessPermissions.FirstOrDefaultAsync(x =>
                    x.RoleId == request.RoleId && x.PermissionId == request.PermissionId,
                cancellationToken: cancellationToken);

        if (rolePermission.HasNoValue)
        {
            role.Value!.GrantAccess(permission.Value!);
        }
        else
        {
            role.Value!.RevokeAccess(permission.Value!);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(new AccessPermissionDto()
        {
            Enabled = rolePermission.HasNoValue,
            Id = permission.Value!.Id,
            Name = permission.Value!.Name.ToString(),
            DisplayName = permission.Value!.DisplayName,
        });
    }
}