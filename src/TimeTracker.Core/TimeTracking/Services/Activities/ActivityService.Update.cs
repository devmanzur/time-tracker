using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Core.Shared.Utils;
using TimeTracker.Core.TimeTracking.Models.Dto;
using TimeTracker.Core.TimeTracking.Models.Entities;
using TimeTracker.Core.TimeTracking.Utils;

namespace TimeTracker.Core.TimeTracking.Services.Activities;

public partial class ActivityService
{
    public async Task<Result<ActivityDetailsDto>> Update(int id, ActivityDto request)
    {
        Maybe<Models.Entities.Activity?> activity =
            await _context.Activities
                .Include(x => x.Tags)
                .FindById(id);
        if (activity.HasNoValue)
        {
            return Result.Failure<ActivityDetailsDto>("Activity not found");
        }

        Maybe<Mandate?> mandate =
            await _context.Mandates.FindById(request.MandateId);
        if (mandate.HasNoValue)
        {
            return Result.Failure<ActivityDetailsDto>("Mandate not found");
        }

        Maybe<Category?> category =
            await _context.Categories.FindById(request.CategoryId);
        if (category.HasNoValue)
        {
            return Result.Failure<ActivityDetailsDto>("Category not found");
        }

        activity.Value!.Update(mandate.Value!, category.Value!, request.Duration, request.Date);

        if (request.LabelIds != null && request.LabelIds.Any())
        {
            var currentTags = activity.Value!.Tags.Select(x => x.ActivityLabelId).ToList();
            ClearRemovedLabels(currentTags);
            await SaveNewlyAddedLabels(currentTags);
        }

        await _context.SaveChangesAsync();

        await _context.SaveChangesAsync();
        return Result.Success(new ActivityDetailsDto()
        {
            CategoryId = activity.Value!.CategoryId,
            Category = category.Value!.Name,
            MandateId = activity.Value!.MandateId,
            Mandate = mandate.Value!.Name,
            Date = activity.Value!.Date.ToString("d"),
            Description = activity.Value!.Description,
            Duration = TimeConverter.ToDuration(activity.Value!.DurationInSeconds),
            Id = activity.Value!.Id
        });

        void ClearRemovedLabels(List<int> currentTags)
        {
            var oldTags = currentTags.Except(request.LabelIds).ToList();
            if (oldTags.Any())
            {
                oldTags.ForEach(oldTag => activity.Value!.RemoveTag(oldTag));
            }
        }

        async Task SaveNewlyAddedLabels(List<int> currentTags)
        {
            var newTags = request.LabelIds.Except(currentTags).ToList();
            if (newTags.Any())
            {
                var labels = await _context.ActivityLabels
                    .Where(x => newTags.Contains(x.Id)).ToListAsync();
                labels.ForEach(label => activity.Value!.AddTag(label));
            }
        }
    }
}