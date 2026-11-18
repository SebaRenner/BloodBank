namespace BloodBank.Domain.Common;

public abstract class AggregateRoot
{
    private readonly List<DomainEvent> _events = [];

    public IReadOnlyList<DomainEvent> Events => _events;

    protected void AddEvent(DomainEvent domainEvent) => _events.Add(domainEvent);
}