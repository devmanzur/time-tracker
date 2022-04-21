using TimeTracker.Core.Auth.Models.Dto;
using CSharpFunctionalExtensions;
using MediatR;

namespace TimeTracker.Core.Auth.UseCases.GetAllPermissions;

public class GetAllPermissionsQuery: IRequest<Maybe<List<AccessPermissionDto>>>
{
    
}