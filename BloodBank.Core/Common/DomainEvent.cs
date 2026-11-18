namespace BloodBank.Domain.Common;

public abstract record DomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();

    public DateTimeOffset OccurredAt { get; } = DateTimeOffset.UtcNow;
}
