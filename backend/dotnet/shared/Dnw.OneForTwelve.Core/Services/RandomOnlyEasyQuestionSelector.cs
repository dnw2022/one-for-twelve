using Dnw.OneForTwelve.Core.Models;

namespace Dnw.OneForTwelve.Core.Services;

public class RandomOnlyEasyQuestionSelector : IQuestionSelector
{
    private readonly IQuestionSelectorHelper _questionSelectorHelper;
    
    public QuestionSelectionStrategies Strategy => QuestionSelectionStrategies.RandomOnlyEasy;
    
    public List<GameQuestion> GetQuestions(string word)
    {
        return _questionSelectorHelper.GetQuestions(word, Categories, Levels);
    }

    public RandomOnlyEasyQuestionSelector(IQuestionSelectorHelper questionSelectorHelper)
    {
        _questionSelectorHelper = questionSelectorHelper;
    }

    internal static readonly QuestionCategories[] Categories =
   {
            QuestionCategories.Geography,
            QuestionCategories.Geography,
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

    internal static readonly QuestionLevels[] Levels = Enumerable.Range(0, 12).Select(_ => QuestionLevels.Easy).ToArray();
}