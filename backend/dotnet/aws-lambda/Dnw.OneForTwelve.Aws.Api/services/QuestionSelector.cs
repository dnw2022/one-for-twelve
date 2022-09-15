using Dnw.OneForTwelve.Aws.Api.models;

namespace Dnw.OneForTwelve.Aws.Api.services;

public interface IQuestionSelector
{
    QuestionSelectionStrategies Strategy { get; }
    List<GameQuestion> GetQuestions(string word);
}

public abstract class QuestionSelector
{
    private readonly IQuestionCache _questionCache;

    protected QuestionSelector(IQuestionCache questionCache)
    {
        _questionCache = questionCache;
    }

    protected abstract List<QuestionCategories> GetCategories();
    protected abstract List<QuestionLevels> GetLevels();
    
    public List<GameQuestion> GetQuestions(string word)
    {
        var selectedQuestions = new List<GameQuestion>();
        var selectedQuestionIds = new HashSet<int>();

        var categories = GetCategories();
        var levels = GetLevels();
        var letters = word.ToArray();

        for (var i = 0; i < letters.Length; i++)
        {
            var category = RemoveRandomItem(categories);
            var level = RemoveRandomItem(levels);

            var firstLetterAnswer = letters[i].ToString().ToUpper();
            var question = _questionCache.GetRandom(firstLetterAnswer, category, level, selectedQuestionIds);

            if (question == null) return selectedQuestions;

            var gameQuestion = new GameQuestion(i+1, i+1, question);

            selectedQuestionIds.Add(question.Id);
            selectedQuestions.Add(gameQuestion);
        }

        return selectedQuestions;
    }
    
    private static T RemoveRandomItem<T>(IList<T> items)
    {
        var randomIndex = Convert.ToInt16(Math.Floor(Random.Shared.NextDouble() * items.Count));
        var item = items[randomIndex];
        items.RemoveAt(randomIndex);

        return item;
    }
}