using Dnw.OneForTwelve.Core.models;

namespace Dnw.OneForTwelve.Core.services;

public interface IGameQuestionShuffler
{
    void ShuffleQuestions(IList<GameQuestion> questions);
}

public class GameQuestionShuffler : IGameQuestionShuffler
{
    public void ShuffleQuestions(IList<GameQuestion> questions)
    {
        var numberOfQuestions = questions.Count;
        for (var i = numberOfQuestions-1; i > 0; i--)
        {
            var randomQuestionIndexBefore = Random.Shared.Next(0, i + 1);
            SwapQuestionsNumbers(questions[i], questions[randomQuestionIndexBefore]);
        }
    }

    private static void SwapQuestionsNumbers(GameQuestion q1, GameQuestion q2)
    {
        (q1.Number, q2.Number) = (q2.Number, q1.Number);
    }
}