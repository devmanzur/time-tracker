using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TimeTracker.Core.Shared.Interfaces
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync(List<EntityEntry<AggregateRoot>> changes);
        void DispatchEvents(List<EntityEntry<AggregateRoot>> changes);
    }
}