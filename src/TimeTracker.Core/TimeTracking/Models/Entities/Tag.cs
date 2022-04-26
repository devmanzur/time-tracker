using TimeTracker.Core.Shared.Interfaces;

namespace TimeTracker.Core.TimeTracking.Models.Entities;

public class Tag : BaseEntity
{
    public ActivityLabel ActivityLabel { get; set; }
    public int ActivityLabelId { get; set; }
    public Activity Activity { get; set; }
    public int ActivityId { get; set; }
}