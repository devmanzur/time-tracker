using FluentValidation;
using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.Shared.Utils;
using TimeTracker.Core.TimeTracking.Models.Entities;
using TimeTracker.Core.TimeTracking.Rules;

namespace TimeTracker.Core.TimeTracking.Models.Dto;

public class ActivityDto : BaseDto
{
    public string? Description { get; set; }
    public Duration Duration { get; set; }
    public DateTime Date { get; set; }
    public int MandateId { get; set; }
    public int CategoryId { get; set; }

    public int[]? LabelIds { get; set; }
}

public class ActivityDtoValidator : BaseFluentValidator<ActivityDto>
{
    public ActivityDtoValidator()
    {
        RuleFor(x => x.Duration).NotNull().NotEmpty().Must(IsValidDuration)
            .WithMessage("Duration must be at least 5 minutes");
        RuleFor(x => x.Date).NotNull().NotEmpty();
        RuleFor(x => x.MandateId).NotNull().NotEmpty().Must(ValidationUtils.IsValidEntityId);
        RuleFor(x => x.CategoryId).NotNull().NotEmpty().Must(ValidationUtils.IsValidEntityId);
    }

    private static bool IsValidDuration(Duration arg)
    {
        var rule = new DurationMustBeAtLeastFiveMinutesRule(arg);
        return !rule.IsBroken();
    }
}