using Dnw.OneForTwelve.Core.Services;
using Xunit;

namespace Dnw.OneForTwelve.Core.UnitTests.Services;

public class RandomServiceTests
{
    [Fact]
    public void Next()
    {
        // Given
        var randomService = new RandomService();

        // When
        var actual = randomService.Next(0, 10);

        // Then
        Assert.InRange(actual, 0, 9);
    }
}