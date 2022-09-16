using System.Collections.Generic;
using System.Linq;
using Dnw.OneForTwelve.Core.Models;
using Dnw.OneForTwelve.Core.Services;
using NSubstitute;
using Xunit;

namespace Dnw.OneForTwelve.Core.UnitTests.Services;

public class RandomQuestionSelectorTests
{
    [Fact]
    public void Strategy()
    {
        // Given
        var selector = new RandomQuestionSelector(Substitute.For<IQuestionSelectorHelper>());
        
        // Then
        Assert.Equal(QuestionSelectionStrategies.Random, selector.Strategy);
    }
    
    [Fact]
    public void GetQuestions()
    {
        // Given
        const string word = "aWord";
        
        var helper = Substitute.For<IQuestionSelectorHelper>();
        var expected = new List<GameQuestion>();
        var expectedCategories = RandomQuestionSelector.Categories;
        var expectedLevels = RandomQuestionSelector.Levels;
        helper
            .GetQuestions(
                word, 
                Arg.Is<QuestionCategories[]>(_ => _.SequenceEqual(expectedCategories)), 
                Arg.Is<QuestionLevels[]>(_ => _.SequenceEqual(expectedLevels)))
            .Returns(expected);
        
        var selector = new RandomQuestionSelector(helper);

        // When
        var actual = selector.GetQuestions(word);

        // Then
        Assert.Equal(expected, actual);
    }
}