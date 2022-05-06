using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.Shared.Utils;

namespace TimeTracker.Core.TimeTracking.Rules;

public class DateMustBeValidRule : IBusinessRule
{
    private readonly DateTime _dateTime;

    public DateMustBeValidRule(DateTime dateTime)
    {
        _dateTime = dateTime;
    }

    public bool IsBroken()
    {
        return _dateTime.HasNoValue() || _dateTime == default || DateTime.UtcNow.Year - _dateTime.Year > 1 ||
               DateTime.UtcNow.Date < _dateTime.Date;
    }

    public string Message => "Date must be valid";
}