using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.TimeTracking.Models.Entities;
using TimeTracker.Core.TimeTracking.Utils;

namespace TimeTracker.Core.TimeTracking.Rules;

public class DurationMustBeAtLeastFiveMinutesRule : IBusinessRule
{
    private readonly Duration _duration;
    private const int FiveMinutes = 5 * 60;

    public DurationMustBeAtLeastFiveMinutesRule(Duration duration)
    {
        _duration = duration;
    }

    public bool IsBroken()
    {
        return _duration == null || TimeConverter.ToSeconds(_duration) < FiveMinutes;
    }

    public string Message => "Duration must be at least 5 minutes";
}