using System;
using System.Collections.Generic;

namespace SlimUtilities
{
    public interface IRowFixture<T>
    {
        IRowFixture<T> AddColumn(string columnName, Func<T, object> converter);
        List<object> ToList();
    }

    internal class RowFixture<T> : IRowFixture<T>
    {
        private readonly List<T> list;
        private readonly IDictionary<string, Func<T, object>> customConverters;

        public RowFixture(IEnumerable<T> list)
        {
            this.list = new List<T>(list);
            customConverters = new Dictionary<string, Func<T, object>>();
        }

        public IRowFixture<T> AddColumn(string columnName, Func<T, object> converter)
        {
            customConverters.Add(columnName, converter);
            return this;
        }

        public List<object> ToList()
        {
            var result = new List<object>();
            foreach (var t in list)
            {
                result.Add(new RowFixtureItem<T>(t, customConverters).Properties);
            }
            return result;
        }
    }
}