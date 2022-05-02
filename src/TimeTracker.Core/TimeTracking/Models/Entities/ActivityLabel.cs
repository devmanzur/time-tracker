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

    private List<Tag> _tags = new List<Tag>();

    public IReadOnlyList<Tag> Tags => _tags.AsReadOnly();

    public Result Initialize(ActivityLabelDto dto)
    {
        if (Validate(dto, out var failure)) return failure;
        this.Name = dto.Name;
        this.ColorCode = dto.ColorCode.ToEnum<ColorCode>();
        return Result.Success();
    }

    public Result Update(ActivityLabelDto dto)
    {
        if (Validate(dto, out var failure)) return failure;
        this.Name = dto.Name;
        this.ColorCode = dto.ColorCode.ToEnum<ColorCode>();
        return Result.Success();
    }

    private static bool Validate(in ActivityLabelDto dto, out Result failure)
    {
        var validator = new ActivityLabelDtoValidator();
        var validation = validator.Validate(dto);
        if (!validation.IsValid)
        {
            {
                failure = Result.Failure(validation.Errors.FirstOrDefault()?.ErrorMessage);
                return true;
            }
        }

        return false;
    }
}