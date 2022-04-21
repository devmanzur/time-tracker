using TimeTracker.Core.Auth.Models.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TimeTracker.Core.Auth.Interfaces
{
    public interface IIdentityDomainEventsDispatcher
    {
        Task DispatchEventsAsync(List<EntityEntry<IdentityAggregateRoot>> changes);
        void DispatchEvents(List<EntityEntry<IdentityAggregateRoot>> changes);
    }
}