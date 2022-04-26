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
}