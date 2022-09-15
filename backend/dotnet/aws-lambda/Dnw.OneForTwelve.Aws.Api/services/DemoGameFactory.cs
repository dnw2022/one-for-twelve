using Dnw.OneForTwelve.Aws.Api.models;

namespace Dnw.OneForTwelve.Aws.Api.services;

public interface IDemoGameFactory {
  Languages Language { get; }
  public Game GetGame();
}