using BloodBank.Domain.Common;

namespace BloodBank.Domain.Donations.Events;

public record DonationRegistered(Guid DonationId, Guid DonorId, DateTimeOffset CollectedAt, Volume Volume) : DomainEvent(DonationId);
