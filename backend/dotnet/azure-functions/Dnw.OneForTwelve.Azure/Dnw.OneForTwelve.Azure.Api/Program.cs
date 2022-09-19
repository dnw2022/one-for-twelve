using Dnw.OneForTwelve.Azure_Api;
using Dnw.OneForTwelve.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(builder =>
    {
        builder.UseMiddleware<LoggingMiddleware>();
    })
    .ConfigureServices(services =>
    {
        services.AddScoped<ILogMessageWriter, LogMessageWriter>();
        
        services.ConfigureJsonSerializerOptions();
        services.AddGameServices();
    })
    .Build();

host.Run();