using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.Shared.Models.Dto;
using TimeTracker.Core.Shared.Utils;
using TimeTracker.Core.TimeTracking.Interfaces;
using TimeTracker.Core.TimeTracking.Models.Dto;
using TimeTracker.Core.TimeTracking.Models.Entities;

namespace TimeTracker.Web.Controllers;

public class ActivityLabelsController : BaseCrudController<ActivityLabelDto, ActivityLabel>
{
    public ActivityLabelsController(ICrudService crudService, IQueryService queryService) : base(crudService,
        queryService)
    {
    }

    [HttpGet("lookup")]
    public async Task<ActionResult<Envelope<List<LookupDto>>>> Lookup()
    {
        var items = await _queryService.Query<ActivityLabel>()
            .Select(x => new LookupDto()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            })
            .ToListAsync();
        return Ok(Envelope.Ok(items));
    }

    [HttpGet("color-codes")]
    public ActionResult<Envelope<List<LookupDto>>> ColorCodesLookup()
    {
        var items = EnumUtils.ToList<ColorCode>().Select(x => new LookupDto()
        {
            Text = x.ToSpacedSentence(),
            Value = x.ToString()
        }).ToList();
        return Ok(Envelope.Ok(items));
    }
}