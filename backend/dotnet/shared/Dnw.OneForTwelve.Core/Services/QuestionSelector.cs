using Dnw.OneForTwelve.Core.Models;

namespace Dnw.OneForTwelve.Core.Services;

public interface IQuestionSelector
{
    QuestionSelectionStrategies Strategy { get; }
    List<GameQuestion> GetQuestions(string word);
}