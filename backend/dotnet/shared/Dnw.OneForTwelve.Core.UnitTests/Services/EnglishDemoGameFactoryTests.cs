using Dnw.OneForTwelve.Core.Models;
using Dnw.OneForTwelve.Core.Services;
using Xunit;
// ReSharper disable StringLiteralTypo

namespace Dnw.OneForTwelve.Core.UnitTests.Services;

public class EnglishDemoGameFactoryTests
{
    [Fact]
    public void GetGame()
    {
        // Given
        var factory = new EnglishDemoGameFactory();

        // When
        var game = factory.GetGame();

        // Then
        Assert.Equal(Languages.English, factory.Language);
        Assert.Equal(12, game.Word.Length);
    }
}