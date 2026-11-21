using BloodBank.Domain.Common;

namespace BloodBank.Domain.Tests.Common;

public class VolumeTest
{
    [Fact]
    public void Test_Volume()
    {
        // Arrange
        var milliliters = 450;

        // Act
        var volume = new Volume(milliliters);

        // Assert
        Assert.Equal(milliliters, volume.Milliliters);  
    }

    [Fact]
    public void Test_Volume_NegativeInput()
    {
        // Arrange
        var milliliters = -100;

        // Act
        var act = () => new Volume(milliliters);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(act);
    }
}
