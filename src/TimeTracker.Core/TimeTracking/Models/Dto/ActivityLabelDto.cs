using AutoMapper;
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


public class ActivityLabelDtoValidator : BaseFluentValidator<ActivityLabelDto>
{
    public ActivityLabelDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.ColorCode).NotNull().NotEmpty().Must(EnumUtils.BelongToType<ColorCode>).WithMessage("Invalid color code value");
    }
}


public class ActivityLabelMappingProfile : Profile
{
    public ActivityLabelMappingProfile()
    {
        CreateMap<ActivityLabel, ActivityLabelDto>()
            .ForMember(x => x.ColorCode, opt =>
                opt.MapFrom(src => src.ColorCode.ToString()));
    }
}