using Microsoft.AspNetCore.Mvc;
using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.Shared.Models.Dto;
using TimeTracker.Core.TimeTracking.Interfaces;

namespace TimeTracker.Web.Controllers;

public abstract class BaseCrudController<T, TE> : BaseApiController
    where T : BaseDto where TE : BaseEntity, ICrudEntity<T>, new()
{
    protected readonly ICrudService _crudService;
    protected readonly IQueryService _queryService;

    protected BaseCrudController(ICrudService crudService,IQueryService queryService)
    {
        _crudService = crudService;
        _queryService = queryService;
    }

    [HttpGet]
    public async Task<ActionResult<Envelope<PageResult<T>>>> GetPage([FromQuery] Segment segment)
    {
        var page = await _queryService.Paginate<T,TE>(segment);
        return Ok(Envelope.Ok(page));
    }
    

    [HttpGet("{id}")]
    public async Task<ActionResult<Envelope<T>>> Get(int id)
    {
        var findItem = await _crudService.FindById<T, TE>(id);
        if (findItem.IsSuccess)
        {
            return Ok(Envelope.Ok(findItem.Value));
        }

        return BadRequest(Envelope.Error(findItem.Error));
    }

    [HttpPost]
    public async Task<ActionResult<Envelope<T>>> Create([FromBody] T request)
    {
        var createItem = await _crudService.CreateItem<T, TE>(request);
        if (createItem.IsSuccess)
        {
            return Ok(Envelope.Ok(createItem.Value));
        }

        return BadRequest(Envelope.Error(createItem.Error));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Envelope<T>>> Update(int id, [FromBody] T request)
    {
        var updateItem = await _crudService.UpdateItem<T, TE>(id, request);
        if (updateItem.IsSuccess)
        {
            return Ok(Envelope.Ok(updateItem.Value));
        }

        return BadRequest(Envelope.Error(updateItem.Error));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Envelope<T>>> Remove(int id)
    {
        var removeItem = await _crudService.RemoveItem<T, TE>(id);
        if (removeItem.IsSuccess)
        {
            return Ok(Envelope.Ok());
        }

        return BadRequest(Envelope.Error(removeItem.Error));
    }
}