using BloodBank.Domain.Common;

namespace BloodBank.Domain.Donations.Events;

public record DonationSentForTesting(Guid DonationId) : DomainEvent(DonationId);
