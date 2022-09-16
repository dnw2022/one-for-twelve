using System;
using Dnw.OneForTwelve.Core.Models;
using Dnw.OneForTwelve.Core.Services;
using NSubstitute;
using Xunit;

namespace Dnw.OneForTwelve.Core.UnitTests.Services;

public class QuestionSelectorFactoryTests
{
    [Fact]
    public void Create_SelectorFound()
    {
        // Given
        var expected = Substitute.For<IQuestionSelector>();
        expected.Strategy.Returns(QuestionSelectionStrategies.Random);
        
        var questionSelectors = new [] { expected };
        
        var factory = new QuestionSelectorFactory(questionSelectors);

        // When
        var actual = factory.Create(QuestionSelectionStrategies.Random);

        // Then
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Create_SelectorNotFound()
    {
        // Given
        var expected = Substitute.For<IQuestionSelector>();
        expected.Strategy.Returns(QuestionSelectionStrategies.RandomOnlyEasy);
        
        var questionSelectors = new [] { expected };
        
        var factory = new QuestionSelectorFactory(questionSelectors);

        // When
        var actual = Assert.Throws<ArgumentException>(() => factory.Create(QuestionSelectionStrategies.Random));

        // Then
        Assert.IsType<ArgumentException>(actual);
    }
}