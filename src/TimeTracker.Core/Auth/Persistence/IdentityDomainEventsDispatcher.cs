using TimeTracker.Core.Auth.Interfaces;
using TimeTracker.Core.Auth.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TimeTracker.Core.Auth.Persistence
{
    public class IdentityDomainEventsDispatcher : IIdentityDomainEventsDispatcher
    {
        private readonly IMediator _mediator;

        public IdentityDomainEventsDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task DispatchEventsAsync(List<EntityEntry<IdentityAggregateRoot>> changes)
        {
            var domainEvents = changes
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            ClearRecords(changes);

            var tasks = domainEvents
                .Select(async domainEvent => { await _mediator.Publish(domainEvent); });
            await Task.WhenAll(tasks);
        }


        public void DispatchEvents(List<EntityEntry<IdentityAggregateRoot>> changes)
        {
            var domainEvents = changes
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            ClearRecords(changes);

            var tasks = domainEvents
                .Select(async domainEvent => { await _mediator.Publish(domainEvent); });
             Task.WhenAll(tasks).RunSynchronously();
        }

        private static void ClearRecords(List<EntityEntry<IdentityAggregateRoot>> changes)
        {
            changes
                .ForEach(entity =>
                {
                    entity.Entity.ClearDomainEvents();
                    entity.State = EntityState.Detached;
                });
        }
    }
}