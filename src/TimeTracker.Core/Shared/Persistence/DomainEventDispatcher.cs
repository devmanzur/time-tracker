using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TimeTracker.Core.Shared.Interfaces;

namespace TimeTracker.Core.Shared.Persistence;

public class DomainEventDispatcher : IDomainEventsDispatcher
{
    private readonly IMediator _mediator;

    public DomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }


    public async Task DispatchEventsAsync(List<EntityEntry<AggregateRoot>> changes)
    {
        var domainEvents = changes
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        ClearRecords(changes);

        var tasks = domainEvents
            .Select(async domainEvent => { await _mediator.Publish(domainEvent); });
        await Task.WhenAll(tasks);
    }


    public void DispatchEvents(List<EntityEntry<AggregateRoot>> changes)
    {
        var domainEvents = changes
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        ClearRecords(changes);

        var tasks = domainEvents
            .Select(async domainEvent => { await _mediator.Publish(domainEvent); });
        Task.WhenAll(tasks).RunSynchronously();
    }

    private static void ClearRecords(List<EntityEntry<AggregateRoot>> changes)
    {
        changes
            .ForEach(entity =>
            {
                entity.Entity.ClearDomainEvents();
                entity.State = EntityState.Detached;
            });
    }
}