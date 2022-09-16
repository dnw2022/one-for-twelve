using System;
using System.Linq;
using Dnw.OneForTwelve.Core.Extensions;
using Dnw.OneForTwelve.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Dnw.OneForTwelve.Core.UnitTests.Extensions;

public class GameServiceExtensionsTests
{
    [Theory]
    [InlineData(typeof(IWordCache), typeof(WordCache))]
    [InlineData(typeof(IQuestionCache), typeof(QuestionCache))]
    [InlineData(typeof(IGameService), typeof(GameService))]
    [InlineData(typeof(IQuestionSelectorFactory), typeof(QuestionSelectorFactory))]
    [InlineData(typeof(IGameQuestionShuffler), typeof(GameQuestionShuffler))]
    [InlineData(typeof(IRandomService), typeof(RandomService))]
    [InlineData(typeof(IItemPicker), typeof(ItemPicker))]
    [InlineData(typeof(IQuestionSelectorHelper), typeof(QuestionSelectorHelper))]
    public void AddGameServices_SingleRegistration(Type interfaceType, Type expected)
    {
        // Given
        var services = new ServiceCollection();

        // When
        services.AddGameServices();

        // Then
        var provider = services.BuildServiceProvider();
        var actual = provider.GetRequiredService(interfaceType);
        
        Assert.IsType(expected, actual);
    }
    
    [Fact]
    public void AddGameServices_DemoGameFactory()
    {
        // Given
        var services = new ServiceCollection();

        // When
        services.AddGameServices();

        // Then
        var provider = services.BuildServiceProvider();
        var actual = provider.GetServices<IDemoGameFactory>().ToList();

        var expected = new[] {typeof(DutchDemoGameFactory), typeof(EnglishDemoGameFactory)};
        CustomAssert.AreTypes(expected, actual);
    }
    
    [Fact]
    public void AddGameServices_QuestionSelector()
    {
        // Given
        var services = new ServiceCollection();

        // When
        services.AddGameServices();

        // Then
        var provider = services.BuildServiceProvider();
        var actual = provider.GetServices<IQuestionSelector>().ToList();

        var expected = new[] {typeof(RandomQuestionSelector), typeof(RandomOnlyEasyQuestionSelector)};
        CustomAssert.AreTypes(expected, actual);
    }
}