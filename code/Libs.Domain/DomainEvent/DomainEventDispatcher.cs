using Libs.Domain.Model;

namespace Libs.Domain.DomainEvent;

public sealed class DomainEventDispatcher(IDomainEventPublisher publisher) : IDomainEventDispatcher
{
    public async Task DispatchAndClearEventsAsync(IEnumerable<BaseEntity> aggregatesWithEvents, CancellationToken cancellationToken = default)
    {
        foreach (var aggregate in aggregatesWithEvents)
        {
            var events = aggregate.DomainEvents.ToArray();
            aggregate.ClearDomainEvents();

            foreach (var domainEvent in events)
                await publisher.PublishAsync(domainEvent, cancellationToken);
        }
    }
}