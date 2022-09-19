using Dnw.OneForTwelve.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Dnw.OneForTwelve.Core.UnitTests.Extensions;

public class AuthExtensionsTests
{
    [Fact]
    public void AddFirebaseAuth()
    {
        // Given
        var services = new ServiceCollection();

        // When
        services.AddFirebaseAuth();

        // Then, no exceptions
        services.BuildServiceProvider();
    }
}