using JetBrains.Annotations;

namespace Dnw.OneForTwelve.Core.Models;

[UsedImplicitly]
public class Game
{
    public string Word { get; }
    [UsedImplicitly] public int NumberOfQuestions => Questions.Count();
    public IEnumerable<GameQuestion> Questions { get; }

    public Game(string word, IEnumerable<GameQuestion> questions)
    {
        Word = word;
        Questions = questions;
    }
}