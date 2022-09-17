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
        var remainingCategories = categories.ToList();
        var remainingLevels = levels.ToList();
        var selectedQuestions = new List<GameQuestion>();
        var selectedQuestionIds = new HashSet<int>();
            
        var letters = word.ToArray();

        for (var i = 0; i < letters.Length; i++)
        {
            var firstLetterAnswer = letters[i].ToString().ToUpper();

            var maxAttempts = remainingCategories.Count;
            Question? selectedQuestion = null;
            
            // First try to find a question by using each of the available
            // categories and levels only once
            for (var attempt = 1; attempt <= maxAttempts; attempt++)
            {
                selectedQuestion = GetRandomQuestion(firstLetterAnswer, remainingCategories, remainingLevels, selectedQuestionIds);
                if (selectedQuestion != null) break;
            }

            // If all attempts so far to select a question have failed
            // use a random category and level from the original list
            // we will not use the ideal question category and level distribution 
            // but its the best we can do
            maxAttempts = 5;
            if (selectedQuestion == null)
            {
                for (var attempt = 1; attempt <= maxAttempts; attempt++)
                {
                    selectedQuestion = GetRandomQuestion(firstLetterAnswer, categories, levels, selectedQuestionIds);
                    if (selectedQuestion != null) break;
                }
            }

            if (selectedQuestion == null) return selectedQuestions;

            selectedQuestionIds.Add(selectedQuestion.Id);
                
            remainingCategories.Remove(selectedQuestion.Category);
            remainingLevels.Remove(selectedQuestion.Level);

            var gameQuestion = new GameQuestion(i + 1, i + 1, selectedQuestion);
            selectedQuestions.Add(gameQuestion);
        }

        return selectedQuestions;
    }

    private Question? GetRandomQuestion(
        string firstLetterAnswer, 
        IList<QuestionCategories> categoryList,
        IList<QuestionLevels> levelList,
        HashSet<int> selectedQuestionIds)
    {
        var category = _itemPicker.PickRandom(categoryList);
        var level = _itemPicker.PickRandom(levelList);
        
        return GetRandomQuestion(firstLetterAnswer, selectedQuestionIds, category, level);
    }

    private Question? GetRandomQuestion(
        string firstLetterAnswer, 
        HashSet<int> selectedQuestionIds,
        QuestionCategories category, 
        QuestionLevels level)
    {
        return _questionCache.GetRandom(firstLetterAnswer, category, level, selectedQuestionIds);
    }
}