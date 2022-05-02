using CSharpFunctionalExtensions;
using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.TimeTracking.Models.Dto;
using TimeTracker.Core.TimeTracking.Rules;
using TimeTracker.Core.TimeTracking.Utils;

namespace TimeTracker.Core.TimeTracking.Models.Entities;

public class Activity : BaseEntity, IAuditable
{
    public string? Description { get; set; }
    public int DurationInSeconds { get; private set; }
    public DateTime Date { get; private set; }
    public int MandateId { get; private set; }
    public Mandate Mandate { get; private set; }
    public int CategoryId { get; private set; }
    public Category Category { get; private set; }

    private List<Tag> _tags = new List<Tag>();

    public IReadOnlyList<Tag> Tags => _tags.AsReadOnly();
    public DateTime CreatedTime { get; set; }
    public string CreatedBy { get; set; }
    public DateTime LastModifiedTime { get; set; }
    public string LastModifiedBy { get; set; }

    protected Activity()
    {
    }

    public Activity(Mandate mandate, Category category, Duration duration, DateTime date)
    {
        CheckRules(new DurationMustBeAtLeastFiveMinutesRule(duration), new DateMustBeValidRule(date),
            new EntityMustHaveValidIdRule(mandate, nameof(Mandate)),
            new EntityMustHaveValidIdRule(category, nameof(category)));
        DurationInSeconds = TimeConverter.ToSeconds(duration);
        Date = date;
        MandateId = mandate.Id;
        CategoryId = category.Id;
    }

    public void Update(Mandate mandate, Category category, Duration duration, DateTime date)
    {
        CheckRules(new DurationMustBeAtLeastFiveMinutesRule(duration), new DateMustBeValidRule(date),
            new EntityMustHaveValidIdRule(mandate, nameof(Mandate)),
            new EntityMustHaveValidIdRule(category, nameof(category)));
        DurationInSeconds = TimeConverter.ToSeconds(duration);
        Date = date;
        MandateId = mandate.Id;
        CategoryId = category.Id;
    }

    public Result AddTag(ActivityLabel label)
    {
        if (_tags.Any(x => x.ActivityLabelId == label.Id))
        {
            return Result.Failure("Duplicate label");
        }

        _tags.Add(new Tag()
        {
            ActivityLabelId = label.Id
        });

        return Result.Success();
    }

    public Result RemoveTag(int labelId)
    {
        Maybe<Tag?> tag = _tags.FirstOrDefault(x => x.ActivityLabelId == labelId);
        if (tag.HasNoValue) return Result.Failure("Label not tagged");

        _tags.Remove(tag.Value!);
        return Result.Success();
    }
}