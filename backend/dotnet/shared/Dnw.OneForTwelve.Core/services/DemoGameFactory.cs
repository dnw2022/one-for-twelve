using Dnw.OneForTwelve.Core.models;

namespace Dnw.OneForTwelve.Core.services;

public interface IDemoGameFactory {
  Languages Language { get; }
  public Game GetGame();
}