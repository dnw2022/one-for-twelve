using Microsoft.Extensions.Logging;

namespace Dnw.OneForTwelve.Azure_Api.Middleware;

public interface ILogMessageWriter
{
    void Write(string message);
    int RandomNumber { get; }
}

public class LogMessageWriter : ILogMessageWriter
{
    private readonly ILogger<LogMessageWriter> _logger;
    public int RandomNumber { get; }

    public LogMessageWriter(ILogger<LogMessageWriter> logger)
    {
        _logger = logger;
        RandomNumber = Random.Shared.Next();
    }

    public void Write(string message) =>
        _logger.LogInformation(message);
}