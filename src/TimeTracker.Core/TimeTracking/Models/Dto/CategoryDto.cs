using AutoMapper;
using FluentValidation;
using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.Shared.Utils;
using TimeTracker.Core.TimeTracking.Models.Entities;

namespace TimeTracker.Core.TimeTracking.Models.Dto;

public class CategoryDto : BaseDto
{
    public string Name { get; set; }
    public string Priority { get; set; }
    public string ColorCode { get; set; }
    public string IconUrl { get; set; }
}

public class CategoryDtoValidator : AbstractValidator<CategoryDto>
{
    public CategoryDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.IconUrl).NotNull().NotEmpty();
        RuleFor(x => x.Priority).NotNull().NotEmpty().Must(EnumUtils.BelongToType<Priority>)
            .WithMessage("Invalid priority value");
        RuleFor(x => x.ColorCode).NotNull().NotEmpty().Must(EnumUtils.BelongToType<ColorCode>)
            .WithMessage("Invalid color code value");
    }
}

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CategoryDto>()
            .ForMember(x => x.ColorCode, opt =>
                opt.MapFrom(src => src.ColorCode.ToString()))
            .ForMember(x => x.Priority, opt =>
                opt.MapFrom(src => src.Priority.ToString()));
    }
}