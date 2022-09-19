using Dnw.OneForTwelve.Core.Models;
using Dnw.OneForTwelve.Core.Services;
using JetBrains.Annotations;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Dnw.OneForTwelve.Azure_Api;

internal record GameApi(ILogger<GameApi> Logger, IGameService GameService)
{
    [UsedImplicitly]
    [Function(nameof(Games))]
    public Game? Games(
        [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "games/{language:alpha}/{strategy:alpha}")] HttpRequestData req,
        string language, 
        string strategy)
    {
        Logger.LogInformation("Language: {language}, Strategy: {strategy}", language, strategy);
        var game = GameService.Start(Enum.Parse<Languages>(language), Enum.Parse<QuestionSelectionStrategies>(strategy));

        return game;
    }
}