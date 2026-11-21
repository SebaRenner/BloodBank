using BloodBank.Domain.Common;

namespace BloodBank.Domain.Donations.Events;

public record DonationTestPassed(Guid DonationId) : DomainEvent(DonationId);
