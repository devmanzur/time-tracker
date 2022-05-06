using Microsoft.AspNetCore.Mvc;
using TimeTracker.Core.Shared.Models.Dto;
using TimeTracker.Core.TimeTracking.Interfaces;
using TimeTracker.Core.TimeTracking.Models.Dto;

namespace TimeTracker.Web.Controllers;

public class ActivitiesController : BaseApiController
{
    private readonly IActivityService _activityService;

    public ActivitiesController(IActivityService activityService)
    {
        _activityService = activityService;
    }
    
    
    [HttpGet]
    public async Task<ActionResult<Envelope<PageResult<ActivityDetailsDto>>>> GetPage([FromQuery] Segment segment)
    {
        var page = await _activityService.GetPage(segment);
        return Ok(Envelope.Ok(page));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Envelope<ActivityDetailsDto>>> Get(int id)
    {
        var findItem = await _activityService.Get(id);
        if (findItem.IsSuccess)
        {
            return Ok(Envelope.Ok(findItem.Value));
        }

        return BadRequest(Envelope.Error(findItem.Error));
    }

    [HttpPost]
    public async Task<ActionResult<Envelope<ActivityDetailsDto>>> Create([FromBody] ActivityDto request)
    {
        var createItem = await _activityService.Create(request);
        if (createItem.IsSuccess)
        {
            return Ok(Envelope.Ok(createItem.Value));
        }

        return BadRequest(Envelope.Error(createItem.Error));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Envelope<ActivityDetailsDto>>> Update(int id, [FromBody] ActivityDto request)
    {
        var updateItem = await _activityService.Update(id, request);
        if (updateItem.IsSuccess)
        {
            return Ok(Envelope.Ok(updateItem.Value));
        }

        return BadRequest(Envelope.Error(updateItem.Error));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Envelope<ActivityDetailsDto>>> Remove(int id)
    {
        var removeItem = await _activityService.Remove(id);
        if (removeItem.IsSuccess)
        {
            return Ok(Envelope.Ok());
        }

        return BadRequest(Envelope.Error(removeItem.Error));
    }
}