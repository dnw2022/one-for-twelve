namespace Dnw.OneForTwelve.Core.Services;

public interface IWordCache
{
    string GetRandom();
}

public class WordCache : IWordCache
{
    private readonly IFileService _fileService;
    private readonly IRandomService _randomService;
    private string[] _words = Array.Empty<string>();

    public WordCache(IFileService fileService, IRandomService randomService)
    {
        _fileService = fileService;
        _randomService = randomService;
    }

    public string GetRandom()
    {
        var randomIndex = _randomService.Next(0, _words.Length);
        return _words[randomIndex];
    }
    
    internal void Init()
    {
        _words = _fileService.GetWords().ToArray();
    }
}