namespace Dnw.OneForTwelve.Aws.Api.services;

public interface IGameService {
  Game? Start(Languages languages, QuestionSelectionStrategies questionSelectionStrategy);
}

public class GameService : IGameService
{
  private readonly Dictionary<string, IDemoGameFactory> _demoGameFactoriesByLanguage;

  public GameService(IEnumerable<IDemoGameFactory> demoGameFactories) {
    _demoGameFactoriesByLanguage = demoGameFactories.ToDictionary(f => f.Language.ToString());
  }

  public Game? Start(Languages language, QuestionSelectionStrategies questionSelectionStrategy)
  {
    if (questionSelectionStrategy == QuestionSelectionStrategies.Demo) {
      if (!_demoGameFactoriesByLanguage.TryGetValue(language.ToString(), out var demoGameFactory)){
        return null;
      }

      return demoGameFactory.GetGame();
    }

    return null;
  }  
}