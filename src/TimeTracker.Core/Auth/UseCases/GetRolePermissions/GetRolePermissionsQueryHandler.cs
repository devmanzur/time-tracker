using TimeTracker.Core.Auth.Models.Dto;
using TimeTracker.Core.Auth.Persistence;
using TimeTracker.Core.Shared.Utils;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TimeTracker.Core.Auth.UseCases.GetRolePermissions;

public class GetRolePermissionsQueryHandler : IRequestHandler<GetRolePermissionsQuery, Maybe<List<AccessPermissionDto>>>
{
    private readonly IdentityContext _context;

    public GetRolePermissionsQueryHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Maybe<List<AccessPermissionDto>>> Handle(GetRolePermissionsQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.RoleAccessPermissions
            .Read()
            .Where(x => x.RoleId == request.RoleId)
            .Include(x => x.Permission)
            .Select(x =>  new AccessPermissionDto()
            {
                Id = x.PermissionId,
                Name = x.Permission.Name.ToString(),
                DisplayName = x.Permission.DisplayName,
                Enabled = true
            })
            .ToListAsync(cancellationToken: cancellationToken);
    }
}