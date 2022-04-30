using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Core.Shared.Models;
using TimeTracker.Core.Shared.Utils;
using TimeTracker.Core.TimeTracking.Interfaces;
using TimeTracker.Core.TimeTracking.Models.Dto;
using TimeTracker.Core.TimeTracking.Persistence;
using TimeTracker.Core.TimeTracking.Utils;

namespace TimeTracker.Core.TimeTracking.Services.Activities;

public partial class ActivityService : IActivityService
{
    private readonly TimeTrackingContext _context;
    private readonly IValidator<ActivityDto> _validator;

    public ActivityService(TimeTrackingContext context, IValidator<ActivityDto> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<Result<ActivityDetailsDto?>> Get(int id)
    {
        return await _context.Activities.AsNoTracking()
            .Include(x => x.Category).AsNoTracking()
            .Include(x => x.Mandate).AsNoTracking()
            .Select(activity => new ActivityDetailsDto()
            {
                CategoryId = activity.CategoryId,
                Category = activity.Category.Name,
                MandateId = activity.MandateId,
                Mandate = activity.Mandate.Name,
                Date = activity.Date.ToString("d"),
                Description = activity.Description,
                Duration = TimeConverter.ToDuration(activity.DurationInSeconds),
                Id = activity.Id
            })
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<PageResult<ActivityDetailsDto>> GetPage(Segment segment)
    {
        var total = await _context.Activities.CountAsync();

        var items = await _context.Activities
            .ReadOnly()
            .Segment(segment)
            .Select(activity => new ActivityDetailsDto()
            {
                CategoryId = activity.CategoryId,
                Category = activity.Category.Name,
                MandateId = activity.MandateId,
                Mandate = activity.Mandate.Name,
                Date = activity.Date.ToString("d"),
                Description = activity.Description,
                Duration = TimeConverter.ToDuration(activity.DurationInSeconds),
                Id = activity.Id
            })
            .ToListAsync();

        return new PageResult<ActivityDetailsDto>(items, segment, total);
    }
}