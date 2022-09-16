using Dnw.OneForTwelve.Core.Models;

namespace Dnw.OneForTwelve.Core.Services;

public class RandomQuestionSelector : IQuestionSelector
{
    private readonly IQuestionSelectorHelper _questionSelectorHelper;

    public QuestionSelectionStrategies Strategy => QuestionSelectionStrategies.Random;
    
    public List<GameQuestion> GetQuestions(string word)
    {
        return _questionSelectorHelper.GetQuestions(word, Categories, Levels);
    }

    public RandomQuestionSelector(IQuestionSelectorHelper questionSelectorHelper)
    {
        _questionSelectorHelper = questionSelectorHelper;
    }

    internal static readonly QuestionCategories[] Categories = {
        QuestionCategories.Geography,
        QuestionCategories.Bible,
        QuestionCategories.Biology,
        QuestionCategories.Cryptic,
        QuestionCategories.Economy,
        QuestionCategories.History,
        QuestionCategories.Art,
        QuestionCategories.Literature,
        QuestionCategories.Music,
        QuestionCategories.Politics,
        QuestionCategories.Sports,
        QuestionCategories.ScienceOrMaths
    };

    internal static readonly QuestionLevels[] Levels = {
        QuestionLevels.Easy,
        QuestionLevels.Easy,
        QuestionLevels.Easy,
        QuestionLevels.Easy,
        QuestionLevels.Normal,
        QuestionLevels.Normal,
        QuestionLevels.Normal,
        QuestionLevels.Normal,
        QuestionLevels.Hard,
        QuestionLevels.Hard,
        QuestionLevels.Hard,
        QuestionLevels.Hard
    };
}