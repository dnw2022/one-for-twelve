using System.Linq;
using Dnw.OneForTwelve.Core.Services;
using Xunit;

namespace Dnw.OneForTwelve.Core.UnitTests.Services;

public class FileServiceTests
{
    [Fact]
    public void GetWords()
    {
        // Given
        var fileService = new FileService();

        // When
        var actual = fileService.GetWords().ToList();

        // Then
        Assert.Equal(412, actual.Count);
    }
    
    [Fact]
    public void GetQuestions()
    {
        // Given
        var fileService = new FileService();

        // When
        var actual = fileService.GetQuestions().ToList();

        // Then
        Assert.Equal(4282, actual.Count);
    }
}