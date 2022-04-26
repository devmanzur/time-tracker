using AutoMapper;
using FluentValidation;
using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.Shared.Utils;
using TimeTracker.Core.TimeTracking.Models.Entities;

namespace TimeTracker.Core.TimeTracking.Models.Dto;

public class MandateDto : BaseDto
{
    public string Name { get;  set; }
    public string ColorCode { get;  set; }
}


public class MandateDtoValidator : AbstractValidator<MandateDto>
{
    public MandateDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.ColorCode).NotNull().NotEmpty().Must(EnumUtils.BelongToType<ColorCode>).WithMessage("Invalid color code value");
    }
}


public class MandateMappingProfile : Profile
{
    public MandateMappingProfile()
    {
        CreateMap<Mandate, MandateDto>()
            .ForMember(x => x.ColorCode, opt =>
                opt.MapFrom(src => src.ColorCode.ToString()));
    }
}