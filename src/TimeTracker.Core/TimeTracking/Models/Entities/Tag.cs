using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.TimeTracking.Interfaces;

namespace TimeTracker.Core.TimeTracking.Models.Entities;

public class Tag : BaseEntity,IIndividuallyOwnedEntity
{
    public ActivityLabel ActivityLabel { get; set; }
    public int ActivityLabelId { get; set; }
    public Activity Activity { get; set; }
    public int ActivityId { get; set; }
    public string IndividualId { get; set; }
}