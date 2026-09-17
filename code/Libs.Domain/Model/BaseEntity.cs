using Libs.Domain.DomainEvent;

namespace Libs.Domain.Model;

public abstract class BaseEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public virtual void SetUpdatedAt()
    {
        UpdatedAt = DateTime.Now;
    }

    public virtual void SetDeletedAt()
    {
        DeletedAt = DateTime.Now;
        SetUpdatedAt();
    }

    public void AddDomainEvent(IDomainEvent @event)
    {
        _domainEvents.Add(@event);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}