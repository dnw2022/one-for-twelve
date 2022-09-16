using System;
using Dnw.OneForTwelve.Core.Models;
using Dnw.OneForTwelve.Core.Services;
using NSubstitute;
using Xunit;

namespace Dnw.OneForTwelve.Core.UnitTests.Services;

public class GameServiceTests
{
    private readonly IDutchRandomGameFactory _dutchRandomGameFactory;
    private readonly IDemoGameFactory _dutchDemoGameFactory;
    private readonly IDemoGameFactory _englishDemoGameFactory;

    private readonly GameService _gameService;
    
    public GameServiceTests()
    {
        _dutchRandomGameFactory = Substitute.For<IDutchRandomGameFactory>();
        
        _dutchDemoGameFactory = Substitute.For<IDemoGameFactory>();
        _dutchDemoGameFactory.Language.Returns(Languages.Dutch);
        
        _englishDemoGameFactory = Substitute.For<IDemoGameFactory>();
        _englishDemoGameFactory.Language.Returns(Languages.English);
        
        var demoGameFactories = new[] {_dutchDemoGameFactory, _englishDemoGameFactory};
        
        _gameService = new GameService(_dutchRandomGameFactory, demoGameFactories);
    }
    
    [Fact]
    public void Start_Dutch_Demo()
    {
        // Given
        const Languages language = Languages.Dutch;
        const QuestionSelectionStrategies strategy = QuestionSelectionStrategies.Demo;
        
        var expected = new Game("", Array.Empty<GameQuestion>());
        _dutchDemoGameFactory.Language.Returns(Languages.Dutch);
        _dutchDemoGameFactory.GetGame().Returns(expected);

        // When
        var actual = _gameService.Start(language, strategy);

        // Then
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Start_English_Demo()
    {
        // Given
        const Languages language = Languages.English;
        const QuestionSelectionStrategies strategy = QuestionSelectionStrategies.Demo;
        
        var expected = new Game("", Array.Empty<GameQuestion>());
        _englishDemoGameFactory.Language.Returns(Languages.English);
        _englishDemoGameFactory.GetGame().Returns(expected);

        // When
        var actual = _gameService.Start(language, strategy);

        // Then
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Start_Dutch_Random()
    {
        // Given
        const Languages language = Languages.Dutch;
        const QuestionSelectionStrategies strategy = QuestionSelectionStrategies.Random;
        
        var expected = new Game("", Array.Empty<GameQuestion>());
        _dutchRandomGameFactory.Get(strategy).Returns(expected);

        // When
        var actual = _gameService.Start(language, strategy);

        // Then
        Assert.Equal(expected, actual);
    }
}