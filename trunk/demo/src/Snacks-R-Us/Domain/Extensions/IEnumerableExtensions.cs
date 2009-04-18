using System;
using System.Collections.Generic;

namespace Snacks_R_Us.Domain.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool Exists<T>(this IEnumerable<T> list, Predicate<T> predicate)
        {
            return new List<T>(list).Exists(predicate);
        }

        public static T Find<T>(this IEnumerable<T> list, Predicate<T> predicate)
        {
            return new List<T>(list).Find(predicate);
        }
    }
}