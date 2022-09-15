using Dnw.OneForTwelve.Aws.Api.models;

namespace Dnw.OneForTwelve.Aws.Api.services;

public class RandomQuestionSelector : QuestionSelector, IQuestionSelector
{
    public QuestionSelectionStrategies Strategy => QuestionSelectionStrategies.Random;

    public RandomQuestionSelector(IQuestionCache questionCache) : base(questionCache)
    {
    }

    protected override List<QuestionCategories> GetCategories()
    {
        return new List<QuestionCategories> {
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
    }

    protected override List<QuestionLevels> GetLevels()
    {
        return new List<QuestionLevels>
        {
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
}