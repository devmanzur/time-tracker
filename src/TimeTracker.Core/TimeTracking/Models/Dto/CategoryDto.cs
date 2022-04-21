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

public class CategoryDtoValidation : AbstractValidator<CategoryDto>
{
    public CategoryDtoValidation()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.IconUrl).NotNull().NotEmpty();
        RuleFor(x => x.Priority).NotNull().NotEmpty().Must(EnumUtils.BelongToType<Priority>);
        RuleFor(x => x.ColorCode).NotNull().NotEmpty().Must(EnumUtils.BelongToType<ColorCode>);
    }
}