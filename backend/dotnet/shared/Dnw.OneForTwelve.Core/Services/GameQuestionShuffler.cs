using Dnw.OneForTwelve.Core.Models;

namespace Dnw.OneForTwelve.Core.Services;

public interface IGameQuestionShuffler
{
    void ShuffleQuestions(IList<GameQuestion> questions);
}

public class GameQuestionShuffler : IGameQuestionShuffler
{
    private readonly IRandomService _randomService;

    public GameQuestionShuffler(IRandomService randomService)
    {
        _randomService = randomService;
    }
    
    public void ShuffleQuestions(IList<GameQuestion> questions)
    {
        var numberOfQuestions = questions.Count;
        for (var i = numberOfQuestions-1; i > 0; i--)
        {
            var randomQuestionIndexBefore = _randomService.Next(0, i + 1);
            SwapQuestionsNumbers(questions[i], questions[randomQuestionIndexBefore]);
        }
    }

    private static void SwapQuestionsNumbers(GameQuestion q1, GameQuestion q2)
    {
        (q1.Number, q2.Number) = (q2.Number, q1.Number);
    }
}