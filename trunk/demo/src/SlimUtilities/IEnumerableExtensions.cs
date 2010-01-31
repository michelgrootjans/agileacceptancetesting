using System.Collections.Generic;

namespace SlimUtilities
{
    public static class IEnumerableExtensions
    {
        public static IRowFixture<T> ToRowFixture<T>(this IEnumerable<T> list)
        {
            return new RowFixture<T>(list);
        }
    }
}