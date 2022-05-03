using FluentValidation;
using FluentValidation.Results;

namespace TimeTracker.Core.Shared.Utils;

public abstract class BaseFluentValidator<T> : AbstractValidator<T> where T: class
{
    protected override bool PreValidate(ValidationContext<T> context, ValidationResult result)
    {
        if (context.InstanceToValidate == null)
        {
            result.Errors.Add(new ValidationFailure("", "Request is null"));
            return false;
        }
        return base.PreValidate(context, result);
    }
}