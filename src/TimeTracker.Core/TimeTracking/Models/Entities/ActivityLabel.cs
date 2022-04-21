using System.Net;
using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.Shared.Utils;
using TimeTracker.Core.TimeTracking.Models.Dto;

namespace TimeTracker.Core.TimeTracking.Models.Entities;

public class ActivityLabel : BaseEntity, ICrudEntity<ActivityLabelDto>
{
    public string Name { get; set; }
    public ColorCode ColorCode { get; set; }

    public void Initialize(ActivityLabelDto dto)
    {
        this.Name = dto.Name;
        this.ColorCode = dto.ColorCode.ToEnum<ColorCode>();
    }

    public void Update(ActivityLabelDto dto)
    {
        this.Name = dto.Name;
        this.ColorCode = dto.ColorCode.ToEnum<ColorCode>();
    }
}