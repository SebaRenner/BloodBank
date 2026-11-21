namespace BloodBank.Domain.Donations;

public enum DonationStatus
{
    Registered,
    SentForTesting,
    TestPassed,
    TestFailed,
    Split,
    Discarded,
}
