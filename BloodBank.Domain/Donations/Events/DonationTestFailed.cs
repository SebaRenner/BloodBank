using BloodBank.Domain.Common;

namespace BloodBank.Domain.Donations.Events;

public record DonationTestFailed(Guid DonationId) : DomainEvent(DonationId)
{
}
