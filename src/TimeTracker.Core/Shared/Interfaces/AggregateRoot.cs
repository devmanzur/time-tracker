using TimeTracker.Core.Shared.Exceptions;

namespace TimeTracker.Core.Shared.Interfaces;

public abstract class AggregateRoot : BaseEntity
{
    private List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();


    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    public void CheckRules(params IBusinessRule[] rules)
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