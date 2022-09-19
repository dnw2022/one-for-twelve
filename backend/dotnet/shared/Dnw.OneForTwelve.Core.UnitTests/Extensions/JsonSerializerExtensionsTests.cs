using System.Text.Json;
using System.Text.Json.Serialization;
using Dnw.OneForTwelve.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Dnw.OneForTwelve.Core.UnitTests.Extensions;

public class JsonSerializerExtensionsTests
{
    [Fact]
    public void ConfigureJsonSerializerOptions()
    {
        // Given
        var services = new ServiceCollection();

        // When
        services.ConfigureJsonSerializerOptions();

        // Then
        var sp = services.BuildServiceProvider();

        var options = sp.GetService<IOptions<JsonSerializerOptions>>()!.Value;
        AssertExpectedOptions(options);
    }

    [Fact]
    public void ConfigureDefaults()
    {
        // Given
        var options = new JsonSerializerOptions();

        // When
        options.ConfigureDefaults();

        // Then
        AssertExpectedOptions(options);
    }

    private static void AssertExpectedOptions(JsonSerializerOptions options)
    {
        Assert.Equal(JsonNamingPolicy.CamelCase, options.PropertyNamingPolicy);
        Assert.Equal(JsonIgnoreCondition.WhenWritingNull, options.DefaultIgnoreCondition);
        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        Assert.Contains(options.Converters, c => c.GetType() == typeof(JsonStringEnumConverter));
    }
}