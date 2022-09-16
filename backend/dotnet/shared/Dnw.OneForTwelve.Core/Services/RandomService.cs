namespace Dnw.OneForTwelve.Core.Services;

public interface IRandomService
{
    int Next(int minValue, int maxValue);
}

public class RandomService : IRandomService
{
    public int Next(int minValue, int maxValue)
    {
        return Random.Shared.Next(minValue, maxValue);
    }
}