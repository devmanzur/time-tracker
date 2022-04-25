using System.Net;
using CSharpFunctionalExtensions;
using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.Shared.Utils;
using TimeTracker.Core.TimeTracking.Models.Dto;

namespace TimeTracker.Core.TimeTracking.Models.Entities;

public class ActivityLabel : BaseEntity, ICrudEntity<ActivityLabelDto>
{
    public string Name { get; set; }
    public ColorCode ColorCode { get; set; }

    public Result Initialize(ActivityLabelDto dto)
    {
        var validator = new ActivityLabelDtoValidator();
        var validation = validator.Validate(dto);
        if (!validation.IsValid)
        {
            return Result.Failure(validation.Errors.FirstOrDefault()?.ErrorMessage);
        }
        this.Name = dto.Name;
        this.ColorCode = dto.ColorCode.ToEnum<ColorCode>();
        return Result.Success();
    }

    public Result Update(ActivityLabelDto dto)
    {
        var validator = new ActivityLabelDtoValidator();
        var validation = validator.Validate(dto);
        if (!validation.IsValid)
        {
            return Result.Failure(validation.Errors.FirstOrDefault()?.ErrorMessage);
        }
        this.Name = dto.Name;
        this.ColorCode = dto.ColorCode.ToEnum<ColorCode>();
        return Result.Success();
    }
}