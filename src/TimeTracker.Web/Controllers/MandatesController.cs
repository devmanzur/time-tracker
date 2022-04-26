using TimeTracker.Core.TimeTracking.Interfaces;
using TimeTracker.Core.TimeTracking.Models.Dto;
using TimeTracker.Core.TimeTracking.Models.Entities;

namespace TimeTracker.Web.Controllers;

public class MandatesController : BaseCrudController<MandateDto, Mandate>
{
    public MandatesController(ICrudService crudService,IQueryService queryService) : base(crudService,queryService)
    {
    }
}