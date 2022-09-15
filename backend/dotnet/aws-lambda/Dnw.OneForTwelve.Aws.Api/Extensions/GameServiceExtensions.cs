using Dnw.OneForTwelve.Aws.Api.services;

namespace Dnw.OneForTwelve.Aws.Api.Extensions;

public static class GameServiceExtensions
{
    public static void AddGameServices(this IServiceCollection services)
    {
        var fileService = new FileService();

        var wordCache = new WordCache(fileService);
        wordCache.Init();
        services.AddSingleton<IWordCache>(wordCache);

        var questionCache = new QuestionCache(fileService);
        questionCache.Init();
        services.AddSingleton<IQuestionCache>(questionCache);

        services.AddSingleton<IDemoGameFactory, DutchDemoGameFactory>();
        services.AddSingleton<IDemoGameFactory, EnglishDemoGameFactory>();
        services.AddSingleton<IGameService, GameService>();
        services.AddSingleton<IDutchRandomGameFactory, DutchRandomGameFactory>();
        services.AddSingleton<IQuestionSelectorFactory, QuestionSelectorFactory>();
        services.AddSingleton<IQuestionSelector, RandomQuestionSelector>();
        services.AddSingleton<IQuestionSelector, RandomOnlyEasyQuestionSelector>();
        services.AddSingleton<IGameQuestionShuffler, GameQuestionShuffler>();
    }
}