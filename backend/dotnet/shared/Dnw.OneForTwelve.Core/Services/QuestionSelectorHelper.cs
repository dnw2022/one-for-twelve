using Dnw.OneForTwelve.Core.Models;

namespace Dnw.OneForTwelve.Core.Services;

public interface IQuestionSelectorHelper
{
    List<GameQuestion> GetQuestions(string word, QuestionCategories[] categories, QuestionLevels[] levels);
}

public class QuestionSelectorHelper : IQuestionSelectorHelper
{
    private readonly IQuestionCache _questionCache;
    private readonly IItemPicker _itemPicker;

    public QuestionSelectorHelper(IQuestionCache questionCache, IItemPicker itemPicker)
    {
        _questionCache = questionCache;
        _itemPicker = itemPicker;
    }

    public List<GameQuestion> GetQuestions(string word, QuestionCategories[] categories, QuestionLevels[] levels)
    {
        var categoryList = categories.ToList();
        var levelList = levels.ToList();
        var selectedQuestions = new List<GameQuestion>();
        var selectedQuestionIds = new HashSet<int>();
            
        var letters = word.ToArray();

        for (var i = 0; i < letters.Length; i++)
        {
            var category = _itemPicker.PickRandom(categoryList);
            var level = _itemPicker.PickRandom(levelList);

            var firstLetterAnswer = letters[i].ToString().ToUpper();
            var question = _questionCache.GetRandom(firstLetterAnswer, category, level, selectedQuestionIds);

            if (question == null) return selectedQuestions;

            var gameQuestion = new GameQuestion(i+1, i+1, question);

            selectedQuestionIds.Add(question.Id);
            selectedQuestions.Add(gameQuestion);
        }

        return selectedQuestions;
    }
}