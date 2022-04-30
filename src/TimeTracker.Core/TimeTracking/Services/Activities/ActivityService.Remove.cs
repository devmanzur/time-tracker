using CSharpFunctionalExtensions;
using TimeTracker.Core.Shared.Utils;
using TimeTracker.Core.TimeTracking.Models.Dto;
using TimeTracker.Core.TimeTracking.Models.Entities;
using TimeTracker.Core.TimeTracking.Utils;

namespace TimeTracker.Core.TimeTracking.Services.Activities;

public partial class ActivityService
{
    public async Task<Result<ActivityDetailsDto>> Remove(int id)
    {
        Maybe<Activity?> activity =
            await _context.Activities.FindById(id);
        if (activity.HasNoValue)
        {
            return Result.Failure<ActivityDetailsDto>("Activity not found");
        }

        _context.Activities.Remove(activity.Value!);
        await _context.SaveChangesAsync();
        return Result.Success(new ActivityDetailsDto()
        {
            CategoryId = activity.Value!.CategoryId,
            Category = "N/A",
            MandateId = activity.Value!.MandateId,
            Mandate = "N/A",
            Date = activity.Value!.Date.ToString("d"),
            Description = activity.Value!.Description,
            Duration = TimeConverter.ToDuration(activity.Value!.DurationInSeconds),
            Id = activity.Value!.Id
        });
    }
}