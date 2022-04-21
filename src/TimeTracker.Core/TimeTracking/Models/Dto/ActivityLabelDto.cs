using FluentValidation;
using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.Shared.Utils;
using TimeTracker.Core.TimeTracking.Models.Entities;

namespace TimeTracker.Core.TimeTracking.Models.Dto;

public class ActivityLabelDto : BaseDto
{
    public string Name { get;  set; }
    public string ColorCode { get;  set; }
}


public class ActivityLabelDtoValidation : AbstractValidator<ActivityLabelDto>
{
    public ActivityLabelDtoValidation()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.ColorCode).NotNull().NotEmpty().Must(EnumUtils.BelongToType<ColorCode>);
    }
}