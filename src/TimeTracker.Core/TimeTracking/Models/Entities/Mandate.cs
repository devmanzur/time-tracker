using TimeTracker.Core.Shared.Interfaces;

namespace TimeTracker.Core.TimeTracking.Models.Entities;

public class Mandate : BaseEntity
{
    public Mandate(string name, ColorCode colorCode)
    {
        Name = name;
        ColorCode = colorCode;
    }

    public string Name { get; private set; }
    public ColorCode ColorCode { get; private set; }
}