using TimeTracker.Core.Shared.Exceptions;

namespace TimeTracker.Core.Shared.Interfaces;

public abstract class BaseEntity 
{
    public int Id { get; set; }
        
        
    protected static void CheckRules(params IBusinessRule[] rules)
    {
        var exception = new BusinessRuleViolationException();

        foreach (var businessRule in rules)
        {
            if (businessRule.IsBroken())
            {
                exception.AddError(businessRule.Message);
            }
        }

        if (exception.Errors.Any())
        {
            throw exception;
        }
    }
}