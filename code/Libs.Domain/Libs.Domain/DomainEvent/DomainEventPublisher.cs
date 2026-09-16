using Microsoft.Extensions.DependencyInjection;

namespace Libs.Domain.DomainEvent;

public sealed class DomainEventPublisher(IServiceProvider serviceProvider) : IDomainEventPublisher
{
    public async Task PublishAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var eventType = domainEvent.GetType();
        var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(eventType);

        var handlers = serviceProvider.GetServices(handlerType);

        foreach (var handler in handlers)
        {
            if (handler is null)
                continue;

            var handleMethod = handlerType.GetMethod(nameof(IDomainEventHandler<IDomainEvent>.HandleAsync))
                               ?? throw new InvalidOperationException($"HandleAsync não encontrado em {handlerType.Name}");

            var task = (Task)handleMethod.Invoke(handler, [domainEvent, cancellationToken])!;
            await task;
        }
    }
}