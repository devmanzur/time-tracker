using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.TimeTracking.Models.Entities;
using TimeTracker.Core.TimeTracking.Utils;

namespace TimeTracker.Core.TimeTracking.Models.Dto;

public class ActivityDetailsDto : BaseDto
{
    public string Mandate { get; set; }
    public string Category { get; set; }
    public string CategoryIconUrl { get; set; }
    public string? Description { get; set; }
    public Duration Duration { get; set; }
    public int DurationInSeconds => TimeConverter.ToSeconds(Duration);
    public string Date { get; set; }
    public int MandateId { get; set; }
    public int CategoryId { get; set; }

    public List<ActivityLabelDto> Labels { get; set; }
}