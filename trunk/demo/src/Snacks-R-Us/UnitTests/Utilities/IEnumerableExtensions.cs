using System.Collections.Generic;
using NUnit.Framework;

namespace Snacks_R_Us.UnitTests.Utilities
{
    public static class IEnumerableExtensions
    {
        public static void ShouldContain<T>(this IEnumerable<T> list, T expected)
        {
            Assert.Contains(expected, new List<T>(list));
        }
    }
}