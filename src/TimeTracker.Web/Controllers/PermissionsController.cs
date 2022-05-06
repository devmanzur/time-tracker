using TimeTracker.Core.Auth.Models.Dto;
using TimeTracker.Core.Auth.UseCases.GetAllPermissions;
using TimeTracker.Core.Auth.UseCases.GetRolePermissions;
using TimeTracker.Core.Auth.UseCases.UpdateRolePermission;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.Core.Shared.Models.Dto;

namespace TimeTracker.Web.Controllers;

public class PermissionsController : BaseApiController
{
    private readonly IMediator _mediator;

    public PermissionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<Envelope<List<AccessPermissionDto>>>> GetAllPermissions()
    {
        var getPermissions = await _mediator.Send(new GetAllPermissionsQuery());
        if (getPermissions.HasNoValue)
        {
            return UnprocessableEntity(Envelope.Error("No permissions found"));
        }

        return Ok(Envelope.Ok(getPermissions.Value));
    }

    [HttpGet("{role}")]
    public async Task<ActionResult<Envelope<List<AccessPermissionDto>>>> GetRolePermissions(string role)
    {
        var getRolePermissions = await _mediator.Send(new GetRolePermissionsQuery(role));
        if (getRolePermissions.HasNoValue)
        {
            return UnprocessableEntity(Envelope.Error("No permissions found"));
        }

        return Ok(Envelope.Ok(getRolePermissions.Value));
    }

    [HttpPost]
    public async Task<ActionResult<Envelope<AccessPermissionDto>>>
        UpdatePermission([FromBody] UpdateRolePermissionCommand request)
    {
        var updatePermission = await _mediator.Send(request);
        if (updatePermission.IsSuccess)
        {
            return Ok(Envelope.Ok(updatePermission.Value));
        }

        return UnprocessableEntity(Envelope.Error(updatePermission.Error));
    }
}