namespace Dnw.OneForTwelve.Aws.Api.services;

public interface IWordCache
{
    string GetRandom();
}

public class WordCache : IWordCache
{
    private readonly IFileService _fileService;
    private string[] _words = Array.Empty<string>();

    public WordCache(IFileService fileService)
    {
        _fileService = fileService;
    }

    public string GetRandom()
    {
        var randomIndex = Random.Shared.Next(0, _words.Length);
        return _words[randomIndex];
    }
    
    internal void Init()
    {
        _words = _fileService.GetWords().ToArray();
    }
}