﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Core.Shared.Models;
using TimeTracker.Core.Shared.Utils;
using TimeTracker.Core.TimeTracking.Interfaces;
using TimeTracker.Core.TimeTracking.Models.Dto;
using TimeTracker.Core.TimeTracking.Models.Entities;

namespace TimeTracker.Web.Controllers;

public class CategoriesController : BaseCrudController<CategoryDto, Category>
{
    public CategoriesController(ICrudService crudService, IQueryService queryService) : base(crudService, queryService)
    {
    }

    [HttpGet("lookup")]
    public async Task<ActionResult<Envelope<List<SelectListItem>>>> Lookup()
    {
        var items = await _queryService.Query<Category>()
            .Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            })
            .ToListAsync();
        return Ok(Envelope.Ok(items));
    }

    [HttpGet("color-codes")]
    public ActionResult<Envelope<List<SelectListItem>>> ColorCodesLookup()
    {
        var items = EnumUtils.ToList<ColorCode>().Select(x => new SelectListItem()
        {
            Text = x.ToSpacedSentence(),
            Value = x.ToString()
        }).ToList();
        return Ok(Envelope.Ok(items));
    }

    [HttpGet("priorities")]
    public ActionResult<Envelope<List<SelectListItem>>> PrioritiesLookup()
    {
        var items = EnumUtils.ToList<Priority>().Select(x => new SelectListItem()
        {
            Text = x.ToSpacedSentence(),
            Value = x.ToString()
        }).ToList();
        return Ok(Envelope.Ok(items));
    }
}