using BloodBank.Domain.Common;
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
        var volume = new Volume(450);

        // Act
        var donation = Donation.Register(donorId, collectedAt, volume);

        // Assert
        Assert.Equal(donorId, donation.DonorId);
        Assert.Equal(collectedAt, donation.CollectedAt);
        Assert.Equal(volume, donation.Volume);
        Assert.Equal(DonationStatus.Registered, donation.Status);
        Assert.Single(donation.Events);
        Assert.IsType<DonationRegistered>(donation.Events.First());
    }

    [Fact]
    public void Test_Donation_SendForTesting()
    {
        // Arrange
        var donation = CreateDonation();

        // Act
        donation.SendForTesting();

        // Assert
        Assert.Equal(DonationStatus.SentForTesting, donation.Status);
        Assert.Equal(2, donation.Events.Count);
        Assert.IsType<DonationSentForTesting>(donation.Events.Last());
    }

    [Fact]
    public void Test_Donation_SendForTesting_AlreadySent()
    {
        // Arrange
        var donation = CreateDonation();
        donation.SendForTesting();

        // Act
        var act = () => donation.SendForTesting();

        // Assert
        Assert.Throws<InvalidOperationException>(act);
        Assert.Equal(2, donation.Events.Count);
    }

    [Fact]
    public void Test_Donation_TestPassed()
    {
        // Arrange
        var bloodGroup = BloodGroup.OPositive;
        var donation = CreateDonation();
        donation.SendForTesting();

        // Act
        donation.TestPassed(bloodGroup);

        // Assert
        Assert.Equal(DonationStatus.TestPassed, donation.Status);
        Assert.Equal(3, donation.Events.Count);
        Assert.IsType<DonationTestPassed>(donation.Events.Last());
        Assert.Equal(bloodGroup, donation.BloodGroup);
    }

    [Fact]
    public void Test_Donation_TestPassed_NotSentForTesting()
    {
        // Arrange
        var bloodGroup = BloodGroup.OPositive;
        var donation = CreateDonation();

        // Act
        var act = () => donation.TestPassed(bloodGroup);

        // Assert
        Assert.Throws<InvalidOperationException>(act);
        Assert.Single(donation.Events);
    }

    [Fact]
    public void Test_Donation_TestFailed()
    {
        // Arrange
        var donation = CreateDonation();
        donation.SendForTesting();

        // Act
        donation.TestFailed("HIV reactive");

        // Assert
        Assert.Equal(DonationStatus.TestFailed, donation.Status);
        Assert.Equal(3, donation.Events.Count);
        var failed = Assert.IsType<DonationTestFailed>(donation.Events.Last());
        Assert.Equal("HIV reactive", failed.Reason);
    }

    [Fact]
    public void Test_Donation_TestFailed_NotSentForTesting()
    {
        // Arrange
        var donation = CreateDonation();

        // Act
        var act = () => donation.TestFailed("HIV reactive");

        // Assert
        Assert.Throws<InvalidOperationException>(act);
        Assert.Single(donation.Events);
    }

    [Fact]
    public void Test_Donation_Discard()
    {
        // Arrange
        var donation = CreateDonation();

        // Act
        donation.Discard();
        
        // Assert
        Assert.Equal(DonationStatus.Discarded, donation.Status);
        Assert.Equal(2, donation.Events.Count);
        Assert.IsType<DonationDiscarded>(donation.Events.Last());
    }

    [Fact]
    public void Test_Donation_Discard_AlreadyDiscarded()
    {
        // Arrange
        var donation = CreateDonation();
        donation.Discard();

        // Act
        var act = () => donation.Discard();

        // Assert
        Assert.Throws<InvalidOperationException>(act);
        Assert.Equal(2, donation.Events.Count);
    }

    private static Donation CreateDonation()
    {
        var donorId = Guid.NewGuid();
        var collectedAt = new DateTimeOffset(2026, 11, 21, 12, 0, 0, TimeSpan.Zero);
        var volume = new Volume(450);
        return Donation.Register(donorId, collectedAt, volume);
    }
}
