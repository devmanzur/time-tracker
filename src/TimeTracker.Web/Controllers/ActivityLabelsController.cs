using Microsoft.AspNetCore.Mvc;
using TimeTracker.Core.Shared.Models;
using TimeTracker.Core.TimeTracking.Interfaces;
using TimeTracker.Core.TimeTracking.Models.Dto;
using TimeTracker.Core.TimeTracking.Models.Entities;

namespace TimeTracker.Web.Controllers;

public class ActivityLabelsController : BaseApiController
{
    private readonly ICrudService _crudService;

    public ActivityLabelsController(ICrudService crudService)
    {
        _crudService = crudService;
    }

    [HttpPost]
    public async Task<ActionResult<Envelope<ActivityLabelDto>>> Create([FromBody] ActivityLabelDto request)
    {
        var createItem = await _crudService.CreateItem<ActivityLabelDto, ActivityLabel>(request);
        if (createItem.IsSuccess)
        {
            return Ok(Envelope.Ok(createItem.Value));
        }

        return BadRequest(Envelope.Error(createItem.Error));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Envelope<ActivityLabelDto>>> Update(int id,[FromBody] ActivityLabelDto request)
    {
        var createItem = await _crudService.UpdateItem<ActivityLabelDto, ActivityLabel>(id,request);
        if (createItem.IsSuccess)
        {
            return Ok(Envelope.Ok(createItem.Value));
        }

        return BadRequest(Envelope.Error(createItem.Error));
    }
}