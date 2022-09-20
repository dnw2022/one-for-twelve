namespace Dnw.OneForTwelve.Core.Config;

public static class JwtBearerConfig
{
    public static string Authority => GetConfigSettings("Authority", "https://securetoken.google.com/one-for-twelve-32778");
    public static string Audience => GetConfigSettings("Audience", "one-for-twelve-32778");
    
    private static string GetConfigSettings(string key, string defaultValue)
    {
        var value = Environment.GetEnvironmentVariable(key);
        return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
    }
}