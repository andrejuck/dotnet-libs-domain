using Libs.Domain.Model;

namespace Libs.Domain.DomainEvent;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEventsAsync(IEnumerable<BaseEntity> aggregatesWithEvents, CancellationToken cancellationToken = default);
}