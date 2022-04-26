using CSharpFunctionalExtensions;
using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.Shared.Utils;
using TimeTracker.Core.TimeTracking.Models.Dto;

namespace TimeTracker.Core.TimeTracking.Models.Entities;

public class Mandate : BaseEntity, ICrudEntity<MandateDto>
{
    public string Name { get; set; }
    public ColorCode ColorCode { get; set; }

    public Result Initialize(MandateDto dto)
    {
        var validator = new MandateDtoValidator();
        var validation = validator.Validate(dto);
        if (!validation.IsValid)
        {
            return Result.Failure(validation.Errors.FirstOrDefault()?.ErrorMessage);
        }

        this.Name = dto.Name;
        this.ColorCode = dto.ColorCode.ToEnum<ColorCode>();
        return Result.Success();
    }

    public Result Update(MandateDto dto)
    {
        var validator = new MandateDtoValidator();
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