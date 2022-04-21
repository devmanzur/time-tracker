using TimeTracker.Core.Shared.Interfaces;
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

    private List<ActivityLabel> _labels = new List<ActivityLabel>();

    public Activity(Mandate mandate, Category category, Duration duration, DateTime date)
    {
        DurationInSeconds = TimeConverter.ToSeconds(duration);
        Date = date;
        MandateId = mandate.Id;
        CategoryId = category.Id;
    }

    public IReadOnlyList<ActivityLabel> Labels => _labels.AsReadOnly();
    public DateTime CreatedTime { get; set; }
    public string CreatedBy { get; set; }
    public DateTime LastModifiedTime { get; set; }
    public string LastModifiedBy { get; set; }
}