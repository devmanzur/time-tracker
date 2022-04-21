namespace TimeTracker.Core.Shared.Exceptions;

public abstract class BaseApplicationException : Exception
{
    public int UserFriendlyCode { get; private set; }
    public BaseApplicationException(int userFriendlyCode, Exception actualException) :
        base(actualException.Message, actualException)
    {
        UserFriendlyCode = userFriendlyCode;
    }
}