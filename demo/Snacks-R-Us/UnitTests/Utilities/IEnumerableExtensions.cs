using System;
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

        public static T GetItem<T>(this IEnumerable<T> list, int index)
        {
            var counter = 0;
            foreach (var t in list)
            {
                if (index == counter++) return t;
            }
            return default(T);
        }

        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var item in list)
                action(item);
        }
    }
}