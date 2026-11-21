using BloodBank.Domain.Donations;
using BloodBank.Domain.Donations.Events;

namespace BloodBank.Domain.Tests.Donations;

public class DonationTest
{
    [Fact]
    public void Test_Donation_Register()
    {
        // Arrange
        var donorId = Guid.NewGuid();
        var collectedAt = new DateTimeOffset(2026, 11, 21, 12, 0, 0, TimeSpan.Zero);

        // Act
        var donation = Donation.Register(donorId, collectedAt);

        // Assert
        Assert.Equal(donorId, donation.DonorId);
        Assert.Equal(collectedAt, donation.CollectedAt);
        Assert.Equal(DonationStatus.Registered, donation.Status);
        Assert.Single(donation.Events);
        Assert.IsType<DonationRegistered>(donation.Events.First());
    }

    [Fact]
    public void Test_Donation_Discard()
    {
        // Arrange
        var donorId = Guid.NewGuid();
        var collectedAt = new DateTimeOffset(2026, 11, 21, 12, 0, 0, TimeSpan.Zero);
        var donation = Donation.Register(donorId, collectedAt);

        // Act
        donation.Discard();
        
        // Assert
        Assert.Equal(DonationStatus.Discarded, donation.Status);
        Assert.Equal(2, donation.Events.Count);
        Assert.IsType<DonationDiscarded>(donation.Events.Last());
    }
}
