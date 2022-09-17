using System.Collections.Generic;
using Dnw.OneForTwelve.Core.Services;
using NSubstitute;
using Xunit;

namespace Dnw.OneForTwelve.Core.UnitTests.Services;

public class ItemPickerTests
{
    [Fact]
    public void PickRandom()
    {
        // Given
        const int expectedItemIndex = 1;
        const string expectedItem = "b";
        
        var items = new List<string> { "a", expectedItem, "c" };

        var randomService = Substitute.For<IRandomService>();
        randomService.Next(0, items.Count).Returns(expectedItemIndex);
        
        var itemPicker = new ItemPicker(randomService);

        // When
        var actual = itemPicker.PickRandom(items);

        // Then
        Assert.Equal(expectedItem, actual);
        Assert.Equal(3, items.Count);
    }
}