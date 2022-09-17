using System;
using System.Collections.Generic;
using System.Linq;
using Dnw.OneForTwelve.Core.Models;
using Dnw.OneForTwelve.Core.Services;
using Dnw.OneForTwelve.Core.UnitTests.Utils;
using NSubstitute;
using Xunit;

namespace Dnw.OneForTwelve.Core.UnitTests.Services;


/// <summary>
/// This really represents the core complexity of the game engine, so most of the testing effort has to go here.
///
/// At the moment the QuestionSelector class is too complex, which makes testing hard.
///
/// Some ideas to improve:
///
/// 1) Is QuestionsPicker a better name?
/// 2) Can we delegate the task of selecting a single question to a different class (QuestionPicker)?
/// </summary>
public class QuestionSelectorHelperTests
{
    [Fact]
    public void GetQuestions()
    {
        // Given
        const string singleLetterWord = "x";
        const QuestionCategories category = QuestionCategories.Geography;
        const QuestionLevels level = QuestionLevels.Easy;

        var questionCache = Substitute.For<IQuestionCache>();
        var testQuestionBuilder = new TestQuestionBuilder();
        var question = testQuestionBuilder.WithAnswer(singleLetterWord).Build();
        questionCache
            .GetRandom(singleLetterWord.ToUpper(), category, level, Arg.Any<HashSet<int>>())
            .Returns(question);

        var categories = new [] {QuestionCategories.Art, QuestionCategories.History };
        var itemPicker = Substitute.For<IItemPicker>();
        itemPicker
            .PickRandom(Arg.Is<List<QuestionCategories>>(_ => _.SequenceEqual(categories)))
            .Returns(category);

        var levels = new [] {QuestionLevels.Easy, QuestionLevels.Hard };
        itemPicker
            .PickRandom(Arg.Is<List<QuestionLevels>>(_ => _.SequenceEqual(levels)))
            .Returns(level);
        
        var selector = new QuestionSelectorHelper(questionCache, itemPicker);

        // When
        var actual = selector.GetQuestions(singleLetterWord, categories, levels);

        // Then
        Assert.Single(actual);
    }
    
    [Fact]
    public void GetQuestions_NoQuestions()
    {
        // Given
        const string word = "aWord";
        
        var selector = new QuestionSelectorHelper(Substitute.For<IQuestionCache>(), Substitute.For<IItemPicker>());

        // When
        var actual = selector.GetQuestions(word, Array.Empty<QuestionCategories>(), Array.Empty<QuestionLevels>());

        // Then
        Assert.Empty(actual);
    }
    
    
}