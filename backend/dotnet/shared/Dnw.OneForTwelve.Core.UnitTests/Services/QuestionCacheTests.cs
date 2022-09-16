using System.Collections.Generic;
using System.Linq;
using Dnw.OneForTwelve.Core.Models;
using Dnw.OneForTwelve.Core.Services;
using Dnw.OneForTwelve.Core.UnitTests.Utils;
using NSubstitute;
using Xunit;

namespace Dnw.OneForTwelve.Core.UnitTests.Services;

public class QuestionCacheTests
{
    private readonly IFileService _fileService;
    private readonly IRandomService _randomService;
    
    private readonly QuestionCache _questionCache;
    
    private readonly TestQuestionBuilder _normalGeographyQuestionBuilder;
    private readonly TestQuestionBuilder _hardBiologyQuestionBuilder;
    private readonly TestQuestionBuilder _easyLiteratureQuestionBuilder;
    
    public QuestionCacheTests()
    {
        _fileService = Substitute.For<IFileService>();
        _randomService = Substitute.For<IRandomService>();
        _questionCache = new QuestionCache(_fileService, _randomService);
        
        _normalGeographyQuestionBuilder = new TestQuestionBuilder().WithCategory(QuestionCategories.Geography).WithLevel(QuestionLevels.Normal);
        _hardBiologyQuestionBuilder = new TestQuestionBuilder().WithCategory(QuestionCategories.Biology).WithLevel(QuestionLevels.Hard);
        _easyLiteratureQuestionBuilder =  new TestQuestionBuilder().WithCategory(QuestionCategories.Literature).WithLevel(QuestionLevels.Easy);
    }
    
    [Fact]
    public void GetRandom()
    { 
        // Given
        var expected = _normalGeographyQuestionBuilder.Build();

        var possibleQuestions = new List<Question>();
        possibleQuestions.AddRange(Enumerable.Range(0, 10).Select(_ => _normalGeographyQuestionBuilder.Build()));
        possibleQuestions.Add(expected);
        possibleQuestions.AddRange(Enumerable.Range(0, 10).Select(_ => _normalGeographyQuestionBuilder.Build()));
        
        var allQuestions = new List<Question>();
        allQuestions.AddRange(Enumerable.Range(0, 10).Select(_ => _hardBiologyQuestionBuilder.Build()));
        allQuestions.AddRange(possibleQuestions);
        allQuestions.AddRange(Enumerable.Range(0, 10).Select(_ => _easyLiteratureQuestionBuilder.Build()));

        _fileService
            .GetQuestions()
            .Returns(allQuestions);
        
        _randomService
            .Next(0, possibleQuestions.Count)
            .Returns(possibleQuestions.IndexOf(expected));

        // When
        _questionCache.Init();
        var actual = _questionCache.GetRandom(expected.FirstLetterAnswer, expected.Category, expected.Level, new HashSet<int>());

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetRandom_NoQuestionsForFirstLetterAnswer()
    {
        // Given
        _fileService.GetQuestions().Returns(new List<Question>());

        // When
        _questionCache.Init();
        var actual = _questionCache.GetRandom("A", QuestionCategories.Biology, QuestionLevels.Hard, new HashSet<int>());

        // Then
        Assert.Null(actual);
    }
    
    [Fact]
    public void GetRandom_NoPossibleQuestionsWithCategoryAndLevel()
    {
        // Given
        var allQuestions = new List<Question>
        {
            _hardBiologyQuestionBuilder.WithCategory(QuestionCategories.Economy).WithLevel(QuestionLevels.Hard).Build(),
            _hardBiologyQuestionBuilder.WithCategory(QuestionCategories.Biology).WithLevel(QuestionLevels.Easy).Build()
        };
        _fileService.GetQuestions().Returns(allQuestions);

        // When
        _questionCache.Init();
        var actual = _questionCache.GetRandom("A", QuestionCategories.Biology, QuestionLevels.Hard, new HashSet<int>());

        // Then
        Assert.Null(actual);
    }
}