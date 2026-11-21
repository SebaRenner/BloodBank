using BloodBank.Domain.Common;

namespace BloodBank.Domain.Donations.Events;

public record DonationDiscarded(Guid DonationId) : DomainEvent(DonationId);
