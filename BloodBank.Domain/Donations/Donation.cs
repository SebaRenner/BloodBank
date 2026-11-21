using BloodBank.Domain.Common;
using BloodBank.Domain.Donations.Events;

namespace BloodBank.Domain.Donations;

public class Donation : AggregateRoot
{
    public Guid DonorId { get; private set; }

    public DateTimeOffset CollectedAt { get; private set; }

    public DonationStatus Status { get; private set; }

    public BloodGroup? BloodGroup { get; private set; }

    private Donation() {}

    public static Donation Register(Guid donorId, DateTimeOffset collectedAt)
    {
        var donation = new Donation();

        donation.AddEvent(new DonationRegistered(Guid.NewGuid(), donorId, collectedAt));

        return donation;
    }

    public void SendForTesting()
    {
        if (Status != DonationStatus.Registered)
            throw new InvalidOperationException($"Donation: {Id} must be registered to be sent for testing, but is {Status}.");

        AddEvent(new DonationSentForTesting(Id));
    }

    public void TestPassed(BloodGroup bloodGroup)
    {
        if (Status != DonationStatus.SentForTesting)
            throw new InvalidOperationException($"Donation: {Id} must be marked as sent for testing, but is {Status}.");

        AddEvent(new DonationTestPassed(Id, bloodGroup));
    }

    public void TestFailed(string reason)
    {
        if (Status != DonationStatus.SentForTesting)
            throw new InvalidOperationException($"Donation: {Id} must be marked as sent for testing, but is {Status}.");

        AddEvent(new DonationTestFailed(Id, reason));
    }

    public void Discard()
    {
        if (Status == DonationStatus.Discarded || Status == DonationStatus.Split)
            throw new InvalidOperationException($"Donation: {Id} cannot be discarded if already discarded or split.");

        AddEvent(new DonationDiscarded(Id));
    }

    protected override void Apply(DomainEvent domainEvent)
    {
        switch (domainEvent)
        {
            case DonationRegistered e: On(e); break;
            case DonationSentForTesting e: On(e); break;
            case DonationTestPassed e: On(e); break;
            case DonationTestFailed e: On(e); break;
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

    private void On(DonationSentForTesting _)
    {
        Status = DonationStatus.SentForTesting;
    }

    private void On(DonationTestPassed e)
    {
        Status = DonationStatus.TestPassed;
        BloodGroup = e.BloodGroup;
    }

    private void On(DonationTestFailed _)
    {
        Status = DonationStatus.TestFailed;
    }

    private void On(DonationDiscarded _)
    {
        Status = DonationStatus.Discarded;
    }
}
