namespace BloodBank.Domain.Events;

public abstract record DomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();

    public DateTimeOffset OccuredAt { get; } = DateTimeOffset.UtcNow;
}
