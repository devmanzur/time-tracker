using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
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

    public async Task<Result> Remove(int id)
    {
        Maybe<Models.Entities.Activity?> activity =
            await _context.Activities.FindById(id);
        if (activity.HasNoValue)
        {
            return Result.Failure<ActivityDetailsDto>("Activity not found");
        }

        _context.Activities.Remove(activity.Value!);
        await _context.SaveChangesAsync();
        return Result.Success();
    }
}