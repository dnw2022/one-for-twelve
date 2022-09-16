using Dnw.OneForTwelve.Core.Models;

namespace Dnw.OneForTwelve.Core.Services;

public interface IQuestionCache
{
    Question? GetRandom(string firstLetterAnswer, QuestionCategories category, QuestionLevels level, HashSet<int> invalidQuestionIds);
}

public class QuestionCache : IQuestionCache
{
    private readonly IFileService _fileService;
    private Dictionary<string, List<Question>> _questionsByFirstLetterAnswer = new();

    public QuestionCache(IFileService fileService)
    {
        _fileService = fileService;
    }

    public Question? GetRandom(string firstLetterAnswer, QuestionCategories category, QuestionLevels level, HashSet<int> invalidQuestionIds)
    {
        var questionsWithFirstLetterAnswer = _questionsByFirstLetterAnswer[firstLetterAnswer];
        var possibleQuestions =
            questionsWithFirstLetterAnswer.Where(q => q.Category == category && q.Level == level && !invalidQuestionIds.Contains(q.Id)).ToList();

        if (!possibleQuestions.Any()) return null;

        var randomQuestionIndex = Convert.ToInt16(Math.Floor(Random.Shared.NextDouble() * possibleQuestions.Count));
        return possibleQuestions[randomQuestionIndex];
    }
    
    internal void Init()
    {
        _questionsByFirstLetterAnswer = _fileService.GetQuestions().GroupBy(q => q.FirstLetterAnswer).ToDictionary(g => g.Key, g => g.ToList());
    }
}