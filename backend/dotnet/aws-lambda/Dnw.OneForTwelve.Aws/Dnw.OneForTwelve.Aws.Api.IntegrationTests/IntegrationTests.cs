using System.Net.Http.Json;
using System.Threading.Tasks;
using Dnw.OneForTwelve.Aws.Api.IntegrationTests.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Dnw.OneForTwelve.Aws.Api.IntegrationTests;

public class IntegrationTests
{
    [Fact]
    public async Task GetGame()
    {
        // Given
        await using var api = new WebApplicationFactory<Program>();
        var client = api.CreateClient();
        
        // When
        var game = await client.GetFromJsonAsync<Game>("/games/Dutch/Random");

        // Then
        Assert.NotNull(game);
        Assert.Equal(12, game!.Questions.Count);
    }
}