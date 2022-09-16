using Dnw.OneForTwelve.Core.Models;

namespace Dnw.OneForTwelve.Core.Services;

public interface IQuestionSelectorFactory
{
    IQuestionSelector Create(QuestionSelectionStrategies strategy);
}

public class QuestionSelectorFactory : IQuestionSelectorFactory
{
    private readonly IEnumerable<IQuestionSelector> _questionSelectors;

    public QuestionSelectorFactory(IEnumerable<IQuestionSelector> questionSelectors)
    {
        _questionSelectors = questionSelectors;
    }
    
    public IQuestionSelector Create(QuestionSelectionStrategies strategy)
    {
        var questionSelector = _questionSelectors.FirstOrDefault(qs => qs.Strategy == strategy);

        if (questionSelector == null)
        {
            throw new ArgumentException($"No QuestionSelector found for strategy ${strategy.ToString()}");
        }

        return questionSelector;
    }  
}