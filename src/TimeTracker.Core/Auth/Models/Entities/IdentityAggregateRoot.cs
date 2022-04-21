using TimeTracker.Core.Shared.Exceptions;
using TimeTracker.Core.Shared.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace TimeTracker.Core.Auth.Models.Entities
{
    public abstract class IdentityAggregateRoot : IdentityUser, IAuditable
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
        public abstract DateTime CreatedTime { get; set; }
        public abstract string CreatedBy { get; set; }
        public abstract DateTime LastModifiedTime { get; set; }
        public abstract string LastModifiedBy { get; set; }
    }
}