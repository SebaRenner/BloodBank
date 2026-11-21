namespace BloodBank.Domain.Common;

public abstract record DomainEvent(Guid AggregateId)
{
    public Guid EventId { get; } = Guid.NewGuid();

    public DateTimeOffset OccurredAt { get; } = DateTimeOffset.UtcNow;
}
