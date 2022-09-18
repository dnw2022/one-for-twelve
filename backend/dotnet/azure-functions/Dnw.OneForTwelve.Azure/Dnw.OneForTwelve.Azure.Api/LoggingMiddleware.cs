using JetBrains.Annotations;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.DependencyInjection;

namespace Dnw.OneForTwelve.Azure_Api;

[UsedImplicitly]
public class LoggingMiddleware : IFunctionsWorkerMiddleware
{
    // Since MyCustomMiddleware is always registered as a Singleton
    // _svc will always return the same instance
    private readonly ILogMessageWriter _logMessageWriter;

    public LoggingMiddleware(ILogMessageWriter logMessageWriter)
    {
        _logMessageWriter = logMessageWriter;
    }

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        // This is the only way to retrieve a scoped or transient dependency
        var svc = context.InstanceServices.GetRequiredService<ILogMessageWriter>();
        svc.Write($"RandomNumber: {_logMessageWriter.RandomNumber}");
        await next(context);
    }
}