namespace BloodBank.Domain.Common;

public abstract class AggregateRoot
{
    private readonly List<DomainEvent> _events = [];

    public Guid Id { get; protected set; }

    public IReadOnlyList<DomainEvent> Events => _events;

    protected void AddEvent(DomainEvent domainEvent) {
        Apply(domainEvent);
        _events.Add(domainEvent);
    } 

    protected abstract void Apply(DomainEvent domainEvent);
}