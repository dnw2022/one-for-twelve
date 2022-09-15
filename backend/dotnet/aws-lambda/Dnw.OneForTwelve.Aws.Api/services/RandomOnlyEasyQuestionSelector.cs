using Dnw.OneForTwelve.Aws.Api.models;

namespace Dnw.OneForTwelve.Aws.Api.services;

public class RandomOnlyEasyQuestionSelector : QuestionSelector, IQuestionSelector
{
    public QuestionSelectionStrategies Strategy => QuestionSelectionStrategies.RandomOnlyEasy;
    
    public RandomOnlyEasyQuestionSelector(IQuestionCache questionCache) : base(questionCache)
    {
    }
    
    protected override List<QuestionCategories> GetCategories()
    {
        return new List<QuestionCategories> {
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
    }
    
    protected override List<QuestionLevels> GetLevels()
    {
        return Enumerable.Range(0, 12).Select(_ => QuestionLevels.Easy).ToList();
    }
}