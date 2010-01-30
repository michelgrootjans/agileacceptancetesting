using System.Collections.Generic;

namespace Snacks_R_Us.AcceptanceTests.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IRowFixture<T> ToRowFixture<T>(this IEnumerable<T> list)
        {
            return new RowFixture<T>(list);
        }
    }
}