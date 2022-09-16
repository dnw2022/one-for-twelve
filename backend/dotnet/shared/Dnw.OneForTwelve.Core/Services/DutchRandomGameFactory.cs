using Dnw.OneForTwelve.Core.Models;

namespace Dnw.OneForTwelve.Core.Services;

public interface IDutchRandomGameFactory
{
    Game Get(QuestionSelectionStrategies questionSelectionStrategy);
}

public class DutchRandomGameFactory : IDutchRandomGameFactory
{
    private readonly IWordCache _wordCache;
    private readonly IQuestionSelectorFactory _questionSelectorFactory;
    private readonly IGameQuestionShuffler _questionShuffler;

    public DutchRandomGameFactory(
        IWordCache wordCache,
        IQuestionSelectorFactory questionSelectorFactory,
        IGameQuestionShuffler questionShuffler)
    {
        _wordCache = wordCache;
        _questionSelectorFactory = questionSelectorFactory;
        _questionShuffler = questionShuffler;
    }

    public Game Get(QuestionSelectionStrategies questionSelectionStrategy)
    {
        var word = _wordCache.GetRandom();
        var questionSelector = _questionSelectorFactory.Create(questionSelectionStrategy);
        var questions = questionSelector.GetQuestions(word);
        _questionShuffler.ShuffleQuestions(questions);

        return new Game(word, questions);
    }
}