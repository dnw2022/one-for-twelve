using System;
using System.Linq;
using Dnw.OneForTwelve.Core.Models;
using Dnw.OneForTwelve.Core.Services;
using Dnw.OneForTwelve.Core.UnitTests.Utils;
using NSubstitute;
using Xunit;

namespace Dnw.OneForTwelve.Core.UnitTests.Services;

public class GameQuestionShufflerTests
{
    [Fact]
    public void ShuffleQuestions()
    {
        // Given
        var randomService = Substitute.For<IRandomService>();
        // The normal implementation of IRandomService returns a random number exclusive the upper bound
        // If we would use that one here, some questions would remain in the correct position in the word
        // To prevent that we change the implementation so that swapping always takes place
        // In that way we can simply test if all questions are not in correct place anymore after shuffling 
        randomService
            .When(x => x.Next(0, Arg.Any<int>())
            .Returns(y => Random.Shared.Next(0, y.ArgAt<int>(1))));
        
        var shuffler = new GameQuestionShuffler(randomService);
        var testQuestionBuilder = new TestQuestionBuilder();
        var gameQuestions = Enumerable.Range(0, 12).Select(i => new GameQuestion(i, i, testQuestionBuilder.Build())).ToList();

        // When
        shuffler.ShuffleQuestions(gameQuestions);

        // Then
        foreach (var gameQuestion in gameQuestions)
        {
            Assert.NotEqual(gameQuestion.Number, gameQuestion.WordPosition);
        }
    }
}