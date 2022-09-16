using Dnw.OneForTwelve.Core.Models;

namespace Dnw.OneForTwelve.Core.Services;

public interface IQuestionCache
{
    Question? GetRandom(string firstLetterAnswer, QuestionCategories category, QuestionLevels level, HashSet<int> invalidQuestionIds);
}

public class QuestionCache : IQuestionCache
{
    private readonly IFileService _fileService;
    private readonly IRandomService _randomService;
    private Dictionary<string, List<Question>> _questionsByFirstLetterAnswer = new();

    public QuestionCache(IFileService fileService, IRandomService randomService)
    {
        _fileService = fileService;
        _randomService = randomService;
    }

    public Question? GetRandom(string firstLetterAnswer, QuestionCategories category, QuestionLevels level, HashSet<int> invalidQuestionIds)
    {
        if (!_questionsByFirstLetterAnswer.TryGetValue(firstLetterAnswer, out var questionsWithFirstLetterAnswer))
        {
            return null;
        }
        
        var possibleQuestions =
            questionsWithFirstLetterAnswer.Where(q => q.Category == category && q.Level == level && !invalidQuestionIds.Contains(q.Id)).ToList();

        if (!possibleQuestions.Any()) return null;

        var randomQuestionIndex = _randomService.Next(0, possibleQuestions.Count);
        return possibleQuestions[randomQuestionIndex];
    }
    
    internal void Init()
    {
        _questionsByFirstLetterAnswer = _fileService.GetQuestions().GroupBy(q => q.FirstLetterAnswer).ToDictionary(g => g.Key, g => g.ToList());
    }
}