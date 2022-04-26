using TimeTracker.Core.TimeTracking.Models.Entities;

namespace TimeTracker.Core.TimeTracking.Utils;

public class TimeConverter
{
    public static int ToSeconds(Duration duration)
    {
        decimal total = duration.Seconds;

        total += ToLowerTimeUnit(duration.Minutes);

        total += ToLowerTimeUnit(ToLowerTimeUnit(duration.Hours));

        return (int) total;
    }
    public static Duration ToDuration(int seconds)
    {
        var hours = ToUpperTimeUnit(ToUpperTimeUnit(seconds));
        var fullHours = (int) hours;

        var remainingHours = hours - fullHours;
        var minutes = ToLowerTimeUnit(remainingHours);
        var fullMinutes = (int) minutes;

        var remainingMinutes = minutes - fullMinutes;
        var fullSeconds = ToLowerTimeUnit(remainingMinutes);

        return new Duration()
        {
            Hours = fullHours,
            Minutes = fullMinutes,
            Seconds = (int) fullSeconds
        };
    }

    private static decimal ToLowerTimeUnit(decimal duration)
    {
        return duration * 60;
    }

    private static decimal ToUpperTimeUnit(decimal duration)
    {
        return duration / 60;
    }
}