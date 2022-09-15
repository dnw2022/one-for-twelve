namespace Dnw.OneForTwelve.Aws.Api.services;

public interface IDemoGameFactory {
  Languages Language { get; }
  public Game GetGame();
}