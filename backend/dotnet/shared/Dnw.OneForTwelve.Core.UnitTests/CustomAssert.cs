using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Dnw.OneForTwelve.Core.UnitTests;

public static class CustomAssert
{
    public static void AreTypes<T>(IList<Type> expected, IList<T> actual) where T: class
    {
        Assert.Equal(expected.Count, actual.Count);

        foreach (var expectedItem in expected)
        {
            var actualType = actual.FirstOrDefault(impl => expectedItem == impl.GetType());
            Assert.IsType(expectedItem, actualType);
        }
    }
}