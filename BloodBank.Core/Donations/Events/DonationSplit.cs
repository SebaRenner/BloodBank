using BloodBank.Domain.Common;

namespace BloodBank.Domain.Donations.Events;

public record DonationSplit(Guid DonationId) : DomainEvent(DonationId)
{
}
