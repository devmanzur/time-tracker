using TimeTracker.Core.Auth.Models.Dto;
using TimeTracker.Core.Auth.Persistence;
using TimeTracker.Core.Shared.Utils;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TimeTracker.Core.Auth.UseCases.GetAllPermissions;

public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, Maybe<List<AccessPermissionDto>>>
{
    private readonly IdentityContext _context;

    public GetAllPermissionsQueryHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Maybe<List<AccessPermissionDto>>> Handle(GetAllPermissionsQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.AccessPermissions.Read()
            .Select(x => new AccessPermissionDto()
            {
                Enabled = false,
                Id = x.Id,
                Name = x.Name.ToString(),
                DisplayName = x.DisplayName
            })
            .ToListAsync(cancellationToken: cancellationToken);
    }
}