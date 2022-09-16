using Dnw.OneForTwelve.Core.Models;

namespace Dnw.OneForTwelve.Core.UnitTests.Utils;

internal class TestQuestionBuilder
{
    private QuestionCategories _category = QuestionCategories.Art;
    private QuestionLevels _level = QuestionLevels.Easy;
        
    public TestQuestionBuilder WithCategory(QuestionCategories category)
    {
        _category = category;
        return this;
    }

    public TestQuestionBuilder WithLevel(QuestionLevels level)
    {
        _level = level;
        return this;
    }
        
    public Question Build()
    {
        return Question.CreateText(1, _category, "text", "answer", _level);
    }
}