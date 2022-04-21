namespace TimeTracker.Core.Shared.Interfaces;

public interface IBusinessRule
{
    bool IsBroken();

    string Message { get; }
}