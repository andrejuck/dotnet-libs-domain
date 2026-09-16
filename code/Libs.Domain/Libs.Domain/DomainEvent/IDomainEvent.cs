namespace Libs.Domain.DomainEvent;

public interface IDomainEvent
{
    public Guid EventId => Guid.NewGuid();
    DateTimeOffset OccurredOn => DateTimeOffset.Now;
}