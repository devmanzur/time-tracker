using Microsoft.AspNetCore.Mvc;
using TimeTracker.Core.Shared.Models;
using TimeTracker.Core.TimeTracking.Interfaces;
using TimeTracker.Core.TimeTracking.Models.Dto;
using TimeTracker.Core.TimeTracking.Models.Entities;

namespace TimeTracker.Web.Controllers;

public class CategoriesController : BaseApiController
{
    private readonly ICrudService _crudService;

    public CategoriesController(ICrudService crudService)
    {
        _crudService = crudService;
    }

    [HttpPost]
    public async Task<ActionResult<Envelope<CategoryDto>>> Create([FromBody] CategoryDto request)
    {
        var createItem = await _crudService.CreateItem<CategoryDto, Category>(request);
        if (createItem.IsSuccess)
        {
            return Ok(Envelope.Ok(createItem.Value));
        }

        return BadRequest(Envelope.Error(createItem.Error));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Envelope<CategoryDto>>> Update(int id,[FromBody] CategoryDto request)
    {
        var createItem = await _crudService.UpdateItem<CategoryDto, Category>(id,request);
        if (createItem.IsSuccess)
        {
            return Ok(Envelope.Ok(createItem.Value));
        }

        return BadRequest(Envelope.Error(createItem.Error));
    }
}