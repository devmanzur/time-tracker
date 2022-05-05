using CSharpFunctionalExtensions;
using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.Shared.Utils;
using TimeTracker.Core.TimeTracking.Interfaces;
using TimeTracker.Core.TimeTracking.Models.Dto;

namespace TimeTracker.Core.TimeTracking.Models.Entities;

public class Mandate : BaseEntity, ICrudEntity<MandateDto>,IIndividuallyOwnedEntity
{
    public string Name { get; set; }
    public ColorCode ColorCode { get; set; }

    public Result Initialize(MandateDto dto)
    {
        if (IsInvalid(dto, out var failure)) return failure;

        this.Name = dto.Name;
        this.ColorCode = dto.ColorCode.ToEnum<ColorCode>();
        return Result.Success();
    }

    public Result Update(MandateDto dto)
    {
        if (IsInvalid(dto, out var failure)) return failure;

        this.Name = dto.Name;
        this.ColorCode = dto.ColorCode.ToEnum<ColorCode>();
        return Result.Success();
    }

    private static bool IsInvalid(in MandateDto dto, out Result output)
    {
        var validator = new MandateDtoValidator();
        var validation = validator.Validate(dto);
        if (!validation.IsValid)
        {
            {
                output = Result.Failure(validation.Errors.FirstOrDefault()?.ErrorMessage);
                return true;
            }
        }
        output = Result.Success();
        return false;
    }

    public string IndividualId { get; set; }
}