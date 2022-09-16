using System.Collections.Generic;
using Dnw.OneForTwelve.Core.Models;
using Dnw.OneForTwelve.Core.Services;
using NSubstitute;
using Xunit;

namespace Dnw.OneForTwelve.Core.UnitTests.Services;

public class DutchRandomGameFactoryTests
{
    [Fact]
    public void Get()
    {
        // Given
        const QuestionSelectionStrategies strategy = QuestionSelectionStrategies.Random;
        
        var wordCache = Substitute.For<IWordCache>();
        const string word = "aWord";
        wordCache.GetRandom().Returns(word);

        var questionSelector = Substitute.For<IQuestionSelector>();
        var questions = new List<GameQuestion>();
        questionSelector.GetQuestions(word).Returns(questions);
        
        var questionSelectorFactory = Substitute.For<IQuestionSelectorFactory>();
        questionSelectorFactory.Create(strategy).Returns(questionSelector);
        
        var questionShuffler = Substitute.For<IGameQuestionShuffler>();
        
        var factory = new DutchRandomGameFactory(wordCache, questionSelectorFactory, questionShuffler);

        // When
        var actual = factory.Get(strategy);

        // Then
        questionShuffler.Received(1).ShuffleQuestions(questions);
        Assert.Equal(word, actual.Word);
        Assert.Equal(questions, actual.Questions);
    }
}