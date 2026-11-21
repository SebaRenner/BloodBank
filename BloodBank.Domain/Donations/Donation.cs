using BloodBank.Domain.Common;
using BloodBank.Domain.Donations.Events;

namespace BloodBank.Domain.Donations;

public class Donation : AggregateRoot
{
    public Guid DonorId { get; private set; }

    public DateTimeOffset CollectedAt { get; private set; }

    public DonationStatus Status { get; private set; }

    private Donation() {}

    public static Donation Register(Guid donorId, DateTimeOffset collectedAt)
    {
        var donation = new Donation();

        donation.AddEvent(new DonationRegistered(Guid.NewGuid(), donorId, collectedAt));

        return donation;
    }

    public void Test()
    {
        if (Status != DonationStatus.Registered)
        {
            throw new InvalidOperationException("Donation must be in 'Registered' status to be tested.");
        }

        // Test logic here or in domain service?!

        // Add event pass/fail test
    }

    public void Discard()
    {
        if (Status == DonationStatus.Discarded || Status == DonationStatus.Split)
        {
            throw new InvalidOperationException("Donation cannot be discarded if already discarded or split.");
        }

        AddEvent(new DonationDiscarded(Id));
    }

    protected override void Apply(DomainEvent domainEvent)
    {
        switch (domainEvent)
        {
            case DonationRegistered e: On(e); break;
            case DonationDiscarded e: On(e); break;
            default: throw new InvalidOperationException($"Unhandled event {domainEvent.GetType().Name}");
        }
    }

    private void On(DonationRegistered e)
    {
        Id = e.DonationId;
        DonorId = e.DonorId;
        CollectedAt = e.CollectedAt;
        Status = DonationStatus.Registered;
    }

    private void On(DonationDiscarded _)
    {
        Status = DonationStatus.Discarded;
    }
}
