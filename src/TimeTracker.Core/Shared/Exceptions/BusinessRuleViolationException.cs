using TimeTracker.Core.Shared.Interfaces;

namespace TimeTracker.Core.Shared.Exceptions;

public class BusinessRuleViolationException : BaseApplicationException
{
    public List<string> Errors { get; private set; } = new List<string>();
    public BusinessRuleViolationException(IBusinessRule brokenRule) : base(
        911, new Exception(brokenRule.Message))
    {
    }

    public BusinessRuleViolationException(string message = "Invalid input, Please fix the errors and try again") : base(911, new Exception(message))
    {
            
    }

    public void AddError(string error)
    {
        Errors.Add(error);
    }

    public override bool Equals(object obj)
    {
        var otherException = obj as BusinessRuleViolationException;

        return otherException.Message == this.Message
               && HasSameErrors(otherException);
    }

    private bool HasSameErrors(BusinessRuleViolationException otherException)
    {
            
        if (!this.Errors.Any())
        {
            return !otherException.Errors.Any();
        }


        if (this.Errors.Count == otherException.Errors.Count)
        {
            for (int i = 0; i < this.Errors.Count; i++)
            {
                if (this.Errors[i] != otherException.Errors[i])
                {
                    return false;
                }
            }

            return true;
        }


        return false;
    }
}