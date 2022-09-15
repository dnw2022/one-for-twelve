using Dnw.OneForTwelve.Core.models;

namespace Dnw.OneForTwelve.Core.services;

public interface IGameService {
  Game? Start(Languages languages, QuestionSelectionStrategies questionSelectionStrategy);
}

public class GameService : IGameService
{
  private readonly IDutchRandomGameFactory _dutchRandomGameFactory;
  private readonly Dictionary<string, IDemoGameFactory> _demoGameFactoriesByLanguage;
  
  public GameService(IDutchRandomGameFactory dutchRandomGameFactory, IEnumerable<IDemoGameFactory> demoGameFactories)
  {
    _dutchRandomGameFactory = dutchRandomGameFactory;
    _demoGameFactoriesByLanguage = demoGameFactories.ToDictionary(f => f.Language.ToString());
  }

  public Game? Start(Languages language, QuestionSelectionStrategies questionSelectionStrategy)
  {
    if (questionSelectionStrategy == QuestionSelectionStrategies.Demo)
    {
      return !_demoGameFactoriesByLanguage.TryGetValue(language.ToString(), out var demoGameFactory) ? null : demoGameFactory.GetGame();
    }

    return language == Languages.English ? null : _dutchRandomGameFactory.Get(questionSelectionStrategy);
  }  
}