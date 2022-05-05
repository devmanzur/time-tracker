using CSharpFunctionalExtensions;
using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.Shared.Utils;
using TimeTracker.Core.TimeTracking.Interfaces;
using TimeTracker.Core.TimeTracking.Models.Dto;

namespace TimeTracker.Core.TimeTracking.Models.Entities;

public class Category : BaseEntity, ICrudEntity<CategoryDto>,IIndividualSpecificEntity
{
    public string Name { get; set; }
    public Priority Priority { get; set; }
    public ColorCode ColorCode { get; set; }
    public string IconUrl { get; set; }
    
    public Result Initialize(CategoryDto dto)
    {
        if (IsInvalid(dto, out var failure)) return failure;
        this.Name = dto.Name;
        Priority = dto.Priority.ToEnum<Priority>();
        ColorCode = dto.ColorCode.ToEnum<ColorCode>();
        IconUrl = dto.IconUrl;
        return Result.Success();
    }

    public Result Update(CategoryDto dto)
    {
        if (IsInvalid(dto, out var failure)) return failure;
        this.Name = dto.Name;
        Priority = dto.Priority.ToEnum<Priority>();
        ColorCode = dto.ColorCode.ToEnum<ColorCode>();
        IconUrl = dto.IconUrl;
        return Result.Success();
    }
    
    private static bool IsInvalid(in CategoryDto dto, out Result output)
    {
        var validator = new CategoryDtoValidator();
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