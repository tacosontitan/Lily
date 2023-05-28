using Lily.Community.Games;

namespace Lily.Community.Tests.Games;

public class CoinFlipCommandTests
{
    [Theory]
    [InlineData(0, CoinFlipResult.Side)]
    [InlineData(1, CoinFlipResult.Heads)]
    [InlineData(49, CoinFlipResult.Heads)]
    [InlineData(50, CoinFlipResult.Tails)]
    [InlineData(100, CoinFlipResult.Tails)]
    public void GetFlipResult_ReturnsExpectedResult(int flipNumber, CoinFlipResult expectedResult)
    {
        // Arrange
        var random = Substitute.For<Random>();
        var logger = Substitute.For<ILogger<CoinFlipCommand>>();
        var command = new CoinFlipCommand(random, logger);

        // Act
        CoinFlipResult result = command.GetFlipResult(flipNumber);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void GetFlipResult_ThrowsArgumentOutOfRangeException_WhenFlipNumberIsOutOfRange(int flipNumber)
    {
        // Arrange
        var random = Substitute.For<Random>();
        var logger = Substitute.For<ILogger<CoinFlipCommand>>();
        var command = new CoinFlipCommand(random, logger);

        // Act
        Exception exception = Record.Exception(() => command.GetFlipResult(flipNumber));

        // Assert
        Assert.IsType<ArgumentOutOfRangeException>(exception);
    }
}
