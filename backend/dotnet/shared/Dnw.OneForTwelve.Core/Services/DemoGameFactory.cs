using Dnw.OneForTwelve.Core.Models;

namespace Dnw.OneForTwelve.Core.Services;

public interface IDemoGameFactory {
  Languages Language { get; }
  public Game GetGame();
}