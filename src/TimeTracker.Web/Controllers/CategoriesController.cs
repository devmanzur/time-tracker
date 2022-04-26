using Microsoft.AspNetCore.Mvc;
using TimeTracker.Core.Shared.Models;
using TimeTracker.Core.TimeTracking.Interfaces;
using TimeTracker.Core.TimeTracking.Models.Dto;
using TimeTracker.Core.TimeTracking.Models.Entities;

namespace TimeTracker.Web.Controllers;

public class CategoriesController : BaseCrudController<CategoryDto, Category>
{
    public CategoriesController(ICrudService crudService,IQueryService queryService) : base(crudService,queryService)
    {
    }
}