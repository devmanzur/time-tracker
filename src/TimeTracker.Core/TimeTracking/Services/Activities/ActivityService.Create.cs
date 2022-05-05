using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Core.Shared.Utils;
using TimeTracker.Core.TimeTracking.Models.Dto;
using TimeTracker.Core.TimeTracking.Models.Entities;
using TimeTracker.Core.TimeTracking.Utils;

namespace TimeTracker.Core.TimeTracking.Services.Activities;

public partial class ActivityService
{
    
    public async Task<Result<ActivityDetailsDto>> Create(ActivityDto request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Result.Failure<ActivityDetailsDto>(validationResult.Errors.FirstOrDefault()!.ErrorMessage);
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

        var activity = new Activity(mandate.Value!, category.Value!, request.Duration, request.Date)
        {
            Description = request.Description
        };

        if (request.LabelIds != null && request.LabelIds.Any())
        {
            var labels = await _context.ActivityLabels.Where(x => request.LabelIds.Contains(x.Id)).ToListAsync();
            labels.ForEach(label => activity.AddTag(label));
        }

        _context.Activities.Add(activity);

        await _context.SaveChangesAsync();
        return Result.Success(new ActivityDetailsDto()
        {
            CategoryId = activity.CategoryId,
            Category = category.Value!.Name,
            MandateId = activity.MandateId,
            Mandate = mandate.Value!.Name,
            Date = activity.Date.ToString("d"),
            Description = activity.Description,
            Duration = TimeConverter.ToDuration(activity.DurationInSeconds),
            Id = activity.Id,
            Labels = activity.Tags.Select(x=>new ActivityLabelDto()
            {
                Id = x.ActivityLabel.Id,
                Name = x.ActivityLabel.Name,
                ColorCode = x.ActivityLabel.ColorCode.ToString()
            }).ToList()
        });
    }
}