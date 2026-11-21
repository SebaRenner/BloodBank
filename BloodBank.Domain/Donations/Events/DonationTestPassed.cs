using BloodBank.Domain.Common;

namespace BloodBank.Domain.Donations.Events;

public record DonationTestPassed(Guid DonationId, BloodGroup BloodGroup) : DomainEvent(DonationId);
