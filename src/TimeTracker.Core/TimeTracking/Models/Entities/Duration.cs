using CSharpFunctionalExtensions;

namespace TimeTracker.Core.TimeTracking.Models.Entities;

public class Duration : ValueObject
{
    public int Hours { get; set; }
    public int Minutes { get; set; }
    public int Seconds { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        return new List<object>()
        {
            Hours, Minutes, Seconds
        };
    }
}