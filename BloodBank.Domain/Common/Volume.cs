namespace BloodBank.Domain.Common;

public record Volume
{
    public int Milliliters { get; }

    public Volume(int milliliters)
    {
        if (milliliters <= 0)
            throw new ArgumentOutOfRangeException("Volume must be positive.");

        Milliliters = milliliters;
    }
}